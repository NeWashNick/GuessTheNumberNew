namespace GuessTheNumber.Models
{
    public class GameResult
    {
        public string PlayerName;
        public string DifficultyName;
        public int SecretNumber;
        public int Attempts;
        public bool IsWin;
        public DateTime Date;

        public GameResult(string playerName, string difficultyName, int secretNumber, int attempts, bool isWin)
        {
            PlayerName = playerName;
            DifficultyName = difficultyName;
            SecretNumber = secretNumber;
            Attempts = attempts;
            IsWin = isWin;
            Date = DateTime.Now;
        }
    }
}