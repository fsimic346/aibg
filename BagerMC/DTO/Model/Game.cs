using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BagerMC.DTO.Model
{
    public class Game
    {
        [JsonProperty("gameId")]
        public int GameId { get; set; }
        [JsonProperty("player1")]
        public Player Player1 { get; set; }
        [JsonProperty("player2")]
        public Player Player2 { get; set; }
        [JsonProperty("currentPlayerId")]
        public int CurrentPlayerId { get; set; }
        [JsonProperty("map")]
        public Map Map { get; set; }
        [JsonProperty("player1ChangedTiles")]
        public List<Tile> Player1ChangedTiles { get; set; }
        [JsonProperty("player2ChangedTiles")]
        public List<Tile> Player2ChangedTiles { get; set; }
        [JsonProperty("numOfMove")]
        public int NumOfMove { get; set; }
        [JsonProperty("finished")]
        public bool Finished { get; set; }
        [JsonProperty("winnerTeamName")]
        public string WinnerTeamName { get; set; }
        [JsonProperty("reasonForEndingGame")]
        public string ReasonForEndingGame { get; set; }

        public Game(int gameId, Player player1, Player player2, int currentPlayerId, Map map, List<Tile> player1ChangedTiles, List<Tile> player2ChangedTiles, int numOfMove, bool finished, string winnerTeamName, string reasonForEndingGame)
        {
            GameId = gameId;
            Player1 = player1;
            Player2 = player2;
            CurrentPlayerId = currentPlayerId;
            Map = map;
            Player1ChangedTiles = player1ChangedTiles;
            Player2ChangedTiles = player2ChangedTiles;
            NumOfMove = numOfMove;
            Finished = finished;
            WinnerTeamName = winnerTeamName;
            ReasonForEndingGame = reasonForEndingGame;
        }

        public Game DeepCopy()
        {
            List<Tile> deepcopyP1ChangedTiles = new List<Tile>();
            foreach (var item in Player1ChangedTiles)
            {
                deepcopyP1ChangedTiles.Add(item);
            }
            List<Tile> deepcopyP2ChangedTiles = new List<Tile>();
            foreach (var item in Player2ChangedTiles)
            {
                deepcopyP2ChangedTiles.Add(item);
            }
            Game deepcopyGame = new Game(this.GameId, this.Player1.DeepCopy(), this.Player2.DeepCopy(), this.CurrentPlayerId, this.Map.DeepCopy(), deepcopyP1ChangedTiles, deepcopyP2ChangedTiles, this.NumOfMove, this.Finished, this.WinnerTeamName, this.ReasonForEndingGame);
            return deepcopyGame;
        }
    }
}
