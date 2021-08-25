using AppGameScoreStandard.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppGameScoreStandard.Services
{
    public class GameScoreApi
    {
        const string URL = "http://restdfilitto.herokuapp.com/highscores";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Connection", "close");

            return client;
        }

        public async Task<List<GameScore>> GetHighScores()
        {
            HttpClient client = GetClient();

            var response = await client.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<GameScore>>(content);
            }

            return new List<GameScore>();
        }

        public async Task<GameScore> GetHighScore(int Id)
        {
            string dados = URL + "?id=" + Id;
            //Uri uri = new Uri(dados);
            HttpClient client = GetClient();
            HttpResponseMessage response = await client.GetAsync(dados);
            //var response = await client.GetAsync(dados);
            if (response.IsSuccessStatusCode) //codigo 200
            {
                string content = await response.Content.ReadAsStringAsync();
                var games = JsonConvert.DeserializeObject<List<GameScore>>(content);

                if (games.Count > 0)
                {
                    return games[0];
                }
                else
                {
                    return new GameScore();
                }
            }
            return new GameScore();
        }

        public async Task CreateHighScore(GameScore gameScore)
        {
            string dados = URL;
            string json = JsonConvert.SerializeObject(gameScore);
            HttpClient client = GetClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(dados, content);
        }

        public async Task UpdateHighScore(GameScore gameScore)
        {
            string dados = URL + "/" + gameScore.Id;
            string json = JsonConvert.SerializeObject(gameScore);
            HttpClient client = GetClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync(dados, content);
        }

        public async Task DeleteHighScore(int id)
        {
            string dados = URL + "/" + id.ToString();
            HttpClient client = GetClient();
            await client.DeleteAsync(dados);
        }
    }
}
