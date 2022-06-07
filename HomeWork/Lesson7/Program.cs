using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson7
{
    class Program
    {

        static void Main(string[] args)
        {
            string secret = "secret";
            Console.WriteLine("Enter password:");
            string input = Console.ReadLine();
            if (input == secret)
            {
                Console.WriteLine("Welcome!");
            }
            else {
                Console.WriteLine("Incorrect password!");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода");
            Console.ReadKey();
        }

    }
}




