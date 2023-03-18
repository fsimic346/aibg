using Newtonsoft.Json;

namespace BagerMC.DTO.Model
{

    public enum ExecutedAction
    {
        MOVE,
        MOVE_IMPOSSIBLE,
        MOVE_ON_POND,
        CONVERT_NECTAR_TO_HONEY,
        SKIP_A_TURN,
        FEED_BEE_WITH_NECTAR,
    }
    public class Player
    {
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
        [JsonProperty("energy")]
        public int Energy { get; set; }
        [JsonProperty("nectar")]
        public int Nectar { get; set; }
        [JsonProperty("honey")]
        public int Honey { get; set; }
        [JsonProperty("turnsFrozen")]
        public int TurnsFrozen { get; set; }
        [JsonProperty("numOfSkipATurnUsed")]
        public int NumOfSkipATurnUsed { get; set; }
        [JsonProperty("executedAction")]
        public ExecutedAction? ExecutedAction { get; set; }
        [JsonProperty("distanceMoved")]
        public int DistanceMoved { get; set; }
        [JsonProperty("teamName")]
        public string TeamName { get; set; }
        [JsonProperty("skin")]
        public int Skin { get; set; }
        [JsonProperty("hiveX")]
        public int HiveX { get; set; }
        [JsonProperty("hiveY")]
        public int HiveY { get; set; }
        [JsonProperty("frozen")]
        public bool Frozen { get; set; }

        public Player(int x, int y, int score, int energy, int nectar, int honey, int turnsFrozen, int numOfSkipATurnUsed, ExecutedAction? executedAction, int distanceMoved, string teamName, int skin, int hiveX, int hiveY, bool frozen)
        {
            X = x;
            Y = y;
            Score = score;
            Energy = energy;
            Nectar = nectar;
            Honey = honey;
            TurnsFrozen = turnsFrozen;
            NumOfSkipATurnUsed = numOfSkipATurnUsed;
            ExecutedAction = executedAction;
            DistanceMoved = distanceMoved;
            TeamName = teamName;
            Skin = skin;
            HiveX = hiveX;
            HiveY = hiveY;
            Frozen = frozen;
        }

        public Player DeepCopy()
        {
            Player deepcopyPlayer = new Player(this.X, this.Y, this.Score, this.Energy, this.Nectar, this.Honey, this.TurnsFrozen, this.NumOfSkipATurnUsed, this.ExecutedAction, this.DistanceMoved, this.TeamName, this.Skin, this.HiveX, this.HiveY, this.Frozen);
            return deepcopyPlayer;
        }
    }
}