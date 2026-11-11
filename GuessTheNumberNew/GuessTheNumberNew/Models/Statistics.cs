namespace GuessTheNumber.Models
{
    public class Statistics
    {
        public int TotalGames;
        public int Wins;
        public int Losses;
        public int BestAttempts = int.MaxValue;
        public string BestDifficulty = "-";

        public double GetWinPercent()
        {
            if (TotalGames == 0)
                return 0;
            return (double)Wins / TotalGames * 100;
        }

        public void AddResult(GameResult result)
        {
            TotalGames++;

            if (result.IsWin)
            {
                Wins++;
                if (result.Attempts < BestAttempts)
                {
                    BestAttempts = result.Attempts;
                    BestDifficulty = result.DifficultyName;
                }
            }
            else
            {
                Losses++;
            }
        }
    }
}