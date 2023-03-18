using BagerMC.DTO.Action;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using BagerMC.DTO.Model;
using BagerMC;

GameAPI gameAPI = new GameAPI();

Training training = new Training();
training.PlayerSpot = 1;
training.PlayerId = gameAPI.PlayerId;
await gameAPI.CreateGame(training);

Move move = new Move();
move.PlayerId = gameAPI.PlayerId;
move.GameId = gameAPI.Game.GameId;

Game localGame = gameAPI.Game.DeepCopy();

string input;
bool isRunning = true;

while (isRunning)
{
    input = Console.ReadLine();
    List<string> inputArgs = input.Split(" ").ToList();
    switch (inputArgs[0])
    {
        case "move":
            move.Distance = int.Parse(inputArgs[1]);
            move.Direction = inputArgs[2];
            Simulator.ApplyMove(localGame, move, true);
            await gameAPI.ExecuteAction(move);
            Simulator.HandleOpponent(localGame, gameAPI.Game);
            Console.WriteLine(JsonConvert.SerializeObject(localGame.Player1));
            Console.WriteLine("=====================");
            Console.WriteLine(JsonConvert.SerializeObject(gameAPI.Game.Player1));
            Console.WriteLine("*********************");
            Console.WriteLine(JsonConvert.SerializeObject(localGame.Player2));
            Console.WriteLine("=====================");
            Console.WriteLine(JsonConvert.SerializeObject(gameAPI.Game.Player2));
            break;
    }
}
await gameAPI.ExecuteAction(move);
//move.PlayerId = gameAPI.PlayerId + 1;
//move.Direction = Direction.w.ToString();
//await gameAPI.ExecuteAction(move);