using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCResourceSystem3
{
    class StringMenu:Menu  
    {
        #region fields
        private List<string> menuItems;
        #endregion

        #region properties
        public new List<string> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; }
        }
        #endregion

        #region constructors
        public StringMenu(string name, List<string> menuItems)
        {
            Name = name;
            MenuItems = menuItems;
        }
        #endregion
        public override int RunMenu()
        {
            Console.Clear();
            Console.WriteLine(Name + ":");
            int counter = 1;
            foreach (string item in menuItems)
            {
                Console.WriteLine($"{counter}.  {item}");
                counter++;
            }
            Console.WriteLine("Enter the number of a Menu Item:");
            string input = Console.ReadLine();
            int choice;
            bool valid = int.TryParse(input, out choice) && choice > 0 && choice <= menuItems.Count;
            if (!valid)
                choice = 0;
            return choice;
        }
    }
}
