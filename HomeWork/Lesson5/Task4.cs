using System;
using System.IO;


namespace Lesson5
{
    class Task4 : InterfaceTask
    {
        public string ShortDescription => "Сохранить дерево каталогов и файлов в текстовый файл.";

        public void Execute()
        {
            Console.WriteLine("\t\t\t\tЗадача 4.");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("* Сохранить дерево каталогов и файлов по заданному пути в текстовый файл - с        *");
            Console.WriteLine("* рекурсией и без.                                                                  *");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("Решение:\n");

            Console.WriteLine("Введите путь для построения дерева (пустая строка - текущий каталог)");
            string path = Console.ReadLine();
            if (path == "")
                path = AppDomain.CurrentDomain.BaseDirectory;

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                Console.WriteLine("Такой директории не существует!");
                Console.WriteLine("\n\nНажмите любую клавишу.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Дерево будет записано в файл tree.txt.");
            Console.WriteLine($"\t ({AppDomain.CurrentDomain.BaseDirectory}tree.txt)");

            TextWriter StansartConsoleOut = Console.Out; //Сохраним стандартный поток вывода Console.Out
            using (FileStream fileStream = new FileStream("tree.txt", FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    Console.SetOut(streamWriter); //Подменим стандартный поток вывода на наш (файловый)
                    PrintDir(directoryInfo, "", true);
                    Console.SetOut(StansartConsoleOut); //Возвращаем стандартный поток вывода на консоль
                }
            }
            
            PrintDir(directoryInfo, "", true);

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Рекурсивный метод вывода дерева дирректорий и файлов
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="indent">строка с отступами</param>
        /// <param name="isLast">true - объект последний в списке данного уровня</param>
        static private void PrintDir(DirectoryInfo dir, string indent, bool isLast)
        {
            #region Вывод корневой дирректории
            Console.Write(indent);
            Console.Write(isLast ? "└─" : "├─");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(dir.Name);
            Console.ResetColor();
            #endregion
            indent += (isLast) ? "  " : "│ ";
            
            DirectoryInfo[] dirs;
            FileInfo[] files;
            #region Проверка на возможность доступа к содержимому корневой дирректории
            try
            {
                dirs = dir.GetDirectories();
                files = dir.GetFiles();
            }
            catch (Exception)
            {
                return;
            };
            #endregion

            #region Вывод дирректорий
            for (int i = 0; i < dirs.Length; i++)
                PrintDir(dirs[i], indent, (i == dirs.Length - 1) && (files.Length == 0));
            #endregion

            #region Вывод файлов
            for (int i = 0; i < files.Length; i++)
            {
                Console.Write(indent);
                Console.Write((i == files.Length - 1) ? "└─" : "├─");
                Console.WriteLine(files[i].Name);
            }
            #endregion

        }
    }
}
