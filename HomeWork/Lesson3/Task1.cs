using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3
{
    //Написать программу, выводящую элементы двухмерного массива по диагонали.
    class Task1 : InterfaceTask
    {
        public void Execute()
        {
            Console.WriteLine("\t\t\t\tЗадача 1.");
            Console.WriteLine("============================================================================");
            Console.WriteLine("* Написать программу, выводящую элементы двухмерного массива по диагонали. *");
            Console.WriteLine("============================================================================");
            Console.WriteLine("Решение:\n");

            int size1 = MyFunctions.InputInt("Введите число столбцов двухмерного массива [1..10]: ", 1, 10);
            int size2 = MyFunctions.InputInt("Введите число строк двухмерного массива [1..10]: ", 1, 10);
            int[,] mas = new int[size1, size2];
            FillMassive(mas);
            Console.WriteLine("\nИмеем массив:");
            PrintMassive(mas);
            Console.WriteLine("\nЭлементы массива по диагонали:");
            PrintDiagonal(mas);

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }

        public string ShortDescription { get => "Вывод двухмерного массива по диагонали."; }


        /// <summary>
        /// заполняет массив случайными числами int
        /// </summary>
        /// <param name="mas">ссылка на массив</param>
        private void FillMassive(int[,] mas)
        {
            Random rnd = new Random();
            for (int i = 0; i < mas.GetLength(0); i++)
                for (int j = 0; j < mas.GetLength(1); j++)
                    mas[i, j] = rnd.Next(-100, 100);
        }

        /// <summary>
        /// Вывод двухмерного массива на консоль
        /// </summary>
        /// <param name="mas">ссылка на массив</param>
        private void PrintMassive<T>(T[,] mas)
        {
            for (int i = 0; i < mas.GetLength(0); i++)
            {     
                for (int j = 0; j < mas.GetLength(1); j++)
                    Console.Write($"{mas[i, j]}\t");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Вывод на элементов массива по диагонали
        /// </summary>
        /// <param name="mas">ссылка на массив</param>
        private void PrintDiagonal<T>(T[,] mas)
        {
            foreach (T item in mas)
            {
                Console.Write(item);
                Console.CursorTop++;
            }
        }


    }
}

