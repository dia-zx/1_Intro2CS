using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3
{
    class Task3 : InterfaceTask
    {
        public string ShortDescription => "Вывод строки в обратном порядке.";

        public void Execute()
        {
            Console.WriteLine("\t\t\t\tЗадача 3.");
            Console.WriteLine("============================================================================");
            Console.WriteLine("* Написать программу, выводящую введённую пользователем строку в           *\n" +
                              "* обратном порядке (olleH вместо Hello).                                   *");
            Console.WriteLine("============================================================================");
            Console.WriteLine("Решение:\n");
            Console.WriteLine("Введите строку: ");
            string str = Console.ReadLine();
            Console.WriteLine("Результат:\n");
            for(int i=str.Length-1; i>=0; i--)
                Console.Write(str[i]);

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
