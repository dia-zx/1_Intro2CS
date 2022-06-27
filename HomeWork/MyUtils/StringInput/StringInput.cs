using System;
using System.Text;

namespace MyUtils
{
    /// <summary>
    /// Реализует пользовательский ввод строки
    /// </summary>
    public class StringInput
    {
        /// <summary>
        /// Реализует пользовательский ввод строки, ограниченной по длине
        /// </summary>
        /// <param name="length">максимальная длина строки ввода</param>
        /// <returns>введенная строка пользователем</returns>
        public static string Exercute(int length)
        {
            int Left = Console.CursorLeft;
            int inputpos = 0;
            Console.CursorVisible = true; //отключаем мигающий курсор
            StringBuilder stringBuilder = new StringBuilder();
            do
            {
                ConsoleKeyInfo KeyInfo = Console.ReadKey(true);

                switch (KeyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        if (inputpos == 0) continue;
                        stringBuilder.Remove(inputpos - 1, 1);
                        inputpos--;
                        break;
                    case ConsoleKey.Enter:
                        return stringBuilder.ToString();
                    case ConsoleKey.Delete:
                        if (inputpos >= stringBuilder.Length) continue;
                        stringBuilder.Remove(inputpos, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (inputpos > 0) inputpos--;
                        else continue;
                        break;
                    case ConsoleKey.RightArrow:
                        if (inputpos < stringBuilder.Length) inputpos++;
                        else continue;
                        break;
                    case ConsoleKey.Home:
                        inputpos = 0;
                        break;
                    case ConsoleKey.End:
                        inputpos = stringBuilder.Length;
                        break;
                    case ConsoleKey.Escape:
                        return string.Empty;

                }
                                
                char ch = KeyInfo.KeyChar;
                if ((ch >= ' ') && (stringBuilder.Length < length))
                {
                    stringBuilder.Insert(inputpos, ch);
                    inputpos++;
                }

                Console.CursorVisible = false; //отключаем мигающий курсор
                Console.CursorLeft = Left;
                if (stringBuilder.Length <= length)
                    Console.Write(stringBuilder + new string(' ', length - stringBuilder.Length));
                else
                    Console.Write(stringBuilder);
                Console.CursorLeft = Left + inputpos;
                Console.CursorVisible = true; //включаем мигающий курсор
            } while (true);
        }
    }
}
