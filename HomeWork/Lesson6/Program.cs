using System;
using System.Diagnostics;

namespace Lesson6
{

    class Program
    {
        /// <summary>
        /// список процессов
        /// </summary>
        static Process[] processes;
        /// <summary>
        /// критерий сортировки процессов ID, Name, Date
        /// </summary>
        static Sort sortType;
        enum Sort { ID, Name, Date };

        static void Main(string[] args)
        {
            Table table = new Table();
            sortType = Sort.Name; // сортируем процессы по Имени по умолчанию
            FillTable(table, sortType);

            table.Wrap = true; //включаем перенос строк в таблице (многострочные ячейки)
            #region Регистрируем пользовательские клавиши
            table.Keys.Add(ConsoleKey.Escape); //выход
            table.Keys.Add(ConsoleKey.Delete); //уничтожение процессов
            table.Keys.Add(((ConsoleKey)'R')); // обновление списка процессов
            table.Keys.Add(ConsoleKey.F1); // сортировка по ID
            table.Keys.Add(ConsoleKey.F2); // сортировка по ProcessName
            table.Keys.Add(ConsoleKey.F3); // сортировка по времени запуска процесса
            table.Keys.Add(ConsoleKey.W);  // включение / откючение Wrap
            #endregion

            #region Регистрируем обработчики событий
            table.KeyPress += Table_KeyPress;
            table.BeforeDraw += Table_BeforeDraw;
            table.AfterDraw += Table_AfterDraw;
            #endregion

            Console.SetWindowSize(120, 40);
            table.Init();
            table.Show();

            Console.WriteLine("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }

        /// <summary>
        /// Обработчик "после отрисовки таблицы..."
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Table_AfterDraw(object sender, EventArgs e)
        {
            Console.ResetColor();
            Console.WriteLine("Используйте курсорные клавиши <Up>, <Down>, <Home>, <End>, <PageUp>, <PageDown> для навигации.");
            Console.WriteLine("Для выделения используйте клавиши <Insert> или <Пробел>.");
            Console.WriteLine("Для сортировки <F1>... <F3>. <W> - вкл/выкл режима переноса строк.");
            Console.WriteLine("Для обновления процессов нажмите <R>");
            Console.WriteLine("Для завершения указанных процессов нажмите <Delete>.");
            Console.WriteLine("Для выхода <Esc>");
        }

        /// <summary>
        /// Обработчик "перед отрисовкой таблицы"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Table_BeforeDraw(object sender, EventArgs e)
        {
            Console.WriteLine($"\t\tСписок запущенных процессов ({((Table)sender).Items.Count}):");
        }

        /// <summary>
        /// Обработчик "нажатия зарегистрированных клавиш"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Table_KeyPress(object sender, EventArgs e)
        {
            Table.TableEventArgs_KeyPress arg = (Table.TableEventArgs_KeyPress)e;
            if (arg.Key == ConsoleKey.Escape)
                arg.stop = true; //выход из таблицы
            #region Уничтожение процессов
            if (arg.Key == ConsoleKey.Delete)
            {
                Console.Write("введите [Y] для завершения процессов: ");
                string input = Console.ReadLine();
                if (input.ToUpper() != "Y") return;
                if (((Table)sender).GetSellectedCount() > 0)
                {
                    for (int i = 0; i < ((Table)sender).Items.Count; i++)
                    {
                        if (((Table)sender).Items[i].Sellected == false) continue;
                        try { processes[i].Kill(); }
                        catch (Exception) { }
                    }
                }
                else
                {
                    try { processes[((Table)sender).ActiveRow].Kill(); }
                    catch (Exception) { }
                }
                System.Threading.Thread.Sleep(200); //ждем уничтожения процессов..
            }
            #endregion

            if (arg.Key == ((ConsoleKey)'R')) { }
            if (arg.Key == ConsoleKey.F1)
                sortType = Sort.ID;
            if (arg.Key == ConsoleKey.F2)
                sortType = Sort.Name;
            if (arg.Key == ConsoleKey.F3)
                sortType = Sort.Date;
            if (arg.Key == ConsoleKey.W)
                ((Table)sender).Wrap = !((Table)sender).Wrap;

            #region обновим список задач
            FillTable(((Table)sender), sortType);
            ((Table)sender).Init();
            #endregion
            Console.Clear();
        }

        /// <summary>
        /// Получения списка процессов, их сортировка и формирование таблицы
        /// </summary>
        /// <param name="table">таблица для заполнения</param>
        /// <param name="sort">критерий сортировки списка задач</param>
        private static void FillTable(Table table, Sort sort)
        {
            processes = Process.GetProcesses();

            switch (sort)
            {
                case Sort.ID:
                    Array.Sort<Process>(processes, (x, y) => { return x.Id - y.Id; });
                    break;
                case Sort.Name:
                    Array.Sort<Process>(processes, (x, y) => { return (string.Compare(x.ProcessName, y.ProcessName)); });
                    break;
                case Sort.Date:
                    Array.Sort<Process>(processes, compDate);
                    break;
            }

            #region Заполняем таблицу
            table.Items.Clear();
            TableItem ti = new TableItem();
            table.StaticItems = ti;
            ti.StrItems = new string[] { "  Id", "   Process Name", " Start Time", "  MainWindowTitle", "Threads" };
            table.ColumnWidth = new int[] { 7, 40, 20, 20, 8 };
            for (int i = 0; i < processes.Length; i++)
            {
                ti = new TableItem();
                ti.StrItems = new string[5];
                ti.StrItems[0] = processes[i].Id.ToString();
                ti.StrItems[1] = processes[i].ProcessName;
                try
                {
                    ti.StrItems[2] = processes[i].StartTime.ToString();
                }
                catch (Exception) { ti.StrItems[2] = "  N\\A"; };
                ti.StrItems[3] = processes[i].MainWindowTitle;
                ti.StrItems[4] = processes[i].Threads.Count.ToString();
                table.Items.Add(ti);
            }
            #endregion
        }

        /// <summary>
        /// функция сравнения процессов по времени запуска
        /// некоторые процессы могут вызывать исключение при попытке чтения поля StartTime
        /// </summary>
        /// <param name="x">процесс 1</param>
        /// <param name="y">процесс 2</param>
        /// <returns></returns>
        static int compDate(Process x, Process y)
        {
            bool f1 = true;
            bool f2 = true;
            try { x.StartTime.ToString(); } catch (Exception) { f1 = false; }
            try { y.StartTime.ToString(); } catch (Exception) { f2 = false; }

            if ((f1 == false) && (f2 == false)) return 0;
            if ((f1 == true) && (f2 == false)) return 1;
            if ((f1 == false) && (f2 == true)) return -1;

            if (x.StartTime == y.StartTime) return 0;
            if (x.StartTime > y.StartTime) return 1;
            if (x.StartTime < y.StartTime) return -1;
            return 0;
        }
    }
}
