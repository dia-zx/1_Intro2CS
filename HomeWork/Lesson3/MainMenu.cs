using System;
using System.Collections.Generic;

namespace Lesson3
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
        /// Конструктор класса
        /// </summary>
        /// <param name="LessonNumber"> Номер домашнего задания для вывода в заголовке </param>
        /// <param name="TaskList"> Коллекция ссылок на задачи </param>
        public MainMenu(int LessonNumber, List<InterfaceTask> TaskList)
        {
            this.LessonNumber = LessonNumber; //копируем в одноименную переменную объекта класса
            this.TaskList = TaskList;
        }

        /// <summary>
        /// Главный метод класса. Выводит меню.
        /// </summary>
        public void Show()
        {
            //Вычисляем максимальную ширину меню...
            string[] menu = GetMenuLinesFromTaskList();
            int maxLen = GetMenuWidth(menu);

            do
            {
                Console.Clear();
                PrintHeader(maxLen);
                PrintMenu(menu);
                Console.WriteLine(AlignCenterString("", '*', maxLen + 2)); //нижняя граница
                if (DoInput()) return;
            } while (true);
        }

        /// <summary>
        /// Формирует массив строк меню из коллекции задач
        /// </summary>
        /// <returns>массив строк меню</returns>
        private string[] GetMenuLinesFromTaskList()
        {
            string[] menu = new string[TaskList.Count];
            for (int i = 0; i < TaskList.Count; i++) {
                menu[i] = $" {i+1} - {i+1} Задача ({TaskList[i].ShortDescription}) ";
            }
            return menu;
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
        /// Возвращает максимальную ширину строки в массиве
        /// </summary>
        /// <param name="menu">массив строк меню</param>
        /// <returns>максимальная ширину строки в массиве</returns>
        private int GetMenuWidth(string [] menu)
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
            Console.WriteLine(AlignCenterString("", '*', maxLen + 2));
            Console.WriteLine("*" + AlignCenterString("Домашнее задание №" + LessonNumber.ToString(),
                            ' ', maxLen) + "*");
            Console.WriteLine("*" + AlignCenterString("Список задач", ' ', maxLen) + "*");
            Console.WriteLine("*" + AlignCenterString("", '_', maxLen) + "*");
        }

        /// <summary>
        /// Вывод списка задач на консоль с выравниванием и оформлением границ
        /// </summary>
        /// <param name="maxLen">требуемая длина стороки</param>
        private void PrintMenu(string[] menu)
        {
            int maxLen = GetMenuWidth(menu);
            foreach(string it in menu)
            {
                string[] strlines = it.Split('\n');
                foreach(string it2 in strlines)
                {
                    string str2 = it2 + new string(' ', maxLen - it2.Length);
                    Console.WriteLine("*" + str2 + "*");
                }
            }
        }

        /// <summary>
        /// Организует пользовательский ввод номера задачи
        /// </summary>
        /// <returns>если true - выход из приложения</returns>
        private bool DoInput()
        {
            Console.WriteLine($"\nВведите число от 1 до {TaskList.Count}. " +
                                "Введите \"exit\" для выхода.");
            do
            {
                string TaskNumberString = Console.ReadLine();
                if (TaskNumberString == "exit") return true;
                if (int.TryParse(TaskNumberString, out int TaskNumber))
                {
                    if (TaskNumber > 0 && TaskNumber <= TaskList.Count)
                    {
                        Console.Clear();
                        TaskList[TaskNumber - 1].Execute();
                        return false;
                    }
                }
                Console.WriteLine("Неверное значение! Повторите ввод.");
            } while (true);

        }
    }
}