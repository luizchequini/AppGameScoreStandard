namespace AppGameScoreStandard.Models
{
    public class GameScore
    {
        public GameScore()
        {
            this.Id = 0;
            this.Highscore = 0;
            this.Game = "";
            this.Name = "";
            this.Email = "";
            this.Phrase = "";
        }

        public int Id { get; set; }
        public int Highscore { get; set; }
        public string Game { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phrase { get; set; }
    }
}
