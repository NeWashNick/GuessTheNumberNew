namespace GuessTheNumber.Models
{
    public class Difficulty
    {
        public string Name;
        public int Min;
        public int Max;
        public int MaxAttempts;

        public Difficulty(string name, int min, int max, int maxAttempts)
        {
            Name = name;
            Min = min;
            Max = max;
            MaxAttempts = maxAttempts;
        }

        public override string ToString()
        {
            return Name + " (" + Min + "-" + Max + ", попыток: " + MaxAttempts + ")";
        }
    }
}