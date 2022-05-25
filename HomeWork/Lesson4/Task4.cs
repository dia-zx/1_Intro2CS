using System;

namespace Lesson4
{
    class Task4 : InterfaceTask
    {
        public string ShortDescription => "Число Фибоначчи.";

        public void Execute()
        {
            Console.WriteLine("\t\t\t\tЗадача 4.");
            Console.WriteLine("=========================================================================================");
            Console.WriteLine("* Написать программу, вычисляющую число Фибоначчи для заданного значения                *");
            Console.WriteLine("* рекурсивным способом.                                                                 *");
            Console.WriteLine("=========================================================================================");
            Console.WriteLine("Решение:\n");

            int n = MyFunctions.InputInt("Введите номер последовательности Фибоначчи: ");
            DateTime time = DateTime.Now;
            Console.WriteLine($"Вариант 1: Число Фибоначчи = {Fibonucci(n)} Потребовалось: {DateTime.Now - time}");

            time = DateTime.Now;
            Console.WriteLine($"Вариант 2: Число Фибоначчи = {Fibonucci2(n).cur} Потребовалось: {DateTime.Now - time}");

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Первый вариант рассчета рекурсивной формулы Фибоначчи (влоб) не оптимальный...
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static long Fibonucci(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n > 0) return Fibonucci(n - 1) + Fibonucci(n - 2);
            return Fibonucci(n + 2) - Fibonucci(n + 1);
        }

        /// <summary>
        /// Второй вариант рассчета рекурсивной формулы Фибоначчи быстрый. (не делается лишняя работа)
        /// т.к. формула использует предыдущее вычисленное значение числа...
        /// </summary>
        /// <param name="n"></param>
        /// <returns>last - значение Фибоначчи на предыдущей итерации,
        /// cur - значение Фибоначчи на текущей итерации
        /// </returns>
        private static (long last, long cur) Fibonucci2(int n)
        {
            long last, cur;
            if (n == 0) return (1, 0);
            if (n > 0)
            {
                (last, cur) = Fibonucci2(n - 1);
                return (cur, last + cur);
            }
            (last, cur) = Fibonucci2(n + 1);
            return (cur, last - cur);
        }
    }
}
