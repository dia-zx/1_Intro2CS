using MyUtils;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Lesson9
{
    /// <summary>
    /// Класс для хранения и загрузки настроек по умолчанию..
    /// </summary>
    [Serializable]
    public class AppUserSettings
    {
        /// <summary>
        /// Начальная дирректория файлового окна 1
        /// </summary>
        public string Window1Path;
        /// <summary>
        /// Начальная дирректория файлового окна 2
        /// </summary>
        public string Window2Path;
        /// <summary>
        /// Цвета бордюра
        /// </summary>
        public TextColors GridColors;
        /// <summary>
        /// Цвета выделенного текста
        /// </summary>
        public TextColors SellectedColors;
        /// <summary>
        /// Цвета основного текста
        /// </summary>
        public TextColors NormalTextColors;
        /// <summary>
        /// Цвета активной строки текста
        /// </summary>
        public TextColors ActiveTextColors;
        /// <summary>
        /// Цвета текста шапки таблицы файлового окна
        /// </summary>
        public TextColors StaticTextColors;

        public AppUserSettings()
        {
            Window1Path = Directory.GetCurrentDirectory();
            Window2Path = Directory.GetCurrentDirectory();
            GridColors = new TextColors(ConsoleColor.Blue, ConsoleColor.Black);
            SellectedColors = new TextColors(ConsoleColor.Green, ConsoleColor.Black);
            NormalTextColors = new TextColors(ConsoleColor.DarkGreen, ConsoleColor.Black);
            ActiveTextColors = new TextColors(ConsoleColor.DarkGreen, ConsoleColor.Gray);
            StaticTextColors = new TextColors(ConsoleColor.Cyan, ConsoleColor.Black);
        }

        /// <summary>
        /// Сериализация и сохранение объекта в файл
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public void Save(string path)
        {
            using (var stream = File.Create(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppUserSettings));
                xmlSerializer.Serialize(stream, this);
            }

        }

        /// <summary>
        /// Загрузка и десериализация объекта из указанного файла
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <returns></returns>
        public static AppUserSettings Load(string path)
        {
            try
            {
                using (var stream = File.OpenRead(path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppUserSettings));
                    return (AppUserSettings)xmlSerializer.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
