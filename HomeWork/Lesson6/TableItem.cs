using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson6
{
    /// <summary>
    /// Описывает строку таблицы
    /// </summary>
    class TableItem
    {
        /// <summary>
        /// содержимое строки таблицы
        /// </summary>
        public string[] StrItems { get; set; }

        /// <summary>
        /// если true - строка таблицы помечена
        /// </summary>
        public bool Sellected { get; set; }

        public TableItem()
        {
            Sellected = false;
        }

        public TableItem(string[] StrItems)
        {
            Sellected = false;
            this.StrItems = StrItems;
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

        /// <summary>
        /// разделяет строки всех колонок в строке таблицы на подстроки, 
        /// ограниченные шириной столбцов
        /// </summary>
        /// <param name="ColumnWidth">макссив ширин столбцов</param>
        /// <returns>массив со строками для каждого столбца таблицы 
        /// (одна ячейка может содержать несколько строк...)
        /// </returns>
        public string[][] TableItemWrap(int[] ColumnWidth)
        {
            if (StrItems.Length != ColumnWidth.Length)
                throw new Exception("Размерность строки не соответствует размерности таблицы TableItemsWrap().");
            List<string>[] tableItemLists = new List<string>[StrItems.Length];
            int height = 0;
            for (int i = 0; i < StrItems.Length; i++)
            {
                tableItemLists[i] = TransformString(StrItems[i], ColumnWidth[i]);
                height = (tableItemLists[i].Count > height) ?
                    tableItemLists[i].Count : height;
            }
            string[][] strings = new string[height][];
            for (int line = 0; line < height; line++)
            {
                strings[line] = new string[StrItems.Length];
                for (int column = 0; column < ColumnWidth.Length; column++)
                {
                    if (tableItemLists[column].Count <= line)
                        strings[line][column] = new string(' ', ColumnWidth[column]);
                    else
                        strings[line][column] = tableItemLists[column][line].PadRight(ColumnWidth[column], ' ');
                }

            }
            return strings;
        }
    }
}
