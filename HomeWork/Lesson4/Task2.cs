using System;

namespace Lesson4
{
    class Task2 : InterfaceTask
    {
        public string ShortDescription => "Принять набор чисел, разделенных пробелом, и возвратить их сумму.";

        public void Execute()
        {
            Console.WriteLine("\t\t\t\tЗадача 2.");
            Console.WriteLine("==========================================================================================");
            Console.WriteLine("* Написать программу, принимающую на вход строку — набор чисел, разделенных пробелом,    *");
            Console.WriteLine("* и возвращающую число — сумму всех чисел в строке. Ввести данные с клавиатуры и вывести *");
            Console.WriteLine("* результат на экран.                                                                    *");
            Console.WriteLine("==========================================================================================");
            Console.WriteLine("Решение:\n");

            Console.WriteLine("Введите набор чисел, разделенных пробелом:");
            (bool stringIsOk, int Sum) = SpliteCheckSum(Console.ReadLine());
            if (stringIsOk == false)
                Console.WriteLine("Неверный ввод!");
            else
                Console.WriteLine($"Сумма введенных чисел: {Sum}");

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Выделяет из строки числа, разделенные пробелом, проверяет на валидность int 
        /// и подсчитывает их сумму
        /// </summary>
        /// <param name="input">входная строка набора чисел, разделенных пробелами</param>
        /// <returns>(bool stringIsOk, int Sum) stringIsOk - true если числа валидны int
        /// Sum - результат суммы чисел
        /// </returns>
        static private (bool stringIsOk, int Sum) SpliteCheckSum(string input)
        {
            string[] ArrayOfStrings = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int Sum = 0;
            foreach (string str in ArrayOfStrings)
            {
                if (int.TryParse(str, out int num) == false)
                    return (false, Sum);
                Sum += num;
            }
            return (true, Sum);
        }

    }
}
