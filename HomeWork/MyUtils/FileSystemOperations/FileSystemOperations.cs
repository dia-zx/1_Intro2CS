using System;
using System.Collections.Generic;
using System.IO;


namespace MyUtils.FileSystemOperations
{
    /// <summary>
    /// класс объединяет операции по работе с файлами
    /// </summary>
    static public class FileSystemOperations
    {
        /// <summary>
        /// Рекурсивное копирование файлов и каталогов
        /// </summary>
        /// <param name="source">источник (файл или каталог)</param>
        /// <param name="dest">пункт назначения (каталог)</param>
        /// <returns></returns>
        public static bool FilesCopy(FileSystemInfo source, string dest)
        {
            try
            {
                if (source.FullName == dest) return false;
                if (!Directory.Exists(dest)) return false;
                if (source is FileInfo)
                {
                    if (!File.Exists(source.FullName)) return false;
                    File.Copy(source.FullName, Path.Combine(dest, source.Name), true);
                    return true;
                }
                if (!Directory.Exists(source.FullName)) return false;
                string newdest = Path.Combine(dest, source.Name);
                Directory.CreateDirectory(newdest);
                DirectoryInfo[] dirs = ((DirectoryInfo)source).GetDirectories();
                foreach (DirectoryInfo it in dirs)
                {
                    FilesCopy(it, newdest);
                }
                FileInfo[] files = ((DirectoryInfo)source).GetFiles();
                foreach (FileInfo it in files)
                {
                    FilesCopy(it, newdest);
                }
                return true;
            }catch(Exception ex)
            {
                FileSystemOperations.SaveErrorMessage(ex);
                return false;
            }
        }

        /// <summary>
        /// Рекурсивное удаление файлов и каталогов
        /// </summary>
        /// <param name="source">иыточник (файл или каталог)</param>
        /// <returns></returns>
        public static bool FilesDelete(FileSystemInfo source)
        {
            try
            {
                if (source is FileInfo)
                {
                    if (!File.Exists(source.FullName)) return false;
                    File.Delete(source.FullName);
                    return true;
                }
                if (!Directory.Exists(source.FullName)) return false;
                DirectoryInfo[] dirs = ((DirectoryInfo)source).GetDirectories();
                foreach (DirectoryInfo it in dirs)
                    FilesDelete(it);
                FileInfo[] files = ((DirectoryInfo)source).GetFiles();
                foreach (FileInfo it in files)
                    FilesDelete(it);
                Directory.Delete(source.FullName);
                return true;
            }catch(Exception ex)
            {
                FileSystemOperations.SaveErrorMessage(ex);
                return false;
            }
        }


        /// <summary>
        /// Логгирование исключений в файл "errors.log"
        /// </summary>
        /// <param name="ex">параметры исключения</param>
        public static void SaveErrorMessage(Exception ex) {
            try
            {
                File.AppendAllText("errors.log", $"{DateTime.Now} - {ex.Message}\n");
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Рекурсивный метод вывода дерева дирректорий и файлов
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="indent">строка с отступами</param>
        /// <param name="isLast">true - объект последний в списке данного уровня</param>
        static public void PrintDir(DirectoryInfo dir, string indent, bool isLast,  List<string> strings)
        {
            #region Вывод корневой дирректории
            string str;
            str = indent;//    Console.Write(indent);
            str += isLast ? "└─" : "├─";//            Console.Write(isLast ? "└─" : "├─");
            //Console.ForegroundColor = ConsoleColor.Green;
            str += dir.Name;// Console.WriteLine(dir.Name);
            strings.Add(str);
            //Console.ResetColor();
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
                PrintDir(dirs[i], indent, (i == dirs.Length - 1) && (files.Length == 0), strings);
            #endregion

            #region Вывод файлов
            for (int i = 0; i < files.Length; i++)
            {
                str = indent;
                str += (i == files.Length - 1) ? "└─" : "├─";
                str += files[i].Name;
                strings.Add(str);
            }
            #endregion
        }
    }
}
