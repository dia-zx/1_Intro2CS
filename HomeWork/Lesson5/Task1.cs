using System;
using System.IO;

namespace Lesson5
{
    class Task1 : InterfaceTask
    {
        public string ShortDescription => "Cохранить в текстовый файл данные, введенные с клавиатуры";

        public void Execute()
        {
            Console.WriteLine("\t\t\t\tЗадача 1.");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("* Ввести с клавиатуры произвольный набор данных и сохранить его в текстовый файл.   *");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("Решение:\n");

            Console.WriteLine($"Введите данные для записи в файл\n\t {AppDomain.CurrentDomain.BaseDirectory}Task1.txt");
            string data = Console.ReadLine();
            File.WriteAllText("Task1.txt", data);

            Console.WriteLine($"Данные записаны в файл Task1.txt.");

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
