using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCResourceSystem3
{
    class StudentMenu:Menu
    {
        private List<Student> menuItems;

        public new List<Student> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; }
        }

        public StudentMenu(string name, List<Student> menuItems)
        {
            Name = name;
            MenuItems = menuItems;
        }

        public override int RunMenu()
        {
            Console.Clear();
            Console.WriteLine(Name + ":");
            int counter = 1;
            foreach (Student item in menuItems)
            {
                Console.WriteLine($"{counter}.  {item.FullName}");
                counter++;
            }
            Console.WriteLine("Enter the number corresponding to the student:");
            string input = Console.ReadLine();
            int choice;
            bool valid = int.TryParse(input, out choice) && choice > 0 && choice <= menuItems.Count;
            if (!valid)
                choice = 0;
            return choice-1;
        }
    }
}
