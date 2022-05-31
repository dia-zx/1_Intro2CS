using System;
using System.IO;

namespace Lesson5
{
    class Task2 : InterfaceTask
    {
        public string ShortDescription => "Дописать текущее время в файл «startup.txt».";

        public void Execute()
        {
            Console.WriteLine("\t\t\t\tЗадача 2.");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("* Написать программу, которая при старте дописывает текущее время в файл            *");
            Console.WriteLine("* «startup.txt».                                                                    *");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("Решение:\n");
            File.AppendAllText("startup.txt", $"\n{DateTime.Now:T}");

            Console.WriteLine($"Данные записаны в файл \n\t {AppDomain.CurrentDomain.BaseDirectory}startup.txt.");
            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }
    }    
}
