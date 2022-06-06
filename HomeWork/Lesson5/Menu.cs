using System;
using System.Collections.Generic;

namespace Lesson5
{
    class Menu
    {
        /// <summary>
        /// выбранный пункт меню
        /// </summary>
        private int SelectedMenuItem;

        /// <summary>
        /// Цвет обычного текста в меню
        /// </summary>
        public ConsoleColor TextColor { get; set; }

        /// <summary>
        /// Цвет бордюра меню
        /// </summary>
        public ConsoleColor BorderColor { get; set; }

        /// <summary>
        /// Цвет фона выбранного пункта меню
        /// </summary>
        public ConsoleColor SelectBackgroundColor { get; set; }

        /// <summary>
        /// Цвет текста выбранного пункта меню
        /// </summary>
        public ConsoleColor SelectForegroundColor { get; set; }

        /// <summary>
        /// Список для хранения пунктов меню
        /// </summary>
        private List<string> menu;

        private int _width = 0;
        /// <summary>
        /// Ширина меню
        /// </summary>
        public int Width {
            get=> _width;
            set => _width = (value < GetAutoWidth()) ? GetAutoWidth() : value;
        } 

        /// <summary>
        /// Возвращает максимальную ширину строки в списке
        /// </summary>
        /// <param name="menu">список строк меню</param>
        /// <returns>максимальная ширину строки в списке</returns>
        public int GetAutoWidth()
        {
            int maxLen = 0;
            foreach (var it in menu)
            {
                int width = MyFunctions.GetTextWidth(it);
                maxLen = (width > maxLen) ? width : maxLen;
            }
            return maxLen;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="MenuList">список с пунктами меню</param>
        public Menu(List<string> MenuList)
        {
            SelectedMenuItem = 0;
            TextColor = ConsoleColor.DarkGreen;
            BorderColor = ConsoleColor.Blue;
            SelectBackgroundColor = ConsoleColor.Black;
            SelectForegroundColor = ConsoleColor.Green;
            menu = MenuList;
            Width = GetAutoWidth();
        }

        /// <summary>
        /// Главный метод класса. Выводит меню.
        /// </summary>
        public int Show()
        {
            int CursorTop = Console.CursorTop;
            do
            {
                Console.SetCursorPosition(0, CursorTop);
                ApplyBorderColor();
                DrawTopLine(Width + 2);

                PrintMenu();

                ApplyBorderColor();
                DrawBottomLine(Width + 2);

                if (DoInput()) return SelectedMenuItem;
            } while (true);
        }

        /// <summary>
        /// Отрисовка верхней горизонтальной рамки окна
        /// </summary>
        /// <param name="width">ширина рамки</param>
        public static void DrawTopLine(int width) {
            Console.WriteLine("╔" + new string('═', width-2) + "╗");
        }

        /// <summary>
        /// Отрисовка нижней горизонтальной рамки окна
        /// </summary>
        /// <param name="width">ширина рамки</param>
        public static void DrawBottomLine(int width)
        {
            Console.WriteLine("╚" + new string('═', width - 2) + "╝");
        }

        /// <summary>
        /// Вывод списка на консоль с выравниванием и оформлением границ
        /// </summary>
        /// <param name="maxLen">требуемая длина стороки</param>
        private void PrintMenu()
        {
            for (int n = 0; n < menu.Count; n++)
            {
                string[] strlines = menu[n].Split('\n');
                foreach (string it2 in strlines)
                {
                    ApplyBorderColor();
                    Console.Write("║");

                    if (n == SelectedMenuItem)
                        ApplySelectColor();
                    else
                        ApplyTextColor();

                    Console.Write(it2.PadRight(Width));

                    ApplyBorderColor();
                    Console.Write("║\n");
                }
            }
        }

        /// <summary>
        /// Применяет цветовые настройки текста для бордюра
        /// </summary>
        private void ApplyBorderColor()
        {
            Console.ResetColor();
            Console.ForegroundColor = BorderColor;
        }

        /// <summary>
        /// Применяет цветовые настройки для вывода обычного текста
        /// </summary>
        private void ApplyTextColor()
        {
            Console.ResetColor();
            Console.ForegroundColor = TextColor;
        }

        /// <summary>
        /// Применяет цветовые настройки текста для выбранного пункта меню
        /// </summary>
        private void ApplySelectColor()
        {
            Console.ResetColor();
            Console.BackgroundColor = SelectBackgroundColor;
            Console.ForegroundColor = SelectForegroundColor;
        }

        /// <summary>
        /// Организует пользовательский ввод номера задачи
        /// </summary>
        /// <returns>если true - активация пункта меню</returns>
        private bool DoInput()
        {
            Console.ResetColor();
            Console.WriteLine("\nИспользуйте курсорные клавиши <Up>, <Down> для выбора пункта меню.");
            Console.WriteLine("Для активации используйте <Enter>.");
            do
            {
                ConsoleKeyInfo ch = Console.ReadKey(true);
                if (ch.Key == ConsoleKey.Enter)
                    return true;
                if (ch.Key == ConsoleKey.UpArrow)
                {
                    SelectedMenuItem--;
                    SelectedMenuItem = (SelectedMenuItem < 0) ? menu.Count - 1 : SelectedMenuItem;
                    return false;
                }
                if (ch.Key == ConsoleKey.DownArrow)
                {
                    SelectedMenuItem++;
                    SelectedMenuItem = (SelectedMenuItem >= menu.Count) ? 0 : SelectedMenuItem;
                    return false;
                }
            } while (true);
        }
    }
}
