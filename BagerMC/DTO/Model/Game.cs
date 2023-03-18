using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
    }
}
