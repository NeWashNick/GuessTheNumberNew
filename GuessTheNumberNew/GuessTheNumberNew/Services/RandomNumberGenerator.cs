namespace GuessTheNumber.Services
{
    public class RandomNumberGenerator : INumberGenerator
    {
        private Random random = new Random();

        public int Generate(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}