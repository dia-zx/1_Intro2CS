using System;
using System.Collections.Generic;
using System.Text;

namespace MyUtils
{
    /// <summary>
    /// Класс, описывающий свойства и методы таблицы
    /// </summary>
    public class Table
    {
        /// <summary>
        /// Координата по горизонтали
        /// </summary>
        public int Left { get; set; }
        /// <summary>
        /// Координата по вертикали
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// Ширина
        /// </summary>
        public int Width
        {
            get
            {
                int s = 2 + Columns.Count - 1;
                foreach (Column it in Columns)
                    s += it.Width;
                return s;
            }
        }
        /// <summary>
        /// true - включено отображение активной строки
        /// </summary>
        public bool ShowActive { get; set; }
        /// <summary>
        /// Коллекция колонок
        /// </summary>
        public List<Column> Columns { get; }
        /// <summary>
        /// Коллекция строк
        /// </summary>
        public List<Row> Rows { get; }

        /// <summary>
        /// Добавляет строку в таблицу
        /// </summary>
        public void AddRow()
        {
            Row r = new Row(this);
            Rows.Add(r);
        }

        /// <summary>
        /// Добавляет строку в таблицу и заполняет ее ячейки строками из массива
        /// </summary>
        /// <param name="str">массив строк для ячеек</param>
        public void AddRow(params string[] str)
        {
            Row r = new Row(this, str);
            Rows.Add(r);
        }

        /// <summary>
        /// Форматирует все строки таблицы
        /// </summary>
        public void FormatAll()
        {
            StaticRow.Format();
            foreach (Row it in Rows)
            {
                it.Format();
            }
        }

        /// <summary>
        /// Возвращает высоту указанной строки
        /// </summary>
        /// <param name="row">номер строки таблицы</param>
        /// <returns></returns>
        private int GetRowTop(int row)
        {
            int r = 0;
            for (int i = 0; i < row; i++)
                r += Rows[i].Height;
            return r;
        }

        /// <summary>
        /// строка шапки таблицы
        /// </summary>
        public Row StaticRow { get; set; }
        /// <summary>
        /// высота таблицы
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// цвета бордюра таблицы
        /// </summary>
        public TextColors GridColors { get; set; }
        /// <summary>
        /// цвета выбранных строк таблицы
        /// </summary>
        public TextColors SellectedColors { get; set; }
        /// <summary>
        /// обычный текст в таблице
        /// </summary>
        public TextColors NormalTextColors { get; set; }
        /// <summary>
        /// цвет активной строки
        /// </summary>
        public TextColors ActiveTextColors { get; set; }
        /// <summary>
        /// цвет шапки
        /// </summary>
        public TextColors StaticTextColors { get; set; }
        /// <summary>
        /// номер активной строки
        /// </summary>
        public int ActiveRow { get; set; }
        /// <summary>
        /// список клавиш для вызова обработчика событий KeyPress
        /// </summary>
        public HashSet<ConsoleKey> Keys { get; set; }
        /// <summary>
        /// обработчик события KeyPress (срабатывание на нажатие клавиш из списка [Keys])
        /// </summary>
        public event EventHandler<TableEventArgs_KeyPress> KeyPress;
        /// <summary>
        /// обработчик события BeforeDraw (перед отрисовкой таблицы)
        /// </summary>
        public event EventHandler BeforeDraw;
        /// <summary>
        /// обработчик события AfterDraw (после отрисовкой таблицы)
        /// </summary>
        public event EventHandler AfterDraw;

        /// <summary>
        /// число выделенных строк таблицы
        /// </summary>
        public int GetSellectedCount()
        {
            int n = 0;
            foreach (var it in Rows)
            {
                if (it.Sellected) n++;
            }
            return n;
        }

        /// <summary>
        /// номер начальной строки (текстовой) с которой начинается отрисовка основного тела таблицы
        /// </summary>
        private int _TopLine;
        public int TopLine { get => _TopLine; }
        /// <summary>
        /// высота шапки таблицы
        /// </summary>
        private int _MainTableTop { get => 2 + StaticRow.Height; }
        /// <summary>
        /// высота основного тела таблицы
        /// </summary>
        private int _MainTableHeight
        {
            get
            {
                int n = Height - _MainTableTop;
                return (n < 0) ? 0 : n;
            }
        }

        /// <summary>
        /// вычисляет число текстовых строк основного поля таблицы
        /// (строка таблицы может быть высотой в несколько строк)
        /// </summary>
        /// <returns>число текстовых строк основного поля таблицы </returns>
        public int GetTotalLines()
        {
            int s = 0;
            foreach (Row it in Rows)
                s += it.Height;
            return s;
        }

        /// <summary>
        /// Вычисляет % прокрутки сверху от общего числа основного поля таблицы
        /// </summary>
        /// <returns> % прокрутки свероху</returns>
        public int GetPositionPercent()
        {
            int percent = (_TopLine + _MainTableHeight) * 100 / GetTotalLines();
            if (percent > 100) percent = 100;
            return percent;
        }

        public Table()
        {
            Rows = new List<Row>();
            Columns = new List<Column>();
            for (int i = 0; i < 3; i++)
            {
                Columns.Add(new Column(this, 15));
            }
            StaticRow = new Row(this, "Column 1", "Column 2", "Column 3");
            foreach (Cell it in StaticRow.Cells)
            {
                it.HorizAlign = Cell.EHorizAlign.Center;
                it.VertAlign = Cell.EVertAlign.Center;
            }


            Height = 30;
            GridColors = new TextColors(ConsoleColor.Blue, ConsoleColor.Black);
            SellectedColors = new TextColors(ConsoleColor.Green, ConsoleColor.Black);
            NormalTextColors = new TextColors(ConsoleColor.DarkGreen, ConsoleColor.Black);
            ActiveTextColors = new TextColors(ConsoleColor.DarkGreen, ConsoleColor.Gray);
            StaticTextColors = new TextColors(ConsoleColor.Cyan, ConsoleColor.Black);
            Keys = new HashSet<ConsoleKey>();

            ActiveRow = 0;
            Init();
            Left = 0;
            Top = 0;
            ShowActive = true;
        }

        /// <summary>
        /// сброс на начальное положение активной строки и начала отрисовки строк таблицы
        /// </summary>
        public void Init()
        {
            ActiveRow = 0;
            _TopLine = 0;
        }

        /// <summary>
        /// отрисовка таблицы на консоль
        /// </summary>
        public void Print()
        {
            Console.CursorVisible = false; //отключаем мигающий курсор
                                           
            DrawTopLine();
            #region Отрисовка заголовка таблицы
            string[] strings = new string[StaticRow.Cells.Count];
            for (int i = 0; i < StaticRow.Height; i++)
            {
                for (int j = 0; j < StaticRow.Cells.Count; j++)
                {
                    strings[j] = StaticRow.Cells[j].FormatedText[i];
                }
                DrawTextLine(strings, StaticTextColors);
            }
            DrawMiddleLine();
            #endregion
;

            #region Отрисовка основного тела таблицы
            int CurLine = 0;//номер отрисованной текстовой строки 
            for (int i = 0; i < Rows.Count; i++)
            {
                if (_TopLine > CurLine + Rows[i].Height)
                {
                    CurLine += Rows[i].Height;
                    continue;
                }


                #region Определяемся с цветом отрисовки строки таблицы
                TextColors textColors = NormalTextColors;
                if (Rows[i].Sellected)
                    textColors = SellectedColors;
                if ((i == ActiveRow) && (ShowActive))
                    textColors = ActiveTextColors;
                #endregion

                #region Пробегаемся по всем текстовым строкам в строке таблице
                for (int lineCell = 0; lineCell < Rows[i].Height; lineCell++)
                {
                    if ((CurLine >= _TopLine) && (CurLine < _TopLine + _MainTableHeight))
                    {
                        strings = new string[Rows[i].Cells.Count];
                        for (int j = 0; j < Rows[i].Cells.Count; j++)
                        {
                            strings[j] = Rows[i].Cells[j].FormatedText[lineCell];
                        }
                        DrawTextLine(strings, textColors); //отрисовываем строку если она в видимом окне...
                    }

                    CurLine++;
                }
                #endregion
                if (_TopLine + _MainTableHeight <= CurLine) break;
            }
            #endregion
            string[] TextLine = new string[Columns.Count];
            for (int i = 0; i < TextLine.Length; i++)
                TextLine[i] = new string(' ', Columns[i].Width);
            for (; CurLine - _TopLine < _MainTableHeight; CurLine++)
            {
                DrawTextLine(TextLine, NormalTextColors);

            }
            DrawBottomLine();
        }

        /// <summary>
        /// Главный метод класса. Выводит меню.
        /// </summary>
        public void Show()
        {
            do
            {
                //Console.SetCursorPosition(0, MenuTop);
                Console.CursorTop = Top;
                BeforeDraw?.Invoke(this, EventArgs.Empty); //если определен обработчик BeforeDraw... запускаем...
                Print();
                AfterDraw?.Invoke(this, EventArgs.Empty); //если определен обработчик AfterDraw... запускаем...
                if (DoInput()) return;
            } while (true);
        }

        /// <summary>
        /// Организует пользовательский ввод с клавиатуры и управление навигацией по таблице
        /// </summary>
        /// <returns>если true - выход из таблицы</returns>
        private bool DoInput()
        {
            do
            {
                ConsoleKeyInfo ch = Console.ReadKey(true);
                if (Keys.Contains(ch.Key) && (KeyPress != null))
                {// если нажатая клавиша зарегистрирована в списке [Keys] и назначен обработчик KeyPress
                    TableEventArgs_KeyPress tableEventArgs_KeyPress = new TableEventArgs_KeyPress(ch.Key);
                    KeyPress?.Invoke(this, tableEventArgs_KeyPress);
                    return (tableEventArgs_KeyPress.stop);
                }

                if ((ch.Key == ConsoleKey.UpArrow) && (ActiveRow > 0))
                {//перемещение активной строки вверх на 1
                    ActiveRow--;
                    CorrectTopLine();
                    return false;
                }
                if ((ch.Key == ConsoleKey.DownArrow) && (ActiveRow != Rows.Count - 1))
                {//перемещение активной строки ввниз на 1
                    ActiveRow++;
                    CorrectTopLine();
                    return false;
                }
                if ((ch.Key == ConsoleKey.PageUp) && (ActiveRow > 0))
                {//перемещение активной строки вверх на несколько строк
                    ActiveRow -= _MainTableHeight;
                    CorrectTopLine();
                    return false;
                }
                if ((ch.Key == ConsoleKey.PageDown) && (ActiveRow != Rows.Count - 1))
                {//перемещение активной строки ввниз на несколько строк
                    ActiveRow += _MainTableHeight;
                    CorrectTopLine();
                    return false;
                }
                if ((ch.Key == ConsoleKey.Insert) || (ch.Key == (ConsoleKey)' '))
                {//выделение строки таблицы и перемещение активной строки вниз на 1
                    Rows[ActiveRow].Sellected = !Rows[ActiveRow].Sellected;
                    ActiveRow++;
                    CorrectTopLine();
                    return false;
                }
                if ((ch.Key == ConsoleKey.Home) && (ActiveRow > 0))
                {//перемещение на начало таблицы
                    ActiveRow = 0;
                    _TopLine = 0;
                    return (false);
                }
                if ((ch.Key == ConsoleKey.End) && (ActiveRow != Rows.Count - 1))
                {//перемещение на конец таблицы
                    ActiveRow = Rows.Count - 1;
                    CorrectTopLine();
                    return (false);
                }

            } while (true);
        }

        /// <summary>
        /// корректирет положение активной строки при выходе за пределы
        /// корректирует положение начала отрисовки основного тела таблицы если 
        /// активная строка вышла из видимого диапазона..
        /// </summary>
        public void CorrectTopLine()
        {
            if (Rows.Count == 0)
            {
                ActiveRow = -1;
                return;
            }
            if (ActiveRow < 0) ActiveRow = 0;
            if (ActiveRow >= Rows.Count) ActiveRow = Rows.Count - 1;
            if (_TopLine + _MainTableHeight < GetRowTop(ActiveRow) + Rows[ActiveRow].Height)
            {
                _TopLine =
                    GetRowTop(ActiveRow) + Rows[ActiveRow].Height - _MainTableHeight;
                return;
            }
            if (_TopLine > GetRowTop(ActiveRow) + Rows[ActiveRow].Height - 1)
                _TopLine = GetRowTop(ActiveRow) + Rows[ActiveRow].Height - 1;
        }

        /// <summary>
        /// отрисовка одной текстовой строчки (строка таблицы может занимать несколько строк)
        /// с указанным цветом
        /// </summary>
        /// <param name="Text">массив строк по столбцам</param>
        /// <param name="textColors">параметры цвета</param>
        private void DrawTextLine(string[] Text, TextColors textColors)
        {
            if (Text.Length != Columns.Count)
                throw new Exception("Размерность строки не соответствует размерности таблицы DrawTextLine().");

            StringBuilder stringBuilder = new StringBuilder();
            Console.CursorLeft = Left;
            for (int i = 0; i < Columns.Count; i++)
            {//проходим по колонкам таблицы...
                GridColors.Apply();
                Console.Write("║");
                textColors.Apply();
                Console.Write(Text[i]);
            }
            GridColors.Apply();
            Console.WriteLine('║');
        }

        /// <summary>
        /// Отрисовка верхней горизонтальной рамки окна
        /// </summary>
        private void DrawTopLine()
        {
            GridColors.Apply();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("╔");
            for (int i = 0; i < Columns.Count; i++)
            {
                stringBuilder.Append('═', Columns[i].Width);
                if (i != Columns.Count - 1)
                    stringBuilder.Append('╦');
            }
            stringBuilder.Append('╗');
            Console.CursorLeft = Left;
            Console.WriteLine(stringBuilder);
        }

        /// <summary>
        /// Отрисовка нижней горизонтальной рамки окна
        /// </summary>
        private void DrawBottomLine()
        {
            GridColors.Apply();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("╚");
            for (int i = 0; i < Columns.Count; i++)
            {
                stringBuilder.Append('═', Columns[i].Width);
                if (i != Columns.Count - 1)
                    stringBuilder.Append('╩');
            }
            stringBuilder.Append('╝');
            Console.CursorLeft = Left;
            Console.WriteLine(stringBuilder);
        }

        /// <summary>
        /// Отрисовка средней горизонтальной рамки окна
        /// </summary>
        private void DrawMiddleLine()
        {
            GridColors.Apply();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("╠");
            for (int i = 0; i < Columns.Count; i++)
            {
                stringBuilder.Append('═', Columns[i].Width);
                if (i != Columns.Count - 1)
                    stringBuilder.Append('╬');
            }
            stringBuilder.Append('╣');
            Console.CursorLeft = Left;
            Console.WriteLine(stringBuilder);
        }

        /// <summary>
        /// класс для передачи аргументов для события KeyPress
        /// </summary>
        public class TableEventArgs_KeyPress : EventArgs
        {
            /// <summary>
            /// код клавиши
            /// </summary>
            public ConsoleKey Key;
            /// <summary>
            /// true - выход из таблицы
            /// </summary>
            public bool stop;
            public TableEventArgs_KeyPress(ConsoleKey Key)
            {
                this.Key = Key;
                this.stop = false;
            }
        }
    }
}
