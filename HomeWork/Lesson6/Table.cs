using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson6
{
    class Table
    {
        /// <summary>
        /// строка шапки таблицы
        /// </summary>
        public TableItem StaticItems { get; set; }
        /// <summary>
        /// строки основного тела таблицы
        /// </summary>
        public List<TableItem> Items { get; set; }
        /// <summary>
        /// массив задает ширины столбцов таблицы
        /// </summary>
        public int[] ColumnWidth { get; set; }
        /// <summary>
        /// Ограничение высоты таблицы
        /// </summary>
        public int MaxHeight { get; set; }
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
        /// включение (true) /выключение (false) переноса строк
        /// </summary>
        public bool Wrap { get; set; }
        /// <summary>
        /// список клавиш для вызова обработчика событий KeyPress
        /// </summary>
        public HashSet<ConsoleKey> Keys { get; set; }
        /// <summary>
        /// обработчик события KeyPress (срабатывание на нажатие клавиш из списка [Keys])
        /// </summary>
        public event EventHandler KeyPress;
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
        public int GetSellectedCount() {
            int n=0;
            foreach(var it in Items)
            {
                if (it.Sellected) n++;
            }
            return n;
        }

        /// <summary>
        /// номер начальной строки (текстовой) с которой начинается отрисовка основного тела таблицы
        /// </summary>
        private int _TopLine;
        /// <summary>
        /// высота шапки таблицы
        /// </summary>
        private int _MainTableTop;
        /// <summary>
        /// высота основного тела таблицы
        /// </summary>
        private int _MainTableHeight;
        /// <summary>
        /// список, хранящий положение и высоту (абсолютную) каждой строки основного тела таблицы
        /// </summary>
        private List<(int Top, int Height)> _MainItemsTopHeight;


        public Table()
        {
            Items = new List<TableItem>();
            StaticItems = new TableItem(new string[] { "Column 1", "Column 2", "Column 3" });
            MaxHeight = 30;
            GridColors = new TextColors(ConsoleColor.Blue, ConsoleColor.Black);
            SellectedColors = new TextColors(ConsoleColor.Green, ConsoleColor.Black);
            NormalTextColors = new TextColors(ConsoleColor.DarkGreen, ConsoleColor.Black);
            ActiveTextColors = new TextColors(ConsoleColor.DarkGreen, ConsoleColor.Gray);
            StaticTextColors = new TextColors(ConsoleColor.Cyan, ConsoleColor.Black);
            Keys = new HashSet<ConsoleKey>();
            _MainItemsTopHeight = new List<(int Top, int Height)>();
            Wrap = true;
            ColumnWidth = new int[] { 15, 15, 15 };
            ActiveRow = 0;
            Init();
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
            int CurLine = 1; //номер отрисованной текстовой строки 

            #region Отрисовка заголовка таблицы
            string[][] strings = StaticItems.TableItemWrap(ColumnWidth);
            for (int i = 0; i < strings.GetLength(0); i++)
            {
                DrawTextLine(strings[i], StaticTextColors);
                CurLine++;
            }
            if (Items.Count != 0)
            {
                DrawMiddleLine();
                CurLine++;
            }
            #endregion
            _MainTableTop = CurLine; //фиксируем высоту шапки таблицы
            _MainTableHeight = MaxHeight - _MainTableTop; //вычисляем оставшуюся высоту на основное тело таблицы
            _MainTableHeight = (_MainTableHeight < 0) ? 0 : _MainTableHeight;

            #region Отрисовка основного тела таблицы
            _MainItemsTopHeight.Clear();
            CurLine = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                strings = Items[i].TableItemWrap(ColumnWidth);
                if (Wrap == false)
                {// перенос строк отключен. Берем только первую строку из ячеек строки
                    string[][] newstrings = new string[1][];
                    newstrings[0] = strings[0];
                    strings = newstrings;
                }

                _MainItemsTopHeight.Add((Top: CurLine, Height: strings.GetLength(0))); ;
                #region Определяемся с цветом отрисовки строки таблицы
                TextColors textColors = NormalTextColors;
                if (Items[i].Sellected)
                    textColors = SellectedColors;
                if (i == ActiveRow)
                    textColors = ActiveTextColors;
                #endregion

                #region Пробегаемся по всем текстовым строкам в строке таблице
                for (int j = 0; j < strings.GetLength(0); j++)
                {
                    if ((CurLine >= _TopLine) && (CurLine < _TopLine + _MainTableHeight))
                        DrawTextLine(strings[j], textColors); //отрисовываем строку если она в видимом окне...
                    CurLine++;
                }
                #endregion 
            }
            #endregion

            DrawBottomLine();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// Главный метод класса. Выводит меню.
        /// </summary>
        public void Show()
        {
            int MenuTop = Console.CursorTop;
            do
            {
                Console.SetCursorPosition(0, MenuTop);
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
                if ((ch.Key == ConsoleKey.DownArrow) && (ActiveRow != Items.Count - 1))
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
                if ((ch.Key == ConsoleKey.PageDown) && (ActiveRow != Items.Count - 1))
                {//перемещение активной строки ввниз на несколько строк
                    ActiveRow += _MainTableHeight;
                    CorrectTopLine();
                    return false;
                }
                if ((ch.Key == ConsoleKey.Insert) || (ch.Key == (ConsoleKey)' '))
                {//выделение строки таблицы и перемещение активной строки вниз на 1
                    Items[ActiveRow].Sellected = !Items[ActiveRow].Sellected;
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
                if ((ch.Key == ConsoleKey.End) && (ActiveRow != Items.Count - 1))
                {//перемещение на конец таблицы
                    ActiveRow = Items.Count - 1;
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
        private void CorrectTopLine()
        {
            if (ActiveRow < 0) ActiveRow = 0;
            if (ActiveRow >= Items.Count) ActiveRow = Items.Count - 1;
            if (_TopLine + _MainTableHeight <
                _MainItemsTopHeight[ActiveRow].Top + _MainItemsTopHeight[ActiveRow].Height)
            {
                _TopLine =
                    _MainItemsTopHeight[ActiveRow].Top + _MainItemsTopHeight[ActiveRow].Height
                    - _MainTableHeight;
                return;
            }
            if (_TopLine > _MainItemsTopHeight[ActiveRow].Top)
                _TopLine = _MainItemsTopHeight[ActiveRow].Top;
        }

        /// <summary>
        /// отрисовка одной текстовой строчки (строка таблицы может занимать несколько строк)
        /// с указанным цветом
        /// </summary>
        /// <param name="Text">массив строк по столбцам</param>
        /// <param name="textColors">параметры цвета</param>
        private void DrawTextLine(string[] Text, TextColors textColors)
        {
            if (Text.Length != ColumnWidth.Length)
                throw new Exception("Размерность строки не соответствует размерности таблицы DrawTextLine().");

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < ColumnWidth.Length; i++)
            {//проходим по колонкам таблицы...
                GridColors.Apply();
                Console.Write("║");
                textColors.Apply();
                stringBuilder.Clear();
                stringBuilder.Append(Text[i]);
                if (Text[i].Length <= ColumnWidth[i])
                    stringBuilder.Append(' ', ColumnWidth[i] - Text[i].Length);
                else
                    stringBuilder.Remove(ColumnWidth[i], stringBuilder.Length - ColumnWidth[i]);
                Console.Write(stringBuilder);
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
            for (int i = 0; i < ColumnWidth.Length; i++)
            {
                stringBuilder.Append('═', ColumnWidth[i]);
                if (i != ColumnWidth.Length - 1)
                    stringBuilder.Append('╦');
            }
            stringBuilder.Append('╗');
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
            for (int i = 0; i < ColumnWidth.Length; i++)
            {
                stringBuilder.Append('═', ColumnWidth[i]);
                if (i != ColumnWidth.Length - 1)
                    stringBuilder.Append('╩');
            }
            stringBuilder.Append('╝');
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
            for (int i = 0; i < ColumnWidth.Length; i++)
            {
                stringBuilder.Append('═', ColumnWidth[i]);
                if (i != ColumnWidth.Length - 1)
                    stringBuilder.Append('╬');
            }
            stringBuilder.Append('╣');
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
