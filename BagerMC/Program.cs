using BagerMC.DTO.Action;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using BagerMC.DTO.Model;
using BagerMC;

GameAPI gameAPI= new GameAPI();

Training training = new Training();
training.PlayerSpot = 1;
training.PlayerId = gameAPI.PlayerId;
await gameAPI.CreateGame(training);

Move move = new Move();
move.Distance = 2;
move.PlayerId = gameAPI.PlayerId;
move.GameId = gameAPI.Game.GameId;
move.Direction = Direction.s.ToString();
Console.ReadLine();
await gameAPI.ExecuteAction(move);
//move.PlayerId = gameAPI.PlayerId + 1;
//move.Direction = Direction.w.ToString();
//await gameAPI.ExecuteAction(move);