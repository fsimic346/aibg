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
Console.ReadLine();

while (!gameAPI.Game.Finished)
{
    GameState currentState = new GameState();
    currentState.State = gameAPI.Game;
    GameState result = MinMax.MiniMax(currentState, 1, -100000, 1000000, true);
    result.Action.Execute(gameAPI);
}