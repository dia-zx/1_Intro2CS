using System;

namespace Lesson3
{
    class Task4 : InterfaceTask
    {
        /// <summary>
        /// отступ отрисовки поля по x
        /// </summary>
        private const int Left = 18;
        /// <summary>
        /// отступ отрисовки поля по y
        /// </summary>
        private int Top;

        public string ShortDescription => "«Морской бой»";

        /// <summary>
        /// Игровое поле... здесь храним все объекты...
        /// </summary>
        private FildStatus[,] GameFild;

        public void Execute()
        {
            GameFild = new FildStatus[10, 10]; // проинициализируем игровое поле
            PlaceShip(5);
            PlaceShip(4);
            PlaceShip(3);
            PlaceShip(3);
            PlaceShip(2);

            do
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tЗадача 4.");
                Console.WriteLine("==============================================================================");
                Console.WriteLine("* «Морской бой»: вывести на экран массив 10х10, состоящий из символов X и O, *\n" +
                                  "* где Х — элементы кораблей, а О — свободные клетки.                         *");
                Console.WriteLine("==============================================================================");
                Console.WriteLine("Решение:\n");


                Print();


                Console.WriteLine("\n\nДля выхода введите \"exit\". ");
                Console.WriteLine("Для выстрела введите координаты, например \"A10\"");
                do
                {
                    string userInput = Console.ReadLine();
                    userInput = userInput.ToLower();
                    if (userInput == "exit") return;
                    if (ShootAt(userInput) == true) break;
                } while (true);

            } while (true);
        }


        [Flags]
        enum FildStatus
        {
            Empty = 0b_0000_0000, // Пустое поле
            Ship = 0b_0000_0001, // занято кораблем
            Shoot = 0b_0000_0010  // простреленная клетка
        }

        enum ShipDirection
        {
            Horiz = 0,  // Корабль будем располагать горизонтально
            Vert = 1    // Корабль будем располагать вертикально
        }

        /// <summary>
        /// Вывод игрового поля на консоль
        /// </summary>
        private void Print()
        {
            if (Console.WindowHeight < 40) Console.WindowHeight = 40;
            #region Отрисовываем A B C D E F....
            Console.ForegroundColor = ConsoleColor.Green;
            Top = Console.CursorTop;
            Console.CursorLeft = Left + 4;
            for (Char ch = 'A'; ch < 'A' + 10; ch++)
            {
                Console.Write(ch);
                Console.CursorLeft += 3;
            }
            Console.ResetColor();
            #endregion

            #region отрисовываем 1 2 3 4 5 6.. по вертикали
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorTop = Top + 2;
            for (int i = 1; i < 11; i++)
            {
                Console.CursorLeft = Left + 2 - 1 - i.ToString().Length;
                Console.Write(i);
                Console.CursorTop += 2;
            }
            Console.ResetColor();
            #endregion

            #region Отрисовываем сетку по горизонтали
            Console.ForegroundColor = ConsoleColor.Blue;
            string str = new string('-', 10 * 4 + 3);
            Console.CursorTop = Top + 1;
            for (int i = 0; i <= 10; i++)
            {
                Console.CursorLeft = Left - 1;
                Console.Write(str);
                Console.CursorTop += 2;
            }
            Console.ResetColor();
            #endregion

            #region Отрисовываем сетку по вертикали
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i <= 10; i++)
            {
                for (int j = 0; j < 11 * 2; j++)
                {
                    Console.CursorLeft = Left + 2 + i * 4;
                    Console.CursorTop = Top + j;
                    Console.Write("|");
                }
            }
            Console.ResetColor();
            #endregion

            #region Отрисовка объектов на поле
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                {
                    Console.CursorTop = Top + 2 + y * 2;
                    Console.CursorLeft = Left + 3 + x * 4;
                    switch (GameFild[x, y])
                    {
                        case FildStatus.Empty: // пустая клетка
                            Console.ResetColor();
                            Console.Write("   ");
                            break;
                        case FildStatus.Ship: // корабль
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("\u2588\u2588\u2588");
                            Console.ResetColor();
                            break;
                        case FildStatus.Shoot: // обстрелянное поле
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" \u2219 ");
                            Console.ResetColor();
                            break;
                        case FildStatus.Ship | FildStatus.Shoot://подбитый корабль
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\u2588\u2588\u2588");
                            Console.ResetColor();
                            break;
                    }
                }
            #endregion
            Console.CursorTop = Top + 22;
        }

        /// <summary>
        /// Случайным образом расставляет на поле корабль указанного размера
        /// </summary>
        /// <param name="ShipLength">размер корабля</param>
        private bool PlaceShip(int ShipLength)
        {
            Random rnd = new Random();

            ShipDirection dir;
            int x, y;
            for (int count = 0; count < 1000; count++) //организуем счетчик числа попыток 
            {
                dir = (ShipDirection)rnd.Next(2);
                x = rnd.Next(10);
                y = rnd.Next(10);
                bool flagOK = true; //здесь будем фиксировать возможность расстановки корабля
                for (int i = 0; i < ShipLength; i++)
                {
                    int NextX = x, NextY = y;
                    if (dir == ShipDirection.Horiz) //следующая точка корабля в зависимости выбранного напраления dir
                        NextX = x + i;
                    else
                        NextY = y + i;

                    if ((NextX > 9) || (NextY > 9) || CheckEmptyAround(NextX, NextY) == false)
                    {// место уже занято... либо вышли за границу поля...
                        flagOK = false;
                        break;
                    }
                }
                if (flagOK) //можно ставить корабль!
                {
                    for (int i = 0; i < ShipLength; i++)
                    {
                        GameFild[x, y] = FildStatus.Ship;
                        if (dir == ShipDirection.Horiz) //следующая точка корабля в зависимости выбранного напраления dir
                            x++;
                        else
                            y++;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверка наличия свободного места в окрестности точки
        /// </summary>
        /// <param name="x">координата x точки</param>
        /// <param name="y">координата y точки</param>
        /// <returns>true - если место вокруг свободно есть</returns>
        private bool CheckEmptyAround(int x, int y)
        {
            for (int X = x - 1; X <= x + 1; X++)
                for (int Y = y - 1; Y <= y + 1; Y++)
                {
                    if ((Y < 0) || (X < 0) || (Y > 9) || (X > 9)) continue;
                    if (GameFild[X, Y] != FildStatus.Empty)
                        return false;
                }
            return true;
        }

        /// <summary>
        /// Дешифрует координаты вида "b10" -> 5,5 и выполняет выстрел)
        /// </summary>
        /// <param name="userInput">текстовая строка с координатами</param>
        /// <returns>false - если координаты верные</returns>
        private bool ShootAt(string userInput)
        {
            if ((userInput[0] < 'a') || (userInput[0] > 'j')) return false;
            int x = userInput[0] - 'a';
            userInput = userInput.Remove(0, 1);
            if (int.TryParse(userInput, out int y) == false) return false;
            y--;
            if ((y < 0) || (y > 9)) return false;

            GameFild[x, y] |= FildStatus.Shoot;

            #region Анимация выстрела
            Console.CursorTop = Top + 2 + y * 2;
            Console.CursorLeft = Left + 4 + x * 4;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorVisible = false;
            Console.Write('#');
            System.Threading.Thread.Sleep(200);
            Console.CursorLeft--;
            Console.Write('*');
            System.Threading.Thread.Sleep(200);
            Console.CursorLeft--;
            Console.Write('\u2219');
            System.Threading.Thread.Sleep(200);
            Console.ResetColor();
            Console.CursorVisible = true;
            #endregion
            return true;            
        }
    }
}
