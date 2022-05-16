using System;
using System.Collections.Generic;

namespace Lesson2
{
    class Program
    {
        //объявим делегата, чтобы иметь возможность передавать методы "Task" как объекты...
        delegate void TaskDelegat();
        /// <summary>
        /// Структура для описания задачи
        ///     TaskTytle - строка заголовка для меню
        ///     Task - Выполняемый метод задачи
        /// </summary>
        struct TaskInfo
        {
            public string TaskTytle;
            public TaskDelegat Task;
        };

        static void Main(string[] args)
        {
            #region Создадим и проинициализируем список задач
            List<TaskInfo> TaskList;
            TaskList = new List<TaskInfo>() {
                new TaskInfo() { TaskTytle = "\"Cреднесуточная температура.\"", Task = Task1 },
                new TaskInfo() { TaskTytle = "\"Текущий месяц.\"",              Task = Task2 },
                new TaskInfo() { TaskTytle = "\"Является ли число чётным.\"",   Task = Task3 },
                new TaskInfo() { TaskTytle = "\"Чек.\"",                        Task = Task4 },
                new TaskInfo() { TaskTytle = "\"Дождливая зима.\"",             Task = Task5 },
                new TaskInfo() { TaskTytle = "\"универсальная структура расписания недели (битовые маски).\"",
                                                                                Task = Task6 },
            };
            #endregion

            do
            {
                Console.Clear();
                Console.WriteLine("**********************************");
                Console.WriteLine("*        Перечень задач          *");
                Console.WriteLine("*--------------------------------*");

                for (int i = 0; i < TaskList.Count; i++) // Выводим список задач
                    Console.WriteLine($"* {i + 1} - Задача {i + 1} ({TaskList[i].TaskTytle})");

                Console.WriteLine("**********************************");
                Console.WriteLine($"Введите число от 1 до {TaskList.Count}. " + 
                                "Введите \"exit\" для выхода.");
                
                string TaskNumberString = Console.ReadLine();
                
                if (TaskNumberString == "exit") return; //выходим если введено "exit" 
                if (Convert2IntANDCheckNumber(TaskNumberString, out int TaskNumber, 1, 6) == false)
                {
                    Console.WriteLine($"Неверное значение! Введите число 1..{TaskList.Count}!" +
                                        "Введите \"exit\" для выхода."); 
                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                    continue;
                }

                Console.Clear();
                TaskList[TaskNumber - 1].Task();    // Запускаем задачу
            } while (true);
        }

        /// <summary>
        /// Преобразует строку (strNumber) в int с проверкой возможности преобразования
        /// и проверяет результирующее значение на принадлежность диапазону [min...max]
        /// </summary>
        /// <param name="strNumber">Строка, которую преобразуем в int</param>
        /// <param name="ResultInt">результат преобразования строки в int</param>
        /// <param name="min">минимальное значение, для ResultInt</param>
        /// <param name="max">максимальное значение, для ResultInt</param>
        /// <returns>true - если результат (ResultInt) можно привести к int и 
        ///                 он удовлетворяет условию: min <= ResultInt <= max
        /// </returns>
        static bool Convert2IntANDCheckNumber(string strNumber, out int ResultInt, int min, int max)
        {
            if (Int32.TryParse(strNumber, out ResultInt))
                if ((ResultInt >= min) && (ResultInt <= max)) return true;
            return false;
        }





        /// <summary>
        ///             Задача 1.
        /// Запросить у пользователя минимальную и максимальную температуру
        /// за сутки и вывести среднесуточную температуру.
        /// </summary>
        static void Task1()
        {
            Console.WriteLine("         Задача 1.");
            Console.WriteLine("===============================================");
            Console.WriteLine("Условие:");
            Console.WriteLine("Запросить у пользователя минимальную и максимальную температуру\n" +
                              "за сутки и вывести среднесуточную температуру.");
            Console.WriteLine("===============================================\n");

            double MinTemperature = InputDouble("Введите минимальную температуру за сутки: ");
            double MaxTemperature = InputDouble("Введите максимальную температуру за сутки: ");

            Console.WriteLine("===============================================\n");
            Console.WriteLine("Результат:");
            Console.WriteLine($" Среднесуточная температура: {FuncMean2(MinTemperature, MaxTemperature):F2}");

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }

        /// <summary>
        /// Функция для вычисления среднего значения для 2х аргументов
        /// </summary>
        /// <param name="value1"> первый аргумент</param>
        /// <param name="value2"> второй аргумент</param>
        /// <returns></returns>
        static double FuncMean2(double value1, double value2)
        {
            return ((value1 + value2) / 2.0);
        }


        /// <summary>
        /// Функция запрашивает ввод значения типа double с проверкой типа.
        /// </summary>
        /// <param name="Text"> текст для запроса</param>
        /// <returns></returns>
        static double InputDouble(string Text)
        {
            double result;
            do
            {
                Console.Write(Text);
                if (double.TryParse(Console.ReadLine(), out result))
                    return result;
                Console.WriteLine("Неверное значение!");
                continue;
            }
            while (true);
        }

        /// <summary>
        /// Функция запрашивает ввод значения типа int с проверкой типа.
        /// </summary>
        /// <param name="Text"> текст для запроса</param>
        /// <returns></returns>
        static int InputInt(string Text)
        {
            int result;
            do
            {
                Console.Write(Text);
                if (int.TryParse(Console.ReadLine(), out result))
                    return result;
                Console.WriteLine("Неверное значение!");
                continue;
            }
            while (true);
        }

        /// <summary>
        /// Функция запрашивает ввод значения типа int с проверкой типа и
        /// проверкой требования нахождения значения на отрезке [Min ... Max]
        /// </summary>
        /// <param name="Text"> текст для запроса</param>
        /// <param name="Min"> минимальное значение, введенного числа </param>
        /// <param name="Max"> максимальное значение, введенного числа</param>
        /// <returns></returns>
        static int InputInt(string Text, int Min, int Max)
        {
            do
            {
                Console.Write(Text);
                if (int.TryParse(Console.ReadLine(), out int result)
                                && (result >= Min) && (result <= Max))
                    return result;
                Console.WriteLine("Неверное значение!");
                continue;
            }
            while (true);
        }



        /// <summary>
        ///             Задача 2.
        /// Запросить у пользователя порядковый номер текущего месяца и вывести его название.
        /// </summary>
        static void Task2()
        {
            Console.WriteLine("         Задача 2.");
            Console.WriteLine("===============================================");
            Console.WriteLine("Условие:");
            Console.WriteLine("Запросить у пользователя порядковый номер\n" +
                              "текущего месяца и вывести его название");
            Console.WriteLine("===============================================\n");

            int MonthNumber = InputInt("Введите порядковый номер месяца [1...12]: ", 1, 12);

            #region Вариант 1. Решение через switch - case
            string Result1_Switch = "";
            switch (MonthNumber)
            {
                case 1: Result1_Switch = "Январь"; break;
                case 2: Result1_Switch = "Февраль"; break;
                case 3: Result1_Switch = "Март"; break;
                case 4: Result1_Switch = "Апрель"; break;
                case 5: Result1_Switch = "Май"; break;
                case 6: Result1_Switch = "Июнь"; break;
                case 7: Result1_Switch = "Июль"; break;
                case 8: Result1_Switch = "Август"; break;
                case 9: Result1_Switch = "Сентябрь"; break;
                case 10: Result1_Switch = "Октябрь"; break;
                case 11: Result1_Switch = "Ноябрь"; break;
                case 12: Result1_Switch = "Декабрь"; break;
            }
            #endregion


            #region Вариант 2. Решение через массив
            string Result2_Massiv;
            string[] MonthList = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май",
                                                "Июнь", "Июнь", "Август", "Сентябрь",
                                                "Октябрь", "Ноябрь", "Декабрь"
                                              };
            Result2_Massiv = MonthList[MonthNumber - 1];
            #endregion


            Console.WriteLine("===============================================\n");
            Console.WriteLine("Результат:");
            Console.WriteLine($" Вариант 1 (switch - case): {Result1_Switch}");
            Console.WriteLine($" Вариант 2 (через массив): {Result2_Massiv}");

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }




        /// <summary>
        ///             Задача 3.
        /// Определить, является ли введённое пользователем число чётным.
        /// </summary>
        static void Task3()
        {
            Console.WriteLine("         Задача 3.");
            Console.WriteLine("===============================================");
            Console.WriteLine("Условие:");
            Console.WriteLine("Определить, является ли введённое пользователем число чётным.");
            Console.WriteLine("===============================================\n");

            int Number = InputInt("Введите целое число: ");

            Console.WriteLine("===============================================\n");
            Console.WriteLine("Результат:");
            if ((Number & 1) == 0)
                Console.WriteLine("Введенное число - четное");
            else
                Console.WriteLine("Введенное число - не четное");
            
            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }




        /// <summary>
        ///             Задача 4.
        /// Для полного закрепления понимания простых типов найдите любой чек, либо фотографию
        /// этого чека в интернете и схематично нарисуйте его в консоли, только за место динамических,
        /// по вашему мнению, данных(это может быть дата, название магазина, сумма покупок)
        /// подставляйте переменные, которые были заранее заготовлены до вывода на консоль.
        /// </summary>
        static void Task4()
        {
            Console.WriteLine("         Задача 4.");
            Console.WriteLine("===============================================");
            Console.WriteLine("Условие:");
            Console.WriteLine("Для полного закрепления понимания простых типов найдите любой чек,\n" +
                " либо фотографию этого чека в интернете и схематично нарисуйте его в консоли,\n" +
                " только за место динамических по вашему мнению, данных\n" +
                "(это может быть дата, название магазина, сумма покупок) подставляйте переменные,\n" +
                " которые были заранее заготовлены до вывода на консоль.");
            Console.WriteLine("===============================================\n");


            string CompanyName = "Дом и сад";
            string CompanyAddr1 = "Павлово, Павловский р-н.";
            string CompanyAddr2 = "ул. Вокзальная, дом 1";
            ulong CompanyPhone = 7_930_071_178L;
            DateTime date = new DateTime(2022, 5, 16, 14, 38, 30);
            ushort CheckNumber = 58;
            decimal Price = 3531.0M;
            uint AuthorizationCode = 82985;



            Console.WriteLine( "\t -------------------------------");
            Console.WriteLine( "\t|            СБЕРБАНК           |");
            Console.WriteLine($"\t|           {CompanyName}           |");
            Console.WriteLine($"\t|  {CompanyAddr1}     |");
            Console.WriteLine($"\t|     {CompanyAddr2}     |");
            Console.WriteLine($"\t|         т. {CompanyPhone}         |");
            Console.WriteLine($"\t|{date:MM.dd.yy}   {date:HH:mm}      Чек {CheckNumber:D4} |");
            Console.WriteLine( "\t|Клиент:                        |");
            Console.WriteLine( "\t|Сумма (руб):                   |");
            Console.WriteLine($"\t|\t\t{Price:F2}         |");
            Console.WriteLine( "\t|Комиссия  - 0 руб.             |");
            Console.WriteLine( "\t|         ОДОБРЕНО              |");
            Console.WriteLine($"\t|Код авторизации:        {AuthorizationCode:D6} |");
            Console.WriteLine( "\t|Проверено на устройстве клиента|");
            Console.WriteLine( "\t -------------------------------");

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }



        /// <summary>
        ///             Задача 5.
        /// Если пользователь указал месяц из зимнего периода, а средняя температура > 0, вывести
        /// сообщение «Дождливая зима».
        /// </summary>
        static void Task5() {
            Console.WriteLine("         Задача 5.");
            Console.WriteLine("===============================================");
            Console.WriteLine("Условие:");
            Console.WriteLine("Если пользователь указал месяц из зимнего периода,\n" +
                " а средняя температура > 0, вывести сообщение «Дождливая зима».");
            Console.WriteLine("===============================================\n");

            int MonthNumber = InputInt("Введите порядковый номер месяца [1...12]: ", 1, 12);
            double MeanTemperature = InputDouble("Введите среднюю температуру: ");

            if ((MeanTemperature > 0) && ((MonthNumber <= 2) || (MonthNumber == 12)))
                Console.WriteLine("\n«Дождливая зима»");

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }



        /// <summary>
        ///             Задача 6.
        /// Для полного закрепления битовых масок, попытайтесь создать универсальную структуру
        /// расписания недели, к примеру, чтобы описать работу какого либо офиса.Явный пример -
        /// офис номер 1 работает со вторника до пятницы, офис номер 2 работает с понедельника до
        /// воскресенья и выведите его на экран консоли
        /// </summary>
        static void Task6() {
            Console.WriteLine("         Задача 6.");
            Console.WriteLine("===============================================");
            Console.WriteLine("Условие:");
            Console.WriteLine("Для полного закрепления битовых масок, попытайтесь\n" +
                              "создать универсальную структуру расписания недели,\n" +
                              "к примеру, чтобы описать работу какого либо офиса.\n" +
                              " Явный пример - офис номер 1 работает со вторника до пятницы,\n" +
                              "офис номер 2 работает с понедельника до воскресенья\n" +
                              "и выведите его на экран консоли.");
            Console.WriteLine("===============================================\n");

            //объявим переменные типа WeekDays для описания рабочих дней 2х офисов
            WeekDays Office1_Working, Office2_Working;

            //офис номер 1 работает со вторника до пятницы (считаем, что пятница не входит..)
            Office1_Working = WeekDays.Tuesday | WeekDays.Wednesday | WeekDays.Thursday;

            //офис номер 2 работает с понедельника до воскресенья (считаем, что воскресенье не входит..)
            Office2_Working = WeekDays.Monday | WeekDays.Tuesday | WeekDays.Wednesday |
                              WeekDays.Thursday | WeekDays.Friday | WeekDays.Saturday;

            Console.WriteLine("Результат:");
            Console.WriteLine("Рабочие дни офиса1: " + Office1_Working.ToString());
            Console.WriteLine("Рабочие дни офиса2: " + Office2_Working.ToString());

            Console.WriteLine("\n\nНажмите любую клавишу.");
            Console.ReadKey();
        }
    }

    [Flags]
    public enum WeekDays
    {
        Monday      = 0b_0000_0001,
        Tuesday     = 0b_0000_0010,
        Wednesday   = 0b_0000_0100,
        Thursday    = 0b_0000_1000,
        Friday      = 0b_0001_0000,
        Saturday    = 0b_0010_0000,
        Sunday      = 0b_0100_0000
    }

}
