using System;
using System.Collections.Generic;

namespace Lesson5
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
        /// Конструктор класса
        /// </summary>
        /// <param name="LessonNumber"> Номер домашнего задания для вывода в заголовке </param>
        /// <param name="TaskList"> Коллекция ссылок на задачи </param>
        public MainMenu(int LessonNumber, List<InterfaceTask> TaskList)
        {
            this.LessonNumber = LessonNumber; //копируем в одноименную переменную объекта класса
            this.TaskList = TaskList;
            TextColor = ConsoleColor.DarkGreen;
            BorderColor = ConsoleColor.Blue;
            SelectBackgroundColor = ConsoleColor.Black;
            SelectForegroundColor = ConsoleColor.Green;
        }

        /// <summary>
        /// Главный метод класса. Выводит меню.
        /// </summary>
        public void Show()
        {
            //Вычисляем максимальную ширину меню...
            List<string> MenuList = new List<string>();
            GetMenuLinesFromTaskList(MenuList);
            MenuList.Add($" {MenuList.Count + 1} - Выход.");

            Menu menu = new Menu(MenuList);
            menu.Width = menu.GetAutoWidth();
            menu.BorderColor = BorderColor;
            menu.SelectBackgroundColor = SelectBackgroundColor;
            menu.SelectForegroundColor = SelectForegroundColor;
            menu.TextColor = TextColor;

            Console.Clear();

            do
            {
                Console.SetCursorPosition(0, 0);
                PrintHeader(menu.Width);
                int n = menu.Show();
                if (n == MenuList.Count - 1) return;
                Console.Clear();
                TaskList[n].Execute();
                Console.Clear();
            } while (true);
        }

        /// <summary>
        /// Заполняет список строк меню из коллекции задач
        /// </summary>
        private void GetMenuLinesFromTaskList(List<string> MenuList)
        {
            for (int i = 0; i < TaskList.Count; i++)
            {
                MenuList.Add($" {i + 1} - Задача ({TaskList[i].ShortDescription}) ");
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
        /// Печать заголовка
        /// </summary>
        /// <param name="maxLen"></param>
        private void PrintHeader(int maxLen)
        {
            ApplyBorderColor();
            Menu.DrawTopLine(maxLen + 2);

            Console.Write("║");
            ApplyTextColor();
            Console.Write(AlignCenterString("Домашнее задание №" + LessonNumber.ToString(),
                            ' ', maxLen));
            ApplyBorderColor();
            Console.Write("║\n");

            Console.Write("║");
            ApplyTextColor();
            Console.Write(AlignCenterString("Список задач", ' ', maxLen));

            ApplyBorderColor();
            Console.Write("║\n");

            Menu.DrawBottomLine(maxLen + 2);
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
    }
}