using BagerMC.DTO.Action;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using BagerMC.DTO.Model;
using BagerMC;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

await ProcessRepositoriesAsync(client);

static async Task ProcessRepositoriesAsync(HttpClient client)
{
    Training training = new Training();
    training.PlayerId = 132485;
    training.PlayerSpot = 1;
    var response = client.PostAsJsonAsync("http://localhost:8080/train/makeGame", training).Result;

    if (response.IsSuccessStatusCode)
    {
        var jsonString = await response.Content.ReadAsStringAsync();
        GlobalState.Game = JsonConvert.DeserializeObject<Game>(jsonString);

    }
}