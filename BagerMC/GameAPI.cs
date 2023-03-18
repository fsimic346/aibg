using BagerMC.DTO.Action;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BagerMC
{
    internal class GameAPI
    {
        private const string BASE_URL = "https://localhost:8080";
        private HttpClient client = new();
        public GameAPI()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }
        public async Task createGame(Training credentials)
        {
            var json = await client.PostAsJsonAsync(
                 "http://localhost:8080/train/makeGame", credentials);

            Console.Write(json);
        }
        public void move(Move moveDTO)
        {
            HttpClient client = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Post, BASE_URL + "/train/move"))
            {
                string json = JsonConvert.SerializeObject(moveDTO);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.Send(request);
                response.EnsureSuccessStatusCode();

                using var streamReader = new StreamReader(response.Content.ReadAsStream());
                Console.WriteLine(streamReader.ReadToEnd());
            }
        }
    }
}
