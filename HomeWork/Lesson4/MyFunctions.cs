using System;

namespace Lesson4
{
    /// <summary>
    /// Класс с набором полезных статических функций
    /// </summary>
    static class MyFunctions
    {
        /// <summary>
        /// Функция запрашивает ввод значения типа double с проверкой типа.
        /// </summary>
        /// <param name="Text"> текст для запроса</param>
        /// <returns></returns>
        static public double InputDouble(string Text)
        {
            do
            {
                Console.Write(Text);
                if (double.TryParse(Console.ReadLine(), out double result))
                    return result;
                Console.WriteLine("Неверное значение!");
            }
            while (true);
        }

        /// <summary>
        /// Функция запрашивает ввод значения типа int с проверкой типа.
        /// </summary>
        /// <param name="Text"> текст для запроса</param>
        /// <returns></returns>
        static public int InputInt(string Text)
        {
            do
            {
                Console.Write(Text);
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;
                Console.WriteLine("Неверное значение!");
            }
            while (true);
        }

        /// <summary>
        /// Функция запрашивает ввод значения типа int с проверкой типа и
        /// проверкой требования нахождения значения на отрезке [Min ... Max]
        /// </summary>
        /// <param name="Text"> текст для запроса</param>
        /// <param name="Min"> минимальное значение, введенного числа </param>
        /// <param name="Max"> максимальное значение, введенного числа</param>
        /// <returns></returns>
        static public int InputInt(string Text, int Min, int Max)
        {
            do
            {
                Console.Write(Text);
                if (int.TryParse(Console.ReadLine(), out int result)
                                && (result >= Min) && (result <= Max))
                    return result;
                Console.WriteLine("Неверное значение!");
            }
            while (true);
        }

        /// <summary>
        /// Функция запрашивает ввод значения типа double с проверкой типа и
        /// проверкой требования нахождения значения на отрезке [Min ... Max]
        /// </summary>
        /// <param name="Text"> текст для запроса</param>
        /// <param name="Min"> минимальное значение, введенного числа </param>
        /// <param name="Max"> максимальное значение, введенного числа</param>
        /// <returns></returns>
        static public double InputDouble(string Text, double Min, double Max)
        {
            do
            {
                Console.Write(Text);
                if (double.TryParse(Console.ReadLine(), out double result)
                                && (result >= Min) && (result <= Max))
                    return result;
                Console.WriteLine("Неверное значение!");
            }
            while (true);
        }



        /// <summary>
        /// Возвращает максимальную ширину строк текста
        /// </summary>
        /// <returns>Максимальная ширина текста</returns>
        static public int GetTextWidth(string str)
        {
            int maxLen = 0;
            string[] strLines = str.Split('\n');
            foreach (string it in strLines)
            {
                maxLen = (it.Length > maxLen) ? it.Length : maxLen;
            }
            return maxLen;
        }
    }
}