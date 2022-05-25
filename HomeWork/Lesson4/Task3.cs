using System;

namespace Lesson4
{
    class Task3 : InterfaceTask
    {
        public string ShortDescription => "Метод по определению времени года.";

        public void Execute()
        {
            Console.WriteLine("\t\t\t\tЗадача 3.");
            Console.WriteLine("=========================================================================================");
            Console.WriteLine("* Написать метод по определению времени года. На вход подаётся число – порядковый номер *");
            Console.WriteLine("* месяца.На выходе — значение из перечисления(enum) — Winter, Spring, Summer,           *");
            Console.WriteLine("* Autumn.Написать метод, принимающий на вход значение из этого перечисления и           *");
            Console.WriteLine("* возвращающий название времени года(зима, весна, лето, осень). Используя эти методы,   *");
            Console.WriteLine("* ввести с клавиатуры номер месяца и вывести название времени года.Если введено         *");
            Console.WriteLine("* некорректное число, вывести в консоль текст «Ошибка: введите число от 1 до 12».       *");
            Console.WriteLine("=========================================================================================");
            Console.WriteLine("Решение:\n");

            int month = MyFunctions.InputInt("Введите порядковый номер месяца [1...12]: ", 1, 12);
            Seasons season = GetSeasonFromMonth(month);
            string seasonStr = GetStringOfSeason(season);
            Console.WriteLine($"Время года: {seasonStr}");

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }

        enum Seasons
        {
            Winter,
            Spring,
            Summer,
            Autumn
        }

        /// <summary>
        /// Преобразует номер месяца в сезон (enum Seasons)
        /// </summary>
        /// <param name="month">номер месяца [1...12]</param>
        /// <returns>сезон (enum Seasons)</returns>
        static private Seasons GetSeasonFromMonth(int month)
        {
            return (Seasons)(((month) % 12) / 3);
        }

        /// <summary>
        /// Возвращает название сезона (string)
        /// </summary>
        /// <param name="season">сезон (enum Seasons)</param>
        /// <returns>название сезона (string)</returns>
        static private string GetStringOfSeason(Seasons season)
        {
            switch (season)
            {
                case Seasons.Winter:
                    return "Winter";
                case Seasons.Spring:
                    return "Spring";
                case Seasons.Summer:
                    return "Summer";
                case Seasons.Autumn:
                    return "Autumn";
                default:
                    throw new Exception("Неверный номер сезона!");
            }
        }
    }
}
