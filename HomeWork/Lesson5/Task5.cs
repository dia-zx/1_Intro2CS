using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace Lesson5
{
    public class Task5 : InterfaceTask
    {
        public string ShortDescription => "Список задач (ToDo-list)";
        private const string Path = "ToDo.txt"; // путь к файлу списка задач
        private List<TODO> ToDoList = new List<TODO>();

        /// <summary>
        /// Добавление новой задачи
        /// </summary>
        /// <param name="Title">Описание задачи</param>
        /// <returns>true - задача успешно добавлена</returns>
        private bool AddToDoItem(string Title)
        {
            if (Title == "") return false;
            ToDoList.Add(new TODO(Title));
            return true;
        }

        /// <summary>
        /// Удаление задачи из списка
        /// </summary>
        /// <param name="number">номер удаляемой задачи</param>
        /// <returns>true - если задача была удалена</returns>
        private bool DelToDoItem(int number)
        {
            if ((number < 0) || (number >= ToDoList.Count))
                return false;
            ToDoList.Remove(ToDoList[number]);
            return true;
        }


        /// <summary>
        /// Изменение статуса задачи на противоположный
        /// </summary>
        /// <param name="number"></param>
        /// <returns>true - если статус задачи был изменен</returns>
        private bool ChangeToDoItem(int number)
        {
            if ((number < 0) || (number >= ToDoList.Count))
                return false;
            ToDoList[number].IsDone = !ToDoList[number].IsDone;
            return true;
        }


        /// <summary>
        /// Вывод списка задач
        /// </summary>
        private void PrintToDoList()
        {
            Console.WriteLine("******************* Список задач ToDo ******************");
            Console.WriteLine(" №    Статус Дата создания   Описание");
            for (int i = 0; i < ToDoList?.Count; i++)
                Console.WriteLine($" {(i + 1).ToString().PadRight(4)}   {ToDoList[i]}");
            Console.WriteLine("********************************************************");
        }


        /// <summary>
        /// Сериализация и сохранение в файл списка задач
        /// </summary>
        private void SaveToDoList()
        {
            string serialize = JsonSerializer.Serialize<List<TODO>>(ToDoList);
            File.WriteAllText(Path, serialize);
        }


        /// <summary>
        /// Чтение файла и десериализация
        /// </summary>
        /// <returns>true - успешное выполнение</returns>
        private bool LoadToDoList()
        {
            try
            {
                string json = File.ReadAllText(Path);
                ToDoList = JsonSerializer.Deserialize<List<TODO>>(json);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Вывод заголовка с условиями задачи
        /// </summary>
        private void PrintHeader()
        {
            Console.WriteLine("\t\t\t\tЗадача 5.");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("* Список задач (ToDo-list):                                                         *");
            Console.WriteLine("*        ■ написать приложение для ввода списка задач;                              *");
            Console.WriteLine("*        ■ задачу описать классом ToDo с полями Title и IsDone;                     *");
            Console.WriteLine("*        ■ на старте, если есть файл tasks.json / xml / bin(выбрать формат),        *");
            Console.WriteLine("*            загрузить из него массив имеющихся задач и вывести их на экран;        *");
            Console.WriteLine("*        ■ если задача выполнена, вывести перед её названием строку «[x]»;          *");
            Console.WriteLine("*        ■ вывести порядковый номер для каждой задачи;                              *");
            Console.WriteLine("*        ■ при вводе пользователем порядкового номера задачи отметить задачу с этим *");
            Console.WriteLine("*            порядковым номером как выполненную;                                    *");
            Console.WriteLine("*        ■ записать актуальный массив задач в файл tasks.json/xml/bin.              *");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("Решение:\n");
            Console.WriteLine("Нажмите любую клавишу.\n\n");
            Console.ReadKey();
        }

        public void Execute()
        {
            PrintHeader();
            #region Формируем меню приложения
            List<string> MenuItems = new List<string>();
            MenuItems.Add(" - Создать новую задачу. ");
            MenuItems.Add(" - Удалить задачу. ");
            MenuItems.Add(" - Изменить статус задачи. ");
            MenuItems.Add(" - Выход. ");
            Menu menu = new Menu(MenuItems);
            #endregion
            LoadToDoList();

            do
            {
                Console.Clear();
                PrintToDoList();
                switch (menu.Show())
                {
                    case 0:         // Создать новую задачу.
                        Console.WriteLine("Введите задачу:");
                        if (AddToDoItem(Console.ReadLine()))
                            SaveToDoList();
                        break;
                    case 1:         // Удалить задачу.
                        Console.WriteLine("Укажите номер задачи для удаления:");
                        if (!int.TryParse(Console.ReadLine(), out int n)) break;
                        if (DelToDoItem(n - 1)) SaveToDoList();
                        break;
                    case 2:         // Изменить статус задачи.
                        Console.WriteLine("Укажите номер задачи для смены статуса:");
                        if (!int.TryParse(Console.ReadLine(), out int n2)) break;
                        if (ChangeToDoItem(n2 - 1)) SaveToDoList();
                        break;
                    case 3: return; // Выход
                }
            } while (true);
        }


    }


    /// <summary>
    /// Класс задачи
    /// </summary>
    public class TODO
    {
        /// <summary>
        /// Текст задачи
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Флаг выполнения задачи
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// Время создания задачи
        /// </summary>
        public DateTime Date { get; set; }

        public TODO()
        {
        }

        public TODO(string Title)
        {
            this.Title = Title;
            Date = DateTime.Now;
            IsDone = false;
        }

        /// <summary>
        /// Переопределенный метод для вывода задачи
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = IsDone ? "[x]" : "[ ]";
            str += $"   ({Date.ToString("dd.mm.yyyy")})  {Title}";
            return str;
        }
    }
}
