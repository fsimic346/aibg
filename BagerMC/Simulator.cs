using BagerMC.DTO.Action;
using BagerMC.DTO.Exceptions;
using BagerMC.DTO.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagerMC
{
    internal class Simulator
    {
        /// <summary>
        /// Applies ConvertNectarToHoney action to currentState
        /// Throws InvalidActionException if player is not in his hive
        /// </summary>
        public static void ApplyConvertNectarToHoney(Game currentState, ConvertNectarToHoney action, bool isFirstPlayer)
        {
            Player player = isFirstPlayer ? currentState.Player1 : currentState.Player2;
            if (player.X != player.HiveX || player.HiveY != player.Y)
            {
                throw new InvalidActionException("Bee is not in a hive");
            }
            int honeyMaxConversion = player.Nectar / 20;
            int honeyProduced = Math.Min(honeyMaxConversion, action.AmountOfHoneyToMake);
            player.Nectar -= honeyProduced * 20;
            player.Score += 30 * honeyProduced;
        }

        /// <summary>
        /// Applies FeedBeeWithNectar action to currentState
        /// Throws InvalidActionException if player is not in his hive
        /// </summary>
        public static void ApplyFeedBeeWithNectar(Game currentState, FeedBeeWithNectar action, bool isFirstPlayer)
        {
            Player player = isFirstPlayer ? currentState.Player1 : currentState.Player2;
            if (player.X != player.HiveX || player.Y != player.HiveY)
            {
                throw new InvalidActionException("Bee is not in a hive");
            }
            int nectarUsage = Math.Min(action.AmountOfNectarToFeedWith, player.Nectar);
            player.Nectar -= nectarUsage;
            player.Energy += nectarUsage * 2;
        }
        public static void ApplyMove(Game currentState, Move action, bool isFirstPlayer)
        {
            Player player = isFirstPlayer ? currentState.Player1 : currentState.Player2;
            Player opponent = !isFirstPlayer ? currentState.Player1 : currentState.Player2;
            if (player.Frozen)
                throw new InvalidActionException("Player is frozen");
            for (int i = 0; i < action.Distance; i++)
            {
                Tuple<int, int> updatedCoo = getCoordinatesAfterMove(player.X, player.Y, action.Direction);
                if (updatedCoo.Item1 < currentState.Map.Height && updatedCoo.Item2 < currentState.Map.Width && updatedCoo.Item1 >= 0 && updatedCoo.Item2 >= 0)
                {
                    player.Energy -= 2;
                    player.X = updatedCoo.Item1;
                    player.Y = updatedCoo.Item2;
                    if (player.Energy == 0)
                    {
                        currentState.Finished = true;
                        currentState.WinnerTeamName = opponent.TeamName;
                        return;
                    }
                    Tile tile = currentState.Map.Tiles[updatedCoo.Item1, updatedCoo.Item2];
                    ApplyTile(tile, player, opponent, currentState);
                    if (currentState.Finished)
                        return;
                }
                else
                {
                    throw new InvalidActionException("You are hitting the wall");
                }
            }
        }
        public static void ApplySkipATurn(Game currentState, bool isFirstPlayer)
        {
            Player player = isFirstPlayer ? currentState.Player1 : currentState.Player2;
            player.Energy += 5;
        }
        private static void ApplyTile(Tile tile, Player player, Player opponent, Game currentState)
        {
            int pct, newNectar;
            switch (tile.TileContent.ItemType)
            {
                case (ItemType.CHERRY_BLOSSOM):
                case (ItemType.LILAC):
                case (ItemType.ROSE):
                case (ItemType.SUNFLOWER):
                    tile.TileContent.ItemType = ItemType.EMPTY;
                    if (player.Nectar < 100)
                    {
                        player.Score += tile.TileContent.NumOfItems;
                        player.Nectar = Math.Min(100, tile.TileContent.NumOfItems + player.Nectar);
                    }
                    break;
                case (ItemType.POND):
                    player.Score -= 100000;
                    currentState.Finished = true;
                    currentState.WinnerTeamName = opponent.TeamName;
                    break;
                case (ItemType.ENERGY):
                    int newEnergy = Math.Min(100, tile.TileContent.NumOfItems + player.Energy);
                    player.Score += newEnergy - player.Energy;
                    player.Energy = newEnergy;
                    tile.TileContent.ItemType = ItemType.EMPTY;
                    break;
                case (ItemType.MINUS_FIFTY_PCT_ENERGY):
                    opponent.Energy /= 2;
                    player.Score += 50;
                    tile.TileContent.ItemType = ItemType.EMPTY;
                    if (opponent.Energy == 0)
                    {
                        currentState.Finished = true;
                        currentState.WinnerTeamName = player.TeamName;
                    }
                    break;
                case (ItemType.BOOSTER_NECTAR_100_PCT):
                    tile.TileContent.ItemType = ItemType.EMPTY;
                    newNectar = Math.Min(100, player.Nectar * 2);
                    if (player.Nectar < 100)
                    {
                        player.Score += newNectar - player.Nectar;
                        player.Nectar = newNectar;
                    }
                    break;
                case (ItemType.BOOSTER_NECTAR_50_PCT):
                    tile.TileContent.ItemType = ItemType.EMPTY;
                    pct = (int)Math.Floor(player.Nectar * 1.5);
                    newNectar = Math.Min(100, pct);
                    if (player.Nectar < 100)
                    {
                        player.Score += newNectar - player.Nectar;
                        player.Nectar = newNectar;
                    }
                    break;
                case (ItemType.BOOSTER_NECTAR_30_PCT):
                    tile.TileContent.ItemType = ItemType.EMPTY;
                    pct = (int)Math.Floor(player.Nectar * 1.3);
                    newNectar = Math.Min(100, pct);
                    if (player.Nectar < 100)
                    {
                        player.Score += newNectar - player.Nectar;
                        player.Nectar = newNectar;
                    }
                    break;
                case (ItemType.SUPER_HONEY):
                    tile.TileContent.ItemType = ItemType.EMPTY;
                    player.Honey += tile.TileContent.NumOfItems;
                    player.Score += tile.TileContent.NumOfItems * 30;
                    break;
                case (ItemType.FREEZE):
                    player.Score += 100;
                    tile.TileContent.ItemType = ItemType.EMPTY;
                    opponent.Frozen = true;
                    break;
            }
        }
        private static void GetMovesForDirection(string direction, Game currentState, bool isFirstPlayer, List<GameState> states)
        {
            int distance = 1;
            while (true)
            {
                Game tempState = currentState.DeepCopy();
                Move action = new Move { Direction = direction, Distance = distance, GameId = currentState.GameId, PlayerId = currentState.CurrentPlayerId };
                try
                {
                    ApplyMove(tempState, action, isFirstPlayer);
                    Console.WriteLine(direction + " " + distance);
                }
                catch (InvalidActionException e)
                {
                    //Console.WriteLine(direction + " " + distance);
                    break;
                }
                states.Add(new GameState { Action = action, State = tempState });
                if (tempState.Finished)
                {
                    break;
                }
                distance++;
            }

        }
        public static List<GameState> GetPossibleStates(Game currentState, bool isFirstPlayer)
        {
            List<GameState> states = new List<GameState>();
            GetMovesForDirection("q", currentState, isFirstPlayer, states);
            GetMovesForDirection("w", currentState, isFirstPlayer, states);
            GetMovesForDirection("e", currentState, isFirstPlayer, states);
            GetMovesForDirection("a", currentState, isFirstPlayer, states);
            GetMovesForDirection("s", currentState, isFirstPlayer, states);
            GetMovesForDirection("d", currentState, isFirstPlayer, states);
            Player player = isFirstPlayer ? currentState.Player1 : currentState.Player2;
            if (player.X == player.HiveX && player.Y == player.HiveY)
            {
                Game tempStateRegenerateEnergy = currentState.DeepCopy();
                FeedBeeWithNectar feedAction = new FeedBeeWithNectar { GameId = currentState.GameId, PlayerId = currentState.CurrentPlayerId, AmountOfNectarToFeedWith = (100 - player.Energy) / 2 };
                ApplyFeedBeeWithNectar(tempStateRegenerateEnergy, feedAction, isFirstPlayer);
                states.Add(new GameState { Action = feedAction, State = tempStateRegenerateEnergy });

                Game tempStateCreateHoney = currentState.DeepCopy();
                ConvertNectarToHoney createHoneyAction = new ConvertNectarToHoney { GameId = currentState.GameId, PlayerId = currentState.CurrentPlayerId, AmountOfHoneyToMake = 10 };
                ApplyFeedBeeWithNectar(tempStateCreateHoney, feedAction, isFirstPlayer);
                states.Add(new GameState { Action = createHoneyAction, State = tempStateCreateHoney });
            }
            Game tempStateSkipATurn = currentState.DeepCopy();
            SkipATurn action = new SkipATurn { GameId = currentState.GameId, PlayerId = currentState.CurrentPlayerId };
            ApplySkipATurn(tempStateSkipATurn, isFirstPlayer);
            states.Add(new GameState { Action = action, State = tempStateSkipATurn });
            return states;
        }

        private static Tuple<int, int> getCoordinatesAfterMove(int xCo, int yCo, String dir)
        {
            int offset;
            switch (dir)
            {
                case "w":
                    return Tuple.Create(xCo - 2, yCo);
                case "s":
                    return Tuple.Create(xCo + 2, yCo);
                case "q":
                    offset = xCo % 2 == 0 ? -1 : 0;
                    return Tuple.Create(xCo - 1, yCo + offset);
                case "e":
                    offset = xCo % 2 == 0 ? 0 : 1;
                    return Tuple.Create(xCo - 1, yCo + offset);
                case "a":
                    offset = xCo % 2 == 0 ? -1 : 0;
                    return Tuple.Create(xCo + 1, yCo + offset);
                case "d":
                    offset = xCo % 2 == 0 ? 0 : 1;
                    return Tuple.Create(xCo + 1, yCo + offset);
                default:
                    return Tuple.Create(0, 0);
            }
        }
        public static bool IsGameFinished(Game game)
        {
            // 1. Uslov za kraj (dostignut potez 500)
            if (game.NumOfMove >= 500)
            {
                game.Finished = true;
                return true;
            }
            // 2. Uslov za kraj (nema cveca na mapi)
            if (IsBoardEmpty(game) == true)
            {
                game.Finished = true;
                return true;
            }
            // 3. Uslov za kraj (SkipATurn > 150)
            if (game.Player1.NumOfSkipATurnUsed == 150 || game.Player2.NumOfSkipATurnUsed == 150)
            {
                game.Finished = true;
                return true;
            }
            // 5. Uslov za kraj (Igrac stao na POND polje)

            // 6. Uslov za kraj (Igrac nema vise energije)
            if (game.Player1.Energy == 0 || game.Player2.Energy == 0)
            {
                game.Finished = true;
                return true;
            }

            return false;
        }

        public static bool IsBoardEmpty(Game game)
        {
            foreach (var tile in game.Map.Tiles)
            {
                if (tile.TileContent.ItemType == ItemType.CHERRY_BLOSSOM || tile.TileContent.ItemType == ItemType.ROSE || tile.TileContent.ItemType == ItemType.LILAC || tile.TileContent.ItemType == ItemType.SUNFLOWER)
                    return false;
            }
            return true;
        }

        public static Direction GetDirection(int oldX, int oldY, int newX, int newY, int distanceTraveled)
        {
            if (oldX > newX && oldY == newY)
            {
                if (distanceTraveled == 1 && Math.Abs(newX - oldX) == 1)
                    return Direction.e;
                return Direction.w;
            }
            if (newX > oldX && oldY == newY)
            {
                if (distanceTraveled == 1 && Math.Abs(newX - oldX) == 1)
                    return Direction.a;
                return Direction.s;
            }
            if (oldX > newX && oldY <= newY)
            {
                return Direction.e;
            }
            if (oldX > newX && newY <= oldY)
            {
                return Direction.q;
            }
            if (newX > oldX && newY <= oldY)
            {
                return Direction.a;
            }
            return Direction.d;
        }

        public static void HandleOpponent(Game oldState, Game newState)
        {
            Player opponent = newState.Player2;
            try
            {
                if (opponent.ExecutedAction == ExecutedAction.MOVE || opponent.ExecutedAction == ExecutedAction.MOVE_ON_POND || opponent.ExecutedAction == ExecutedAction.MOVE_IMPOSSIBLE)
                {
                    Move move = new Move();
                    move.Distance = opponent.DistanceMoved;
                    move.Direction = GetDirection(oldState.Player2.X, oldState.Player2.Y, opponent.X, opponent.Y, opponent.DistanceMoved).ToString();
                    Console.WriteLine(move.Direction);
                    ApplyMove(oldState, move, false);
                }
                else if (opponent.ExecutedAction == ExecutedAction.SKIP_A_TURN)
                {
                    ApplySkipATurn(oldState, false);
                }
                else if (opponent.ExecutedAction == ExecutedAction.FEED_BEE_WITH_NECTAR)
                {
                    FeedBeeWithNectar feedBeeWithNectar = new FeedBeeWithNectar();
                    feedBeeWithNectar.AmountOfNectarToFeedWith = (newState.Player2.Energy - oldState.Player2.Energy) / 2;
                    ApplyFeedBeeWithNectar(oldState, feedBeeWithNectar, false);

                }
                else
                {
                    ConvertNectarToHoney convertNectarToHoney = new ConvertNectarToHoney();
                    convertNectarToHoney.AmountOfHoneyToMake = (newState.Player2.Honey - oldState.Player2.Honey);
                    ApplyConvertNectarToHoney(oldState, convertNectarToHoney, false);
                }
            }
            catch (Exception)
            {

            }
        }


    }
}
