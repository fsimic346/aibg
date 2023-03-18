using BagerMC.DTO.Action;
using BagerMC.DTO.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BagerMC
{
    public class GameAPI
    {
        public Game Game;
        public int PlayerId = 132485;
        private int GameId;
        public string BaseUrl = "http://localhost:8080/";
        public HttpClient HttpClient;
        private bool IsTraining = true;
        public GameAPI()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            HttpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            if (IsTraining)
                BaseUrl += "train/";
        }

        //public async Task ExecuteAction<T>(T data) where T:BaseAction
        //{
        //    var genType = data.GetType();

        //    if (genType != typeof(ConvertNectarToHoney) && genType != typeof(FeedBeeWithNectar) && genType != typeof(Move) && genType != typeof(SkipATurn)) { return; }


        //    Move move = data as Move;
        //    string endpoint = char.ToLower(genType.Name[0]) + genType.Name[1..];
        //    var response = HttpClient.PostAsJsonAsync(BaseUrl + endpoint, data).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonString = await response.Content.ReadAsStringAsync();
        //        Game = JsonConvert.DeserializeObject<Game>(jsonString);
        //    }
        //}
        public async Task CreateGame(Training data)
        {
            string uri = IsTraining ? BaseUrl + "makeGame" : BaseUrl + $"joinGame?playerId={PlayerId}&gameId={GameId}";
            //string uri = $"http://localhost:8080/botVSbot?player1Id={PlayerId}&player2Id={PlayerId+1}";
            // var response = HttpClient.GetStringAsync(uri).Result;

            var response = HttpClient.PostAsJsonAsync(uri, data).Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Game = JsonConvert.DeserializeObject<Game>(jsonString);
            }
        }
        //public async Task Move(Move data)
        //{
        //    var response = HttpClient.PostAsJsonAsync("move", data).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonString = await response.Content.ReadAsStringAsync();
        //        GlobalState.Game = JsonConvert.DeserializeObject<Game>(jsonString);
        //    }
        //}

        //public async Task ConvertNectarToHoney(ConvertNectarToHoney data)
        //{
        //    var response = HttpClient.PostAsJsonAsync("convertNectarToHoney", data).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonString = await response.Content.ReadAsStringAsync();
        //        GlobalState.Game = JsonConvert.DeserializeObject<Game>(jsonString);
        //    }
        //}

        //public async Task FeedBeWithNectar(FeedBeeWithNectar data)
        //{
        //    var response = HttpClient.PostAsJsonAsync("feedBeeWithNectar", data).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonString = await response.Content.ReadAsStringAsync();
        //        GlobalState.Game = JsonConvert.DeserializeObject<Game>(jsonString);
        //    }
        //}

        //public async Task SkipATurn(SkipATurn data)
        //{
        //    var response = HttpClient.PostAsJsonAsync("skipATurn", data).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonString = await response.Content.ReadAsStringAsync();
        //        GlobalState.Game = JsonConvert.DeserializeObject<Game>(jsonString);
        //    }
        //}
    }
}
