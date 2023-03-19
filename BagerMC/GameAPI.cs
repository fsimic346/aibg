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
        private int GameId = 123;
        public string BaseUrl = "http://localhost:8080/";
        public HttpClient HttpClient;
        private bool IsTraining = false;
        public static bool IsFirstPlayer = true;
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
        public async Task CreateGame(Training data)
        {
            string uri = IsTraining ? BaseUrl + "makeGame" : BaseUrl + $"joinGame?playerId={PlayerId}&gameId={GameId}";
            //string uri = $"http://localhost:8080/botVSbot?player1Id={PlayerId}&player2Id={PlayerId + 1}";
            if (!IsTraining)
            {
                var response = HttpClient.GetStringAsync(uri).Result;
                Game = JsonConvert.DeserializeObject<Game>(response);
                if(Game.Player1.ExecutedAction != null)
                {
                    IsFirstPlayer = false;
                }
            }
            else
            {

                var response = HttpClient.PostAsJsonAsync(uri, data).Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Game = JsonConvert.DeserializeObject<Game>(jsonString);
                }
            }
        }
    }
}
