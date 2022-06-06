using System.Collections.Generic;

namespace Lesson5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<InterfaceTask> TaskList = new List<InterfaceTask> {
                new Task1(),
                new Task2(),
                new Task3(),
                new Task4(),
                new Task5()
            };
            MainMenu menu = new MainMenu(5, TaskList);
            menu.Show();
        }
    }
}
