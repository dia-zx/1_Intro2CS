using System;
using System.IO;

namespace Lesson5
{
    class Task3 : InterfaceTask
    {
        public string ShortDescription => "Запись введенных чисел (0...255) в бинарный файл.";

        public void Execute()
        {
            Console.WriteLine("\t\t\t\tЗадача 3.");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("* Ввести с клавиатуры произвольный набор чисел (0...255) и записать их в бинарный   *");
            Console.WriteLine("* файл.                                                                             *");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("Решение:\n");

            Console.WriteLine("Введите произвольный набор чисел (0...255), разделенных пробелами.");
            string input = Console.ReadLine();
            string [] inputMas = input.Split(new string [] { " " }, StringSplitOptions.RemoveEmptyEntries);
            byte[] byteMas = new byte[inputMas.Length];
            for(int i=0;i< inputMas.Length; i++)
            {
                byteMas[i] = byte.Parse(inputMas[i]);
            }
            File.WriteAllBytes("Task3.bin", byteMas);

            Console.WriteLine($"Данные ({byteMas.Length} байт) записаны в файл Task3.bin.");
            Console.WriteLine($"\t ({AppDomain.CurrentDomain.BaseDirectory}Task3.bin)");
            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
