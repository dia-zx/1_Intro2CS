using System;

namespace Lesson1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите Ваше имя: ");
            string name = Console.ReadLine();
            Console.Write($"Привет, {name}, сегодня {DateTime.Now}");
            Console.ReadLine();
        }
    }
}
