using MyUtils;
using MyUtils.FileSystemOperations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Lesson9
{
    /// <summary>
    /// Проект Консольного файлового менеджера.
    /// реализовано:
    /// 1 - две файловые панели (окна) с навигацией и переключением между ними
    /// 2 - файловые функции: рекурсивное копирование, рекурсивное перемещение, рекурсивное удаление, создание дирректорий 
    ///         как с отдельным файлом/дирректорией, так и с выделенной группой элементов
    /// 3 - построение дерева каталогов
    /// 4 - запуск файлов в отдельном потоке
    /// 5 - сохранение текущих настроек (settings.xml) и их загрузка при запуске приложения
    /// 6 - Сохранение лога исключений в файле (errors.log)
    /// 7 - вызов справочного окна (О программе)
    /// </summary>
    class Program
    {
        /// <summary>
        /// Ширина окна приложения
        /// </summary>
        const int ApplicationWidth = 120;
        /// <summary>
        /// Высота окна приложения
        /// </summary>
        const int ApplicationHeight = 36;
        /// <summary>
        /// Файловое окно 1
        /// </summary>
        static FileManagerWindow fileManagerWindow1;
        /// <summary>
        /// Файловое окно 2
        /// </summary>
        static FileManagerWindow fileManagerWindow2;

        static Table Window1;
        static Table Window2;
        /// <summary>
        /// Окно отображения дерева файлов
        /// </summary>
        static Table Tree;
        /// <summary>
        /// Окно сообщений
        /// </summary>
        static MessageWindow messageWindow;
        /// <summary>
        /// Показывает активно ли файловое окно 1
        /// </summary>
        static bool Window1Active;
        /// <summary>
        /// Показывает активно ли файловое окно 2
        /// </summary>
        static bool Window2Active;
        /// <summary>
        /// Класс для хранения настроек приложения
        /// </summary>
        static AppUserSettings userSettings;

        static void Main(string[] args)
        {
            Init();
            Window2Active = true;
            Window1Active = false;

            do
            {
                Console.Clear();
                if (Tree != null)
                {
                    Tree.Show();
                    Tree = null;
                    continue;
                }
                if (Window1Active)
                {
                    Window2.ShowActive = false;
                    Window2.Print();
                    Window1.ShowActive = true;
                    Window1.Show();
                    continue;
                }
                if (Window2Active)
                {
                    Window1.ShowActive = false;
                    Window1.Print();
                    Window2.ShowActive = true;
                    Window2.Show();
                }
            } while (Window1Active || Window2Active);

            #region Запись пользовательских настроек
            userSettings.Window1Path = fileManagerWindow1.CurDir;
            userSettings.Window2Path = fileManagerWindow2.CurDir;
            userSettings.Save("settings.xml");
            #endregion
        }

        /// <summary>
        /// Инициализация приложения
        /// </summary>
        private static void Init()
        {
            Console.WindowWidth = ApplicationWidth;
            Console.BufferWidth = ApplicationWidth;
            Console.WindowHeight = ApplicationHeight;
            Console.BufferHeight = ApplicationHeight;

            messageWindow = new MessageWindow();
            messageWindow.Width = 40;
            messageWindow.Height = 10;
            messageWindow.Left = (ApplicationWidth - messageWindow.Width) / 2;
            messageWindow.Top = (ApplicationHeight - messageWindow.Height) / 2;


            fileManagerWindow1 = CreateFileManagerWindow();
            Window1 = fileManagerWindow1;


            fileManagerWindow2 = CreateFileManagerWindow();
            Window2 = fileManagerWindow2;

            #region Чтение пользовательских настроек
            userSettings = AppUserSettings.Load("settings.xml");
            if (userSettings == null)
            {
                userSettings = new AppUserSettings();
            }
            #endregion

            if (Directory.Exists(userSettings.Window1Path))
                fileManagerWindow1.CurDir = userSettings.Window1Path;
            fileManagerWindow1.GridColors = userSettings.GridColors;
            fileManagerWindow1.SellectedColors = userSettings.SellectedColors;
            fileManagerWindow1.NormalTextColors = userSettings.NormalTextColors;
            fileManagerWindow1.ActiveTextColors = userSettings.ActiveTextColors;
            fileManagerWindow1.StaticTextColors = userSettings.StaticTextColors;

            if (Directory.Exists(userSettings.Window2Path))
                fileManagerWindow2.CurDir = userSettings.Window2Path;
            fileManagerWindow2.GridColors = userSettings.GridColors;
            fileManagerWindow2.SellectedColors = userSettings.SellectedColors;
            fileManagerWindow2.NormalTextColors = userSettings.NormalTextColors;
            fileManagerWindow2.ActiveTextColors = userSettings.ActiveTextColors;
            fileManagerWindow2.StaticTextColors = userSettings.StaticTextColors;

            fileManagerWindow1.Update();
            fileManagerWindow2.Update();

            Window2.Left = 60;
        }

        /// <summary>
        /// Обработчик события "После отрисовки" файлового окна
        /// </summary>
        /// <param name="sender">файловое окно</param>
        /// <param name="e"></param>
        private static void FileManagerWindow_AfterDraw(object sender, EventArgs e)
        {
            DrawPosition((Table)sender);
            Console.Title = $"FileManager      \"{((FileManagerWindow)sender).CurDir}\"";
            #region Вывод основного меню
            string[] str = { "F1 Справка", "F4 Дерево", "F5 Копирование", "F6 Перемещение", "F7 Каталог", "F8 Удаление",
                              "F10 Выход с сохранением"};
            ((FileManagerWindow)sender).NormalTextColors.Apply();
            Console.Write(" ");
            foreach (string it in str)
            {
                ((FileManagerWindow)sender).SellectedColors.Apply();
                Console.Write(it);
                ((FileManagerWindow)sender).NormalTextColors.Apply();
                Console.Write("   ");
            }
            #endregion
            #region "Подвал"
            Console.Write("\n");
            Console.WriteLine("Клавиши: <Up>, <Down>, <PageUp>, <PageDown>, <Home>, <End> - для перемещения. ");
            Console.WriteLine("         <Space>, <Insert> - для выделения.   <Tab> - переключение между панелями.     <Enter> - запуск процесса");
            #endregion
        }

        /// <summary>
        /// cСоздает файловое окно
        /// </summary>
        /// <returns></returns>
        private static FileManagerWindow CreateFileManagerWindow()
        {
            FileManagerWindow window = new FileManagerWindow();
            window.AfterDraw += FileManagerWindow_AfterDraw;
            window.KeyPress += FileManagerWindow_KeyPress;
            #region Регистрируем необходимые клавиши для работы основного меню
            window.Keys.Add(ConsoleKey.F1); // Справка
            window.Keys.Add(ConsoleKey.F4); // Дерево каталогов
            window.Keys.Add(ConsoleKey.F5); // Копирование
            window.Keys.Add(ConsoleKey.F6); // Перемещение
            window.Keys.Add(ConsoleKey.F7); // Каталог
            window.Keys.Add(ConsoleKey.F8); // Удаление
            window.Keys.Add(ConsoleKey.F10);// Выход
            window.Keys.Add(ConsoleKey.Enter);// активация
            window.Keys.Add(ConsoleKey.Tab);// переключение на другое окно
            #endregion
            return window;
        }

        /// <summary>
        /// Обработчик события нажатия на клавишу
        /// </summary>
        /// <param name="sender">файловое окн</param>
        /// <param name="e"></param>
        private static void FileManagerWindow_KeyPress(object sender, Table.TableEventArgs_KeyPress e)
        {
            FileManagerWindow window = (FileManagerWindow)sender; //текущее окно (источник события)
            FileManagerWindow window2 = (window == Window1) ? (FileManagerWindow)Window2 : (FileManagerWindow)Window1;

            switch (e.Key)
            {
                case ConsoleKey.Enter:  //запуск на исполнение
                    FileManager_Exercute(window);
                    break;
                case ConsoleKey.Tab:    //Переключение между файловыми панелями
                    e.stop = true;
                    Window1Active = !Window1Active;
                    Window2Active = !Window2Active;
                    break;
                case ConsoleKey.F1:     //Вызов справки                    
                    FileManager_Help();
                    e.stop = true;
                    break;
                case ConsoleKey.F4:     // Дерево
                    FileManager_Tree(window);
                    window.Update();
                    window2.Update();
                    e.stop = true;
                    break;
                case ConsoleKey.F5:     // Копирование файлов и каталогов
                    FileManager_Copy(window, window2);
                    window.Update();
                    window2.Update();
                    e.stop = true;
                    break;
                case ConsoleKey.F6:     // Перемещение файлов и каталогов
                    FileManager_Move(window, window2);
                    window.Update();
                    window2.Update();
                    e.stop = true;
                    break;
                case ConsoleKey.F7:    // Создание дирректории
                    FileManager_CreateDir(window);
                    window.Update();
                    window2.Update();
                    e.stop = true;
                    break;
                case ConsoleKey.F8:     // Удаление
                    FileManager_Delete(window);
                    window.Update();
                    window2.Update();
                    e.stop = true;
                    break;
                case ConsoleKey.F10:    //Выход
                    e.stop = true;
                    Window1Active = false;
                    Window2Active = false;
                    break;
            }
        }

        /// <summary>
        /// Справка. Вывод сообщения "О программе"
        /// </summary>
        private static void FileManager_Help()
        {
            MessageWindow messageWindow = new MessageWindow();
            messageWindow.Left = (ApplicationWidth - messageWindow.Width) / 2;
            messageWindow.Top = (ApplicationHeight - messageWindow.Height) / 2;
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(MessageWindow.FormatCenterString("Программа \"Файловый менеджер\"", messageWindow.Width - 2));
            stringBuilder.Append(MessageWindow.FormatCenterString("Финальный проект по курсу", messageWindow.Width - 2));
            stringBuilder.AppendLine(MessageWindow.FormatCenterString("\"Введение в C#\"", messageWindow.Width - 2));
            stringBuilder.Append(MessageWindow.FormatCenterString("Разработчик: Давыдов И.А.", messageWindow.Width - 2));
            stringBuilder.AppendLine(MessageWindow.FormatCenterString("GeekBrains 2022 г.", messageWindow.Width - 2));

            messageWindow.Show(" Справка ", stringBuilder.ToString(), 0);
        }

        /// <summary>
        /// Запуск на исполнение активного файла в окне.
        /// </summary>
        /// <param name="window">файловое окно, в котором вызван метод</param>
        private static void FileManager_Exercute(FileManagerWindow window)
        {
            try
            {
                string filename = window.GetFileSystem(window.ActiveRow).FullName;
                if (File.Exists(filename))
                    Process.Start(filename);
            }
            catch (Exception ex)
            {
                FileSystemOperations.SaveErrorMessage(ex);
            }
        }

        /// <summary>
        /// Рекурсивное копирование файлов и каталогов в окне файлового менеджера
        /// </summary>
        /// <param name="window">файловое окно, в котором вызван метод</param>
        /// <param name="window2">файловое окно, определяющее "куда копировать"</param>
        private static void FileManager_Move(FileManagerWindow window, FileManagerWindow window2)
        {
            if (window.IsDriveList) return; //блокируем перемещение из списка дисков
            if (window2.IsDriveList) return; //блокируем перемещение в список дисков
            if ((window.GetSellectedCount() == 0) && (window.ActiveRow == 0)) return; //блокировка перемещения родительского каталога
            try
            {
                if (window.GetSellectedCount() == 0) //нет выделенных элементов в файловом окне
                {
                    FileSystemInfo source = window.GetFileSystem(window.ActiveRow);
                    if (source is DirectoryInfo)
                    {
                        ((DirectoryInfo)source).MoveTo(Path.Combine(window2.CurDir, source.Name));
                    }
                    if (source is FileInfo)
                    {
                        ((FileInfo)source).MoveTo(Path.Combine(window2.CurDir, source.Name));
                    }
                }
                else
                {// есть выделенные элементы в файловом окне
                    for (int i = 1; i < window.Rows.Count; i++)
                    {
                        if (window.Rows[i].Sellected == false) continue;
                        FileSystemInfo source = window.GetFileSystem(i);
                        if (source is DirectoryInfo)
                        {
                            ((DirectoryInfo)source).MoveTo(Path.Combine(window2.CurDir, source.Name));
                        }
                        if (source is FileInfo)
                        {
                            ((FileInfo)source).MoveTo(Path.Combine(window2.CurDir, source.Name));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileSystemOperations.SaveErrorMessage(ex); //записываем исключение в лог файл
            }
        }

        /// <summary>
        /// Создание каталога
        /// </summary>
        /// <param name="window">файловое окно, в котором вызван метод</param>
        private static void FileManager_CreateDir(FileManagerWindow window)
        {
            if (window.IsDriveList) return; //блокируем создание в списке дисков
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(" Введите имя дирректории,");
            stringBuilder.Append(MessageWindow.FormatCenterString("(\"NewDir\")", messageWindow.Width - 2));
            stringBuilder.AppendLine(" или полный путь:");
            stringBuilder.Append(MessageWindow.FormatCenterString("(\"C:\\DIR1\\NewDir\")", messageWindow.Width - 2));

            string userpath = messageWindow.Show(" Создание дирректории ", stringBuilder.ToString(), messageWindow.Width - 2);
            try
            {
                if (Path.GetDirectoryName(userpath) == string.Empty) //если введено просто имя каталога (не полный путь)
                    Directory.CreateDirectory(Path.Combine(window.CurDir, userpath));
                else //если ввели полный путь каталога
                    Directory.CreateDirectory(Path.Combine(userpath));
            }
            catch (Exception ex)
            {
                FileSystemOperations.SaveErrorMessage(ex); //записываем исключение в лог файл
            }
        }

        /// <summary>
        /// рекурсивное копирование файлов и каталогов в окне файлового менеджера
        /// </summary>
        /// <param name="window">файловое окно, в котором вызван метод</param>
        /// <param name="window2">файловое окно, определяющее "куда копировать"</param>
        private static void FileManager_Copy(FileManagerWindow window, FileManagerWindow window2)
        {
            if (window.IsDriveList) return; //блокируем копирование из списка дисков
            if (window2.IsDriveList) return; //блокируем копирование в список дисков
            if ((window.GetSellectedCount() == 0) && (window.ActiveRow == 0)) return; //блокировка копирования родительского каталога
            try
            {
                if (window.GetSellectedCount() == 0)    //нет выделенных элементов в файловом окне
                    FileSystemOperations.FilesCopy(window.GetFileSystem(window.ActiveRow), window2.CurDir);
                else
                {// есть выделенные элементы в файловом окне
                    for (int i = 1; i < window.Rows.Count; i++)
                    {
                        if (window.Rows[i].Sellected == false) continue;
                        FileSystemOperations.FilesCopy(window.GetFileSystem(i), window2.CurDir);
                    }
                }
            }
            catch (Exception ex)
            {
                FileSystemOperations.SaveErrorMessage(ex); //записываем исключение в лог файл
            }
        }

        /// <summary>
        /// рекурсивное удаление файлов и каталогов в окне файлового менеджера
        /// </summary>
        /// <param name="window">файловое окно, в котором вызван метод</param>
        private static void FileManager_Delete(FileManagerWindow window)
        {
            if (window.IsDriveList) return; //блокируем удаление из списка дисков
            if ((window.GetSellectedCount() == 0) && (window.ActiveRow == 0)) return; //блокировка удаления родительского каталога
            string str;
            if (window.GetSellectedCount() <= 1)
                str = "объекта";
            else
                str = $"объектов ({window.GetSellectedCount()})";
            str = messageWindow.Show(" Внимание! ", $"\n     Подтвердите удаление {str}\n\n          \"Y(y)\" - удалить.\n", 1);

            if (str.ToUpper() != "Y") return;
            try
            {
                if (window.GetSellectedCount() == 0) //нет выделенных элементов в файловом окне
                    FileSystemOperations.FilesDelete(window.GetFileSystem(window.ActiveRow));
                else
                {// есть выделенные элементы в файловом окне
                    for (int i = 1; i < window.Rows.Count; i++)
                    {
                        if (window.Rows[i].Sellected == false) continue;
                        FileSystemOperations.FilesDelete(window.GetFileSystem(i));
                    }
                }
            }
            catch (Exception ex)
            {
                FileSystemOperations.SaveErrorMessage(ex); //записываем исключение в лог файл
            }
        }

        /// <summary>
        /// Построение и отображение дерева файлов и каталогов
        /// </summary>
        /// <param name="window">файловое окно, в котором вызван метод</param>
        private static void FileManager_Tree(FileManagerWindow window)
        {
            if (window.CurDir == string.Empty) return;
            if (!Directory.Exists(window.CurDir)) return;
            List<string> strings = new List<string>();
            try
            {
                FileSystemOperations.PrintDir(new DirectoryInfo(window.CurDir), "", true, strings);
            }
            catch (Exception ex)
            {
                FileSystemOperations.SaveErrorMessage(ex);  //записываем исключение в лог файл
            }
            #region создаем таблицу с деревом
            Tree = new Table();
            Tree.GridColors = userSettings.GridColors;
            Tree.SellectedColors = userSettings.SellectedColors;
            Tree.NormalTextColors = userSettings.NormalTextColors;
            Tree.ActiveTextColors = userSettings.ActiveTextColors;
            Tree.StaticTextColors = userSettings.StaticTextColors;
            Tree.Columns.Clear();
            Tree.Rows.Clear();

            Tree.Columns.Add(new Column(Tree, ApplicationWidth - 3));
            Tree.StaticRow = new Row(Tree, window.CurDir);

            foreach (string it in strings)
                Tree.AddRow(it);
            Tree.FormatAll();
            Tree.AfterDraw += Tree_AfterDraw;
            Tree.KeyPress += Tree_KeyPress;
            Tree.Keys.Add(ConsoleKey.F4); // Возврат
            #endregion
        }

        /// <summary>
        /// обработчик нажатий клавиш в окне дерева
        /// </summary>
        /// <param name="sender">окно, вызвавшее обработчик</param>
        /// <param name="e">переданный параметр (код клавиши)</param>
        private static void Tree_KeyPress(object sender, Table.TableEventArgs_KeyPress e)
        {
            if (e.Key == ConsoleKey.F4)
            {//выход из дерева
                fileManagerWindow1.Update();
                fileManagerWindow2.Update();
                e.stop = true;
            }
        }

        /// <summary>
        /// обработчик события "после отрисовки окна"
        /// </summary>
        /// <param name="sender">окно, вызвавшее обработчик</param>
        /// <param name="e"></param>
        private static void Tree_AfterDraw(object sender, EventArgs e)
        {
            DrawPosition((Table)sender);

            Console.Write(" ");
            ((Table)sender).SellectedColors.Apply();
            Console.Write("F4 Возврат");
        }

        /// <summary>
        /// отображение общего числа элементов и числа выделенных,
        /// также отображает положение видимой части строк в % от общего числа
        /// </summary>
        /// <param name="table">окно таблицы</param>
        public static void DrawPosition(Table table)
        {
            table.NormalTextColors.Apply();
            Console.CursorLeft = table.Left;
            Console.Write(new string(' ', table.Width));
            string str = $"выбрано элементов ({table.GetSellectedCount()} из {table.Rows.Count})   Положение {table.GetPositionPercent()}%";
            str = str.PadLeft(table.Width);
            table.GridColors.Apply();
            Console.CursorLeft = table.Left;
            Console.WriteLine(str);
        }
    }
}
