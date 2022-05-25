using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3
{
    class Task2 : InterfaceTask
    {
        private string[,] PhoneBook;
        public void Execute()
        {
            PhoneBook = null;
            AddContact("Денис Д.", "+7909-12-345-67, asddh@cxxbv.ru");
            AddContact("Алексей Б.В.", "+7903-22-325-01, asd@bv.ru");
            AddContact("Владимир", "+7902-18-365-43, zz@den.com");
            AddContact("Сергей", "+7900-11-353-82, rambler@news.ru");
            AddContact("Иван", "+7905-68-952-36");

            do
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tЗадача 2.");
                Console.WriteLine("============================================================================");
                Console.WriteLine("* Написать программу «Телефонный справочник»: создать двумерный массив 5х2, *");
                Console.WriteLine("* хранящий список телефонных контактов: первый элемент хранит имя контакта, *");
                Console.WriteLine("* второй — номер телефона/ email.                                           *");
                Console.WriteLine("============================================================================");
                Console.WriteLine("Решение:\n");
                Console.WriteLine("\t\tТелефонная книга:");

                Print();
                Console.WriteLine("\nВведите \"1\" - для добавления контакта.");
                Console.WriteLine("Введите \"2\" - для удаления контакта.");
                Console.WriteLine("Введите \"0\" - для выхода.");
                string input = Console.ReadLine();
                if (input == "0") return;
                if (input == "1") MenuAddContact();
                if (input == "2") MenuDelContact();
            } while (true);
        }

        public string ShortDescription { get => "«Телефонный справочник»"; }

        /// <summary>
        /// Добавляет новый контакт в телефонную книгу
        /// </summary>
        /// <param name="name">имя контакта</param>
        /// <param name="phone">телефонный номер / e-mail</param>
        private void AddContact(string name, string phone)
        {
            string[,] newPhoneBook;
            if (PhoneBook != null)
            {
                newPhoneBook = new string[PhoneBook.GetLength(0) + 1, 2];
                Array.Copy(PhoneBook, newPhoneBook, PhoneBook.Length);
            }
            else
                newPhoneBook = new string[1, 2];

            newPhoneBook[newPhoneBook.GetLength(0) - 1, 0] = name;
            newPhoneBook[newPhoneBook.GetLength(0) - 1, 1] = phone;
            PhoneBook = newPhoneBook;
        }

        /// <summary>
        /// Удаляет контакт из телефонной книги
        /// </summary>
        /// <param name="number">номер удаляемого контакта</param>
        private void DelContact(int number) {
            if(PhoneBook.GetLength(0) == 1)
            {
                PhoneBook = null;
                return;
            }
            string[,] newPhoneBook;
            newPhoneBook = new string[PhoneBook.GetLength(0) - 1, 2];
            int j = 0;
            for (int i = 0;  i< PhoneBook.GetLength(0); i++)
            {
                if (i == number) continue;
                newPhoneBook[j, 0] = PhoneBook[i, 0];
                newPhoneBook[j++, 1] = PhoneBook[i, 1];
            }
            PhoneBook = newPhoneBook;
        }
        /// <summary>
        /// Вывод на консоль телефонного справочника
        /// </summary>
        private void Print()
        {
            if (PhoneBook == null)
            {
                Console.WriteLine("Телефонная книга пуста!");
                return;
            }
            for (int i = 0; i < PhoneBook.GetLength(0); i++)
            {
                Console.WriteLine($" {i} -\t{PhoneBook[i, 0]}\t{PhoneBook[i, 1]}");
            }
        }

        private void MenuAddContact() {
            Console.Write("Имя контакта: ");
            string name = Console.ReadLine();
            Console.Write("Телефонный номер / e-mail: ");
            string phone = Console.ReadLine();
            this.AddContact(name, phone);
        }

        private void MenuDelContact()
        {
            if(PhoneBook == null || PhoneBook.GetLength(0) == 0)
            {
                Console.WriteLine("Нет контактов для удаления.");
                Console.ReadKey();
                return;
            }

            DelContact(
                MyFunctions.InputInt($"Порядковый номер контакта [0..{PhoneBook.GetLength(0)-1}]: ",
                        0, PhoneBook.GetLength(0)-1)
                      );            
        }
    }
}
