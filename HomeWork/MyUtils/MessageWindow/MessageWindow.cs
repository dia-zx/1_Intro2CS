using MyUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson9
{
    /// <summary>
    /// Выводит окно с текстовым сообщением и строкой ввода
    /// </summary>
    public class MessageWindow
    {
        /// <summary>
        /// Координата X окна
        /// </summary>
        public int Left { get; set; }
        /// <summary>
        /// Координата Y окна
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// Высота окна
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Ширина окна
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Цвета бордюра 
        /// </summary>
        public TextColors GridColors { get; set; }
        /// <summary>
        /// Цвета обычного текста
        /// </summary>
        public TextColors NormalTextColors { get; set; }

        public MessageWindow()
        {
            GridColors = new TextColors(ConsoleColor.Yellow, ConsoleColor.DarkBlue);
            NormalTextColors = new TextColors(ConsoleColor.Green, ConsoleColor.DarkBlue);
            Left = 0;
            Top = 0;
            Width = 40;
            Height = 10;
        }

        /// <summary>
        /// Вывод окна на консоль с вводом строки пользователя
        /// </summary>
        /// <param name="Title">заголовок окна</param>
        /// <param name="Text">текст в окне</param>
        /// <param name="TextInputLengthMax">ограничение на кол-во введенных символов пользователем; 0 - ожидание любой клавиши</param>
        /// <returns></returns>
        public string Show(string Title, string Text, int TextInputLengthMax)
        {
            #region Отрисовка пустой рамки окна
            DrawTopLine(Title);
            for (int i = 0; i < Height - 2; i++)
                DrawEmptyLine();
            DrawBottomLine();
            #endregion

            #region Отрисовка текста
            List<string> textStrings = Cell.TransformString(Text, Width - 2);
            NormalTextColors.Apply();
            Console.CursorTop = Top + 2;
            for (int i = 0; i < textStrings.Count; i++)
            {
                Console.CursorLeft = Left + 1;
                Console.WriteLine(textStrings[i]);
            }
            #endregion

            if (TextInputLengthMax == 0)
            {// Просто ожидаем любую клавишу...
                Console.ReadKey();
                Console.ResetColor();
                return "";
            }
            Console.CursorLeft = Left + (Width - TextInputLengthMax) / 2; //установим строку ввода по центру
            string input = StringInput.Exercute(TextInputLengthMax);
            Console.ResetColor();
            return input;
        }

        /// <summary>
        /// Отрисовка верхней линии окна с заголовком
        /// </summary>
        /// <param name="Title">заголовок окна</param>
        private void DrawTopLine(string Title)
        {
            if (Title.Length > Width - 2)
                Title = Title.Remove(Width - 2);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("╔");
            stringBuilder.Append(new string('═', (Width - 2 - Title.Length) / 2));
            stringBuilder.Append(Title);
            stringBuilder.Append(new string('═', Width - 1 - stringBuilder.Length));
            stringBuilder.Append('╗');
            GridColors.Apply();
            Console.CursorLeft = Left;
            Console.CursorTop = Top;
            Console.WriteLine(stringBuilder);
        }

        /// <summary>
        /// Отрисовка средней линии окна |......|
        /// </summary>
        private void DrawEmptyLine()
        {
            Console.CursorLeft = Left;
            GridColors.Apply(); Console.Write("║");
            NormalTextColors.Apply();
            Console.Write(new string(' ', Width - 2));
            GridColors.Apply(); Console.WriteLine("║");
        }

        /// <summary>
        /// Отрисовка нижней рамки окна
        /// </summary>
        private void DrawBottomLine()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("╚");
            stringBuilder.Append(new string('═', Width - 2));
            stringBuilder.Append('╝');
            GridColors.Apply();
            Console.CursorLeft = Left;
            Console.WriteLine(stringBuilder);
        }

        /// <summary>
        /// форматирует строку по центру указанного размера (добавлением пробелов)
        /// </summary>
        /// <param name="str">исходная строка</param>
        /// <param name="width">заданная ширина</param>
        /// <returns></returns>
        public static string FormatCenterString(string str, int width)
        {
            if (str.Length <= width)
            {
                StringBuilder stringBuilder = new StringBuilder(new string(' ', (width - str.Length) / 2));
                stringBuilder.Append(str);
                stringBuilder.Append(new string(' ', width - stringBuilder.Length));
                return stringBuilder.ToString();
            }
            return str;
        }
    }
}
