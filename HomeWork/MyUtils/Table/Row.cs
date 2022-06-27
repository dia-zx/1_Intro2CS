using System;
using System.Collections.Generic;

namespace MyUtils
{
    /// <summary>
    /// Класс, описывающий свойства и методы строки таблицы
    /// </summary>
    public class Row
    {
        /// <summary>
        /// Ссылка на родительскую таблицу
        /// </summary>
        private Table Owner;
        /// <summary>
        /// Коллекция ячеек строки
        /// </summary>
        public List<Cell> Cells { get; set; }
        /// <summary>
        /// Высота строки
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// true - текущая строка является выделенной
        /// </summary>
        public bool Sellected { get;  set; }
        /// <summary>
        /// true - автоматическое выравнивание по высоте
        /// </summary>
        public bool autoheight { get; set; }
        /// <summary>
        /// Определение высоты строки по максимальной высоте ячейки
        /// </summary>
        private void CalcHeight()
        {
            int height = 0;
            foreach(Cell it in Cells)
            {
                it.Wrap();
                if(height<it.WrapText.Count)
                    height = it.WrapText.Count;
            }
            Height = height;
        }

        /// <summary>
        /// разбиение текста по строкам и выравнивание
        /// </summary>
        public void Format() {
            if (autoheight) CalcHeight();
            foreach(Cell it in Cells)
            {
                it.Wrap();
                it.Format();
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Owner">родительская таблица</param>
        public Row(Table Owner)
        {
            Height = 1;
            autoheight = true;
            Sellected = false;
            this.Owner = Owner;
            Cells = new List<Cell>();
            for(int i = 0; i < Owner.Columns.Count; i++)
            {
                Cells.Add(new Cell(this, Owner.Columns[i]));
            }
        }

        /// <summary>
        /// Создает строку и заполняет коллекцию ячеек в ней
        /// текстом из массива строк
        /// </summary>
        /// <param name="Owner">родительская таблица</param>
        /// <param name="str">набор строк</param>
        public Row(Table Owner, params string[] str)
        {
            Height = 1;
            autoheight = true;
            Sellected = false;
            if (Owner.Columns.Count > str.Length)
                throw new Exception("Row(Table Owner, params string[] str). Количество элементов string превышает число столбцов.");
            this.Owner = Owner;
            Cells = new List<Cell>();
            for (int i = 0; i < Owner.Columns.Count; i++)
                Cells.Add(new Cell(this, Owner.Columns[i]));
            for(int i = 0; i < str.Length; i++)
                Cells[i].Text = str[i];
        }
    }
}
