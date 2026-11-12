using GuessTheNumber.Models;

namespace GuessTheNumber.Services
{
    public interface IStatisticsStorage
    {
        Statistics Load();
        void Save(Statistics stats);
    }
}