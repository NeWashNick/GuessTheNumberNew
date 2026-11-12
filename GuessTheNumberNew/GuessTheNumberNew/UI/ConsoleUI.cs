using GuessTheNumber.Models;
using GuessTheNumber.Services;
using System;

namespace GuessTheNumber.UI
{
    public class ConsoleUI
    {
        private Game game;
        private IStatisticsStorage storage;
        private Statistics stats;
        private GameSettings settings;

        public ConsoleUI(Game game, IStatisticsStorage storage)
        {
            this.game = game;
            this.storage = storage;
            this.stats = storage.Load();
            this.settings = new GameSettings(DifficultyService.GetDefault());
            game.OnMessage += ShowMessage;
            game.OnGameEnded += GameEnded;
        }

        public void Run()
        {
            Console.WriteLine("УГАДАЙ ЧИСЛО");
            Console.WriteLine();

            bool working = true;
            while (working)
            {
                Console.WriteLine();
                Console.WriteLine("Меню");
                Console.WriteLine("1. Играть");
                Console.WriteLine("2. Статистика");
                Console.WriteLine("3. Настройки");
                Console.WriteLine("4. Выход");

                int choice = ReadNumber("Выбери пункт: ", 1, 4);

                if (choice == 1)
                {
                    StartGame();
                }
                else if (choice == 2)
                {
                    ShowStatistics();
                }
                else if (choice == 3)
                {
                    ChangeSettings();
                }
                else
                {
                    working = false;
                    Console.WriteLine("Пока!");
                }
            }
        }

        private void StartGame()
        {
            Console.WriteLine();
            Console.WriteLine("Игрок: " + settings.PlayerName);
            Console.WriteLine("Сложность: " + settings.Difficulty.ToString());
            Console.WriteLine();

            game.Start(settings);

            while (game.IsFinished == false)
            {
                int min = settings.Difficulty.Min;
                int max = settings.Difficulty.Max;
                int guess = ReadNumber("Введи число (" + min + "-" + max + "): ", min, max);
                game.MakeGuess(guess);
            }

            Console.WriteLine();
            Console.WriteLine("Нажми Enter чтобы продолжить...");
            Console.ReadLine();
        }

        private void ShowStatistics()
        {
            Console.WriteLine();
            Console.WriteLine("Статистика");
            Console.WriteLine("Всего игр: " + stats.TotalGames);
            Console.WriteLine("Побед: " + stats.Wins);
            Console.WriteLine("Поражений: " + stats.Losses);
            Console.WriteLine("Процент побед: " + stats.GetWinPercent().ToString("F1") + "%");

            if (stats.BestAttempts != int.MaxValue)
            {
                Console.WriteLine("Лучший результат: " + stats.BestAttempts + " попыток (" + stats.BestDifficulty + ")");
            }
            else
            {
                Console.WriteLine("Лучший результат: пока нет");
            }

            Console.WriteLine();
            Console.WriteLine("Нажми Enter...");
            Console.ReadLine();
        }

        private void ChangeSettings()
        {
            Console.WriteLine();
            Console.Write("Как тебя зовут? (просто Enter если не менять): ");
            string name = Console.ReadLine();

            if (name != null && name.Trim() != "")
            {
                settings.PlayerName = name.Trim();
            }

            Console.WriteLine();
            Console.WriteLine("Выбери сложность:");

            var levels = DifficultyService.GetAll();
            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + levels[i].ToString());
            }

            int num = ReadNumber("Твой выбор: ", 1, levels.Count);
            settings.Difficulty = levels[num - 1];

            Console.WriteLine();
            Console.WriteLine("Сохранил. Игрок: " + settings.PlayerName);
            Console.WriteLine("Сложность: " + settings.Difficulty.Name);
            Console.WriteLine("Нажми Enter...");
            Console.ReadLine();
        }
        private void ShowMessage(string message)
        {
            Console.WriteLine("→ " + message);
        }
        private void GameEnded(GameResult result)
        {
            stats.AddResult(result);
            storage.Save(stats);

            Console.WriteLine();
            if (result.IsWin == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ПОБЕДА!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ПОРАЖЕНИЕ");
            }
            Console.ResetColor();
        }

        private int ReadNumber(string text, int min, int max)
        {
            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                int number;

                bool ok = int.TryParse(input, out number);
                if (ok == true && number >= min && number <= max)
                {
                    return number;
                }

                Console.WriteLine("Ошибка! Введи число от " + min + " до " + max);
            }
        }
    }
}