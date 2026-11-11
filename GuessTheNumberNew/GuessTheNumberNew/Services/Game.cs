using GuessTheNumber.Models;

namespace GuessTheNumber.Services
{
    public class Game
    {
        private INumberGenerator generator;
        private GameSettings settings;
        private int secretNumber;
        private int attempts;
        private bool isFinished;

        public event Action<string> OnMessage;
        public event Action<GameResult> OnGameEnded;

        public bool IsFinished
        {
            get { return isFinished; }
        }

        public int AttemptsLeft
        {
            get { return settings.Difficulty.MaxAttempts - attempts; }
        }

        public Game(INumberGenerator generator)
        {
            this.generator = generator;
        }

        public void Start(GameSettings settings)
        {
            this.settings = settings;
            secretNumber = generator.Generate(settings.Difficulty.Min, settings.Difficulty.Max);
            attempts = 0;
            isFinished = false;

            if (OnMessage != null)
                OnMessage("Я загадал число от " + settings.Difficulty.Min + " до " + settings.Difficulty.Max + ". Удачи!");
        }

        public void MakeGuess(int guess)
        {
            if (isFinished)
            {
                if (OnMessage != null)
                    OnMessage("Игра уже закончена!");
                return;
            }

            if (guess < settings.Difficulty.Min || guess > settings.Difficulty.Max)
            {
                if (OnMessage != null)
                    OnMessage("Число должно быть от " + settings.Difficulty.Min + " до " + settings.Difficulty.Max);
                return;
            }

            attempts++;

            if (guess == secretNumber)
            {
                isFinished = true;
                if (OnMessage != null)
                    OnMessage("Правильно! Число было " + secretNumber + ". Ты угадал за " + attempts + " попыток!");

                GameResult result = new GameResult(settings.PlayerName, settings.Difficulty.Name, secretNumber, attempts, true);
                if (OnGameEnded != null)
                    OnGameEnded(result);
            }
            else if (attempts >= settings.Difficulty.MaxAttempts)
            {
                isFinished = true;
                if (OnMessage != null)
                    OnMessage("Попытки закончились. Я загадал " + secretNumber);

                GameResult result = new GameResult(settings.PlayerName, settings.Difficulty.Name, secretNumber, attempts, false);
                if (OnGameEnded != null)
                    OnGameEnded(result);
            }
            else if (guess < secretNumber)
            {
                if (OnMessage != null)
                    OnMessage("Моё число БОЛЬШЕ. Осталось попыток: " + AttemptsLeft);
            }
            else
            {
                if (OnMessage != null)
                    OnMessage("Моё число МЕНЬШЕ. Осталось попыток: " + AttemptsLeft);
            }
        }
    }
}