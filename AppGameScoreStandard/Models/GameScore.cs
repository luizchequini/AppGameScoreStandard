using Newtonsoft.Json;

namespace AppGameScoreStandard.Models
{
    public class GameScore
    {
        public GameScore()
        {
            Id = 0;
            Highscore = 0;
            Game = "";
            Name = "";
            Email = "";
            Phrase = "";
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("highscore")]
        public int Highscore { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phrase")]
        public string Phrase { get; set; }
    }
}
