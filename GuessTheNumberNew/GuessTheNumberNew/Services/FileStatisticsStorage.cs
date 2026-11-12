using System.Text.Json;
using GuessTheNumber.Models;

namespace GuessTheNumber.Services
{
    public class FileStatisticsStorage : IStatisticsStorage
    {
        private string fileName = "stats.json";

        public Statistics Load()
        {
            if (!File.Exists(fileName))
                return new Statistics();

            try
            {
                string json = File.ReadAllText(fileName);
                Statistics stats = JsonSerializer.Deserialize<Statistics>(json);
                if (stats == null)
                    return new Statistics();
                return stats;
            }
            catch
            {
                return new Statistics();
            }
        }

        public void Save(Statistics stats)
        {
            try
            {
                string json = JsonSerializer.Serialize(stats, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка сохранения: " + ex.Message);
            }
        }
    }
}