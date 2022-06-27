using System;
using System.Collections.Generic;
using System.Text;

namespace MyUtils
{
    /// <summary>
    /// Описывает свойства и методы отдельной ячейки в таблице
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Выравнивание текста по горизонтали
        /// </summary>
        public enum EHorizAlign
        {
            Left,
            Center,
            Right
        }
        /// <summary>
        /// Выравнивание текста по вертикали
        /// </summary>
        public enum EVertAlign
        {
            Top,
            Center,
            Bottom
        }
        /// <summary>
        /// Выравнивание текста по вертикали
        /// </summary>
        public EVertAlign VertAlign { get; set; }
        /// <summary>
        /// Выравнивание текста по горизонтали
        /// </summary>
        public EHorizAlign HorizAlign { get; set; }
        /// <summary>
        /// Текст в ячейки
        /// </summary>
        private string _text;
        /// <summary>
        /// Текст в ячейки
        /// </summary>
        public string Text { get => _text; set { _text = value; Wrap(); } }
        /// <summary>
        /// Текст ячейки, разбитый по строкам
        /// </summary>
        private List<string> _WrapText;
        /// <summary>
        /// Текст ячейки, разбитый по строкам
        /// </summary>
        public List<string> WrapText { get=>_WrapText; }
        /// <summary>
        /// Выровненный построчный текст ячейки по вертикали и горизонтали
        /// </summary>
        private List<string> _FormatedText;
        /// <summary>
        /// Выровненный построчный текст ячейки по вертикали и горизонтали
        /// </summary>
        public List<string> FormatedText { get => _FormatedText; }
        /// <summary>
        /// ссылка на родительскую строку
        /// </summary>
        private readonly Row RowOwner;
        /// <summary>
        /// ссылка на родительскую колонку
        /// </summary>
        private readonly Column ColumnOwner;

        public Cell(Row RowOwner, Column ColumnOwner)
        {
            this.RowOwner = RowOwner;
            this.ColumnOwner = ColumnOwner;
            Text = String.Empty;
                        
            VertAlign = EVertAlign.Top;
            HorizAlign = EHorizAlign.Left;
        }

        /// <summary>
        /// Разбивает текст на строки
        /// </summary>
        public void Wrap() {
            _WrapText = TransformString(_text, ColumnOwner.Width);
        }

        /// <summary>
        /// Выравнивает разбитый построчно текст по горизонтали и вертикали
        /// </summary>
        public void Format()
        {
            _FormatedText = HorizFormat(VertFormat(_WrapText));
        }

        /// <summary>
        /// Выравнивание строк коллекции по вертикали
        /// </summary>
        /// <param name="Text">исходные строки текста</param>
        /// <returns>коллекция выравненных по вертикали строк</returns>
        private List<string> VertFormat(List<string> Text) {
            List<string> res = new List<string>();
            if (Text.Count >= RowOwner.Height) { // линий текста больше высоты ячейки
                for (int i = 0; i < RowOwner.Height; i++) {
                    res.Add(WrapText[i]);
                }
                return res;
            }
            switch (VertAlign)
            {
                case EVertAlign.Top:
                    res.AddRange(Text);
                    for (int i = res.Count; i < RowOwner.Height; i++)
                        res.Add(string.Empty);
                    break;

                case EVertAlign.Center:
                    for(int i =0;i< (RowOwner.Height - Text.Count)/2;i++)
                        res.Add(string.Empty);
                    res.AddRange(Text);
                    for (int i = res.Count; i < RowOwner.Height; i++)
                        res.Add(string.Empty);
                    break;

                case EVertAlign.Bottom:
                    for (int i = 0; i < (RowOwner.Height - Text.Count); i++)
                        res.Add(string.Empty);
                    res.AddRange(Text);
                    break;
                default:
                    break;
            }
            return res;
        }

        /// <summary>
        /// Выравнивание строк коллекции по горизонтали
        /// </summary>
        /// <param name="Text">исходные строки текста</param>
        /// <returns>коллекция выравненных по горизонтали строк</returns>
        private List<string> HorizFormat(List<string> Text)
        {
            List<string> res = new List<string>();
            foreach(string it in Text)
            {
                switch (HorizAlign)
                {
                    case EHorizAlign.Left:
                        res.Add(it.PadRight(ColumnOwner.Width));
                        break;

                    case EHorizAlign.Center:
                        res.Add(it.PadLeft((ColumnOwner.Width - it.Length)/2 + it.Length).PadRight(ColumnOwner.Width));
                        break;

                    case EHorizAlign.Right:
                        res.Add(it.PadLeft(ColumnOwner.Width));
                        break;
                    default:
                        break;
                }
            }
            return res;
        }

        /// <summary>
        /// разделяет строку на подстроки ограниченные шириной [width]
        /// </summary>
        /// <param name="str">входная строка</param>
        /// <param name="width">ограничение длины строк</param>
        /// <returns>коллекция строк на которые разделилась сторока [str]</returns>
        public static List<string> TransformString(string str, int width)
        {
            List<string> stringsList = new List<string>();
            str = str.Replace("\t", "    ");
            string[] strings = str.Split('\n');
            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i].Length <= width)
                {
                    stringsList.Add(strings[i]);
                    continue;
                }

                StringBuilder stringBuilder = new StringBuilder();

                for (int j = 0; j < strings[i].Length; j++)
                {
                    stringBuilder.Append(strings[i][j]);
                    if (stringBuilder.Length == width)
                    {
                        stringsList.Add(stringBuilder.ToString());
                        stringBuilder.Clear();
                    }
                }
                if (stringBuilder.Length != 0)
                    stringsList.Add(stringBuilder.ToString());
            }
            return stringsList;
        }
    }
}
