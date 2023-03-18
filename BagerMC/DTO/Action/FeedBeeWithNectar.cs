using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagerMC.DTO.Action
{
    public class FeedBeeWithNectar : BaseAction
    {
        [JsonProperty("playerId")]
        public int PlayerId { get; set; }
        [JsonProperty("gameId")]
        public int GameId { get; set; }
        [JsonProperty("amountOfNectarToFeedWith")]
        public int AmountOfNectarToFeedWith { get; set; }
    }
}
