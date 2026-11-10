namespace GuessTheNumberNew.Models
{
    public class GameResult
    {
        public string PlayerName { get; set; }
        public Difficulty Difficulty { get; set; }
        public bool IsWon { get; set; }
        public int AttemptsCount { get; set; }
        public DateTime Date { get; set; }
    }
}
