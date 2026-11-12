using GuessTheNumber.Models;
using System.Collections.Generic;

namespace GuessTheNumber.Services
{
    public static class DifficultyService
    {
        public static List<Difficulty> GetAll()
        {
            List<Difficulty> list = new List<Difficulty>();
            list.Add(new Difficulty("Лёгкий", 1, 50, 10));
            list.Add(new Difficulty("Средний", 1, 100, 8));
            list.Add(new Difficulty("Сложный", 1, 500, 10));
            list.Add(new Difficulty("Эксперт", 1, 1000, 12));
            return list;
        }

        public static Difficulty GetDefault()
        {
            return GetAll()[1];
        }
    }
}