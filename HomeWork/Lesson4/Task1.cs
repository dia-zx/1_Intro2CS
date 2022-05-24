using System;

namespace Lesson4
{
    class Task1 : InterfaceTask
    {
        public string ShortDescription => "Написать метод GetFullName...";

        public void Execute()
        {
            Console.WriteLine("\t\t\t\tЗадача 1.");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("* Написать метод GetFullName(string firstName, string lastName, string patronymic), *");
            Console.WriteLine("* принимающий на вход ФИО в разных аргументах и возвращающий объединённую строку с  *");
            Console.WriteLine("* с ФИО. Используя метод, написать программу выводящую в консоль 3–4 разных ФИО.    *");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("Решение:\n");

            rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                (string firstName, string lastName, string patronymic) = GetRandomFIO();
                Console.WriteLine(GetFullName(firstName, lastName, patronymic));
            }
            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }
        private Random rnd;

        /// <summary>
        /// Склеивает ФИО
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="patronymic">Отчество</param>
        /// <returns></returns>
        private static string GetFullName(string firstName, string lastName, string patronymic) {
            return $"{lastName} {firstName} {patronymic}";
        }

        /// <summary>
        /// Генерирует случайным образом кортеж из Ф, И, О
        /// </summary>
        /// <returns>кортеж из ФИО</returns>
        private (string firstName, string lastName, string patronymic) GetRandomFIO() {
            string[] firstName = { "Петр", "Владимир", "Андрей", "Константин", "Михаил", "Александр", "Егор", "Алексей"};
            string[] lastName = { "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Петров", "Соколов", "Михайлов"};
            string[] patronymic = { "Федорович", "Ярославович", "Юрьевич", "Степанович", "Станиславович", "Алексеевич",
                                    "Борисович", "Викторович"};
             
            return (firstName[rnd.Next(firstName.Length)], lastName[rnd.Next(lastName.Length)],
                patronymic[rnd.Next(patronymic.Length)]);
        }
    }
}
