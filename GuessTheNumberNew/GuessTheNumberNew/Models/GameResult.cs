namespace GuessTheNumberNew.Models
{
    public class GameSettings
    {
        public Difficulty CurrentDifficulty { get; set; }
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
        public int MaxAttempts { get; set; }
    }

}
