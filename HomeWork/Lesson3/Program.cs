﻿using System.Collections.Generic;

namespace Lesson3
{
    class Program
    {
        static void Main(string[] args)
        {
            List <InterfaceTask> TaskList = new List<InterfaceTask> { 
                new Task1(), new Task2(), new Task3(), new Task4()
            };
            MainMenu menu = new MainMenu(3,TaskList);
            menu.Show();
        }
    }
}
