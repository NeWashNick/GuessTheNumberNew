namespace GuessTheNumber.Models
{
    public class GameSettings
    {
        public string PlayerName = "Игрок";
        public Difficulty Difficulty;

        public GameSettings(Difficulty difficulty)
        {
            Difficulty = difficulty;
        }
    }
}