using System;

namespace Lesson8
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Properties.Settings.Default.UserName == "")
                InputUserSettings();
            do
            {
                Greeatings();
                Console.WriteLine("\nВведите <1> для изменения данных. Иначе - выход");
                if (Console.ReadKey().KeyChar != '1') return;                
                InputUserSettings();
            } while (true);
        }

        /// <summary>
        /// Ввод данных пользователя и сохранение профиля
        /// </summary>
        static void InputUserSettings() {
            Console.Clear();
            Console.Write("Введите имя пользователя: ");
            Properties.Settings.Default.UserName = Console.ReadLine();
            Console.Write("Введите ваш возраст: ");
            Properties.Settings.Default.UserAge = int.Parse(Console.ReadLine());
            Console.Write("Введите ваш род деятельности: ");
            Properties.Settings.Default.UserJob = Console.ReadLine();
            Properties.Settings.Default.Save(); //сохраняем профиль
        }

        /// <summary>
        /// Приветствует пользователя по данным профиля
        /// </summary>
        static void Greeatings() {
            Console.WriteLine(Properties.Settings.Default.GreeatingsText + " " +
                Properties.Settings.Default.UserName);
            Console.WriteLine($"Вам {Properties.Settings.Default.UserAge} лет.");
            Console.WriteLine($"Ваш род деятельности: {Properties.Settings.Default.UserJob}");
        }
    }
}
