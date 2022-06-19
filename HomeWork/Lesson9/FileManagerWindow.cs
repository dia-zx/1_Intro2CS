using MyUtils;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lesson9
{
    /// <summary>
    /// Файловое окно на базе таблицы
    /// </summary>
    class FileManagerWindow : Table
    {
        /// <summary>
        /// Ширина окна
        /// </summary>
        public new int Width { get; set; }
        /// <summary>
        /// Высота окна
        /// </summary>
        public int Heigh { get; set; }
        /// <summary>
        /// Текущая дирректория
        /// </summary>
        public string CurDir { get; set; }
        /// <summary>
        /// true - если дирректория корневая
        /// </summary>
        private bool _isRoot;
        /// <summary>
        /// true - если дирректория корневая
        /// </summary>
        public bool IsRoot { get => _isRoot; }
        /// <summary>
        /// true - если сейчас список дисков а не файлов и дирректорий
        /// </summary>
        private bool _isDriveList;
        /// <summary>
        /// true - если сейчас список дисков а не файлов и дирректорий
        /// </summary>
        public bool IsDriveList { get => _isDriveList; }
        /// <summary>
        /// список с системной информацией о файлах и дирректориях, который соответствует основному.
        /// </summary>
        private List<FileSystemInfo> _FileSystemInfo;
        /// <summary>
        /// Получение системной файловой информации по файлу\дирректории из списка по номеру
        /// </summary>
        /// <param name="line">номер строки списка</param>
        /// <returns></returns>
        public FileSystemInfo GetFileSystem(int line) => _FileSystemInfo[line];

        /// <summary>
        /// получение нового списка файлов и дирректорий, настройка отображения
        /// </summary>
        public void Update()
        {
            string last = GetFileSystem(ActiveRow)?.FullName;
            UpdateTableFileList();
            FormatAll();
            for (int i = 0; i < Rows.Count; i++)
            {
                if (GetFileSystem(i).FullName != last) continue;
                ActiveRow = i;
                CorrectTopLine();
                return;
            }
            Init();
            CorrectTopLine();
        }

        public FileManagerWindow()
        {
            StaticRow = new Row(this, "Имя", "Размер", "Дата");
            foreach (Cell it in StaticRow.Cells)
            {
                it.HorizAlign = Cell.EHorizAlign.Center;
                it.VertAlign = Cell.EVertAlign.Center;
            }
            _FileSystemInfo = new List<FileSystemInfo>();
            Columns[2].Width = 10;
            Columns[0].Width = 30;
            CurDir = System.IO.Directory.GetCurrentDirectory();
            UpdateTableFileList();
            base.KeyPress += FileManagerWindow_KeyPress;
        }

        /// <summary>
        /// обработчик события KeyPress (срабатывание на нажатие клавиш из списка [Keys])
        /// </summary>
        public new event EventHandler<TableEventArgs_KeyPress> KeyPress;

        /// <summary>
        /// Обрабатывает нажатие клавиши <Enter> если это относится к навигации по каталогам
        /// вызывает внешний обработчик, если он подписан..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileManagerWindow_KeyPress(object sender, TableEventArgs_KeyPress e)
        {
            FileManagerWindow window;
            window = (FileManagerWindow)sender;
            if (e.Key == ConsoleKey.Enter)
            {
                if ((window.ActiveRow == 0) && (window.IsRoot))
                {
                    window.UpdateTableDrives();

                    window.CurDir = string.Empty;
                    window.CorrectTopLine();
                    window.Init();
                    return;
                }
                FileSystemInfo fInfo;
                fInfo = window.GetFileSystem(window.ActiveRow);
                if (fInfo is DirectoryInfo)
                {
                    string lastdir = window.CurDir;
                    window.CurDir = window.GetFileSystem(window.ActiveRow).FullName;
                    window.UpdateTableFileList();
                    for (int i = 0; i < window.Rows.Count; i++)
                    {
                        if (window.GetFileSystem(i).FullName != lastdir) continue;
                        window.ActiveRow = i;
                        window.CorrectTopLine();
                        return;
                    }
                    window.Init();
                    return;
                }
            }
            KeyPress?.Invoke(this, e);
        }

        /// <summary>
        /// Заполняет таблицу списком дисков
        /// </summary>
        public void UpdateTableDrives()
        {
            _FileSystemInfo.Clear();
            Rows.Clear();

            string[] str = new string[3]; ;

            DriveInfo[] drives = DriveInfo.GetDrives();
            str[0] = "..";
            str[1] = "<Диск>";
            str[2] = string.Empty;
            AddRow(str);
            Rows[Rows.Count - 1].Cells[1].HorizAlign = Cell.EHorizAlign.Center;
            _FileSystemInfo.Add(null);
            foreach (var it in drives)
            {
                str[0] = it.Name;
                str[1] = "<Диск>";
                str[2] = string.Empty;
                AddRow(str);
                Rows[Rows.Count - 1].Cells[1].HorizAlign = Cell.EHorizAlign.Center;
                _FileSystemInfo.Add(it.RootDirectory);
            }
            _isRoot = true;
            _isDriveList = true;
            FormatAll();
            return;

        }

        /// <summary>
        /// Обновляет список файлов и каталогов по пути CurDir
        /// </summary>
        public void UpdateTableFileList()
        {
            _FileSystemInfo.Clear();
            Rows.Clear();
            _isRoot = false;
            _isDriveList = false;

            string[] str = new string[3]; ;
            if (Directory.Exists(CurDir) == false) return;

            DirectoryInfo directoryInfo = new DirectoryInfo(CurDir);

            if (directoryInfo.Root.Name == CurDir)
            {
                str[0] = "..";
                str[1] = "<Диск>";
                str[2] = string.Empty;
                AddRow(str);
                Rows[Rows.Count - 1].Cells[1].HorizAlign = Cell.EHorizAlign.Center;
                _FileSystemInfo.Add(directoryInfo);
                _isRoot = true;
            }
            if (directoryInfo.Root.Name != CurDir)
            {
                str[0] = "..";
                str[1] = "<Папка>";
                str[2] = directoryInfo.CreationTime.ToString("dd.MM.yyyy");
                AddRow(str);
                Rows[Rows.Count - 1].Cells[1].HorizAlign = Cell.EHorizAlign.Center;
                _FileSystemInfo.Add(directoryInfo.Parent);
            }


            string[] dirs = System.IO.Directory.GetDirectories(CurDir);
            for (int i = 0; i < dirs.Length; i++)
            {
                directoryInfo = new DirectoryInfo(dirs[i]);
                str[0] = directoryInfo.Name;
                str[1] = "<Папка>";
                str[2] = directoryInfo.CreationTime.ToString("dd.MM.yyyy");
                AddRow(str);
                Rows[Rows.Count - 1].Cells[1].HorizAlign = Cell.EHorizAlign.Center;
                _FileSystemInfo.Add(directoryInfo);
            }

            string[] files = Directory.GetFiles(CurDir);
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(files[i]);
                str[0] = fileInfo.Name;
                str[1] = fileInfo.Length.ToString("### ### ### ###");
                str[2] = fileInfo.CreationTime.ToString("dd.MM.yyyy");
                AddRow(str);
                Rows[Rows.Count - 1].Cells[1].HorizAlign = Cell.EHorizAlign.Right;
                _FileSystemInfo.Add(fileInfo);
            }
            FormatAll();
        }


    }
}
