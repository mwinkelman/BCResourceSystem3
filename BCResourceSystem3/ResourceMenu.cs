using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCResourceSystem3
{
    class ResourceMenu:Menu
    {
        private List<Resource> menuItems;

        public new List<Resource> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; }
        }

        public ResourceMenu(string name, List<Resource> menuItems)
        {
            Name = name;
            MenuItems = menuItems;
        }
        public ResourceMenu()
        { }
        public override int RunMenu()
        {
            Console.Clear();
            Console.WriteLine(Name + ":");
            int counter = 1;
            foreach (Resource item in menuItems)
            {
                Console.WriteLine($"{counter}.  {item.Title} ({item.Type})");
                counter++;
            }
            Console.WriteLine("Enter the number corresponding to the resource:");
            string input = Console.ReadLine();
            int choice;
            bool valid = int.TryParse(input, out choice) && choice > 0 && choice <= menuItems.Count;
            if (!valid)
                choice = 0;
            return choice - 1;
        }
    }
}
