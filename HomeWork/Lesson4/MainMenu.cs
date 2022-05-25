using System;
using System.Collections.Generic;

namespace Lesson4
{
    class MainMenu
    {
        /// <summary>
        /// Коллекция ссылок на задачи
        /// </summary>
        private readonly List<InterfaceTask> TaskList;

        /// <summary>
        /// Номер домашнего задания для вывода в заголовке
        /// </summary>
        private readonly int LessonNumber;

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
        /// Символ для отрисовки бордюра
        /// </summary>
        public Char BorderChar { get; set; }

        /// <summary>
        /// Список для хранения пунктов меню
        /// </summary>
        private List<string> menu;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="LessonNumber"> Номер домашнего задания для вывода в заголовке </param>
        /// <param name="TaskList"> Коллекция ссылок на задачи </param>
        public MainMenu(int LessonNumber, List<InterfaceTask> TaskList)
        {
            this.LessonNumber = LessonNumber; //копируем в одноименную переменную объекта класса
            this.TaskList = TaskList;
            SelectedMenuItem = 0;
            TextColor = ConsoleColor.DarkGreen;
            BorderColor = ConsoleColor.Blue;
            SelectBackgroundColor = ConsoleColor.Black;
            SelectForegroundColor = ConsoleColor.Green;
            BorderChar = '*';
            menu = new List<string>();
        }

        /// <summary>
        /// Главный метод класса. Выводит меню.
        /// </summary>
        public void Show()
        {
            //Вычисляем максимальную ширину меню...
            GetMenuLinesFromTaskList();
            menu.Add($" {menu.Count + 1} - Выход.");
            int maxLen = GetMenuWidth(menu);
            Console.Clear();

            do
            {
                Console.SetCursorPosition(0, 0);
                PrintHeader(maxLen);
                PrintMenu(menu);
                Console.WriteLine(AlignCenterString("", BorderChar, maxLen + 2)); //нижняя граница
                if (DoInput()) return;
            } while (true);
        }

        /// <summary>
        /// Заполняет список строк меню из коллекции задач
        /// </summary>
        private void GetMenuLinesFromTaskList()
        {
            for (int i = 0; i < TaskList.Count; i++)
            {
                menu.Add($" {i + 1} - Задача ({TaskList[i].ShortDescription}) ");
            }
        }

        /// <summary>
        /// Создает строку размером [TotalLength] помещая [Text] в центр и заполняя
        /// свободное пространство символом [FillLetter]
        /// </summary>
        /// <param name="Text">Текст, размещаемый по центру</param>
        /// <param name="FillLetter">Символ для заполнения свободного пространства</param>
        /// <param name="TotalLength">Требуемая длина результирующей строки</param>
        /// <returns>строка с текстом, выровненным по-центру</returns>
        private string AlignCenterString(string Text, Char FillLetter, int TotalLength)
        {
            string result = new string(FillLetter, (TotalLength - Text.Length) / 2) + Text;
            result += new string(FillLetter, TotalLength - result.Length);
            return result;
        }

        /// <summary>
        /// Возвращает максимальную ширину строки в списке
        /// </summary>
        /// <param name="menu">список строк меню</param>
        /// <returns>максимальная ширину строки в списке</returns>
        private int GetMenuWidth(List<string> menu)
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
        /// Печать заголовка
        /// </summary>
        /// <param name="maxLen"></param>
        private void PrintHeader(int maxLen)
        {
            ApplyBorderColor();
            Console.WriteLine(AlignCenterString("", BorderChar, maxLen + 2));
            Console.Write(BorderChar);
            ApplyTextColor();
            Console.Write(AlignCenterString("Домашнее задание №" + LessonNumber.ToString(),
                            ' ', maxLen));
            ApplyBorderColor();
            Console.Write($"{BorderChar} \n");

            Console.Write(BorderChar);
            ApplyTextColor();
            Console.Write(AlignCenterString("Список задач", ' ', maxLen));

            ApplyBorderColor();
            Console.Write($"{BorderChar} \n");

            Console.WriteLine(AlignCenterString("", BorderChar, maxLen + 2));
        }

        /// <summary>
        /// Вывод списка задач на консоль с выравниванием и оформлением границ
        /// </summary>
        /// <param name="maxLen">требуемая длина стороки</param>
        private void PrintMenu(List<string> menu)
        {
            int maxLen = GetMenuWidth(menu);
            for (int n = 0; n < menu.Count; n++)
            {
                string[] strlines = menu[n].Split('\n');
                foreach (string it2 in strlines)
                {

                    ApplyBorderColor();
                    Console.Write(BorderChar);

                    if (n == SelectedMenuItem)
                        ApplySelectColor();
                    else
                        ApplyTextColor();

                    Console.Write(it2.PadRight(maxLen));

                    ApplyBorderColor();
                    Console.Write($"{BorderChar} \n");
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
        /// <returns>если true - выход из приложения</returns>
        private bool DoInput()
        {
            Console.ResetColor();
            Console.WriteLine("\nИспользуйте курсорные клавиши <Up>, <Down> для выбора пункта меню.");
            Console.WriteLine("Для активации используйте <Enter>.");
            do
            {
                ConsoleKeyInfo ch = Console.ReadKey(true);
                if (ch.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    if (SelectedMenuItem == menu.Count - 1)
                        return true;
                    TaskList[SelectedMenuItem].Execute();
                    Console.ResetColor();
                    Console.Clear();
                    return false;
                }
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