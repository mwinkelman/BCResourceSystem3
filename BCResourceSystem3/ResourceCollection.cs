using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace BCResourceSystem3
{
    class ResourceCollection
    {
        #region fields
        private List<Resource> items;
        #endregion

        #region properties
        public List<Resource> Items
        {
            get { return items; }
            set { items = value; }
        }       
        #endregion

        #region constructors
        public ResourceCollection()
        { }
        public ResourceCollection(List<Resource> items)
        {
            Items = items;
            
        }
        #endregion

        #region methods
        public void ViewAll()
        {
            Console.WriteLine("ALL RESOURCES:");
            foreach (Resource resource in Items)
            {
                Console.WriteLine($"{resource.Title} ({resource.Type})");
            }
        }
        public void ViewAvailable()
        {
            Console.WriteLine("AVAILABLE RESOURCES");
            foreach (Resource resource in Items)
            {
                if (resource.Status == null)
                {
                    Console.WriteLine($"{resource.Title} ({resource.Type})");
                }
            }
        }
        public void ViewCheckedOut()
        {
            Console.WriteLine("CHECKED OUT RESOURCES");
            foreach (Resource resource in Items)
            {
                if (resource.Status != null)
                {
                    Console.WriteLine($"{resource.Title} ({resource.Type}) \nChecked out by {resource.Status}");
                }
            }
        }
        public bool ViewIndividual(ResourceMenu resourceMenu,List<Resource> resourceList)
        {
            int choice = resourceMenu.RunMenu();
            bool valid = true;
            try
            {
                Resource resource = resourceList[choice];
                string availability;
                if (resource.Status == null)
                { availability = "Available to Checkout"; }
                else
                { availability = $"Unavailable. Checked out by {resource.Status.FullName}"; }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Title: {resource.Title}");
                sb.AppendLine($"ISBN: {resource.ISBN}");
                sb.AppendLine($"Length: {resource.Length}");
                sb.AppendLine($"Status: {availability}");
                string info = sb.ToString();
                Console.WriteLine(info);
            }
            catch (ArgumentOutOfRangeException)
            {
                valid = false;
            }
            return valid;
        }
        public void CheckOut(StudentMenu studentMenu, ResourceMenu resourceMenu)
        {
            DateTime checkout = DateTime.Now;
            DateTime dueDate = checkout.AddDays(3);
            int studentNumber = studentMenu.RunMenu();
            Student student = studentMenu.MenuItems[studentNumber];
            Console.WriteLine($"Student is {student.FullName}");
            int resourceNumber = resourceMenu.RunMenu();
            Resource resource = resourceMenu.MenuItems[resourceNumber];
            Console.WriteLine($"resource is {resource.Title}");
            resource.Status = student;
            Console.WriteLine($"{resource.Title} has been checked out by {student.FullName}. It is due back by {dueDate}.");
        }
        public void Return(StudentMenu studentMenu, ResourceMenu resourceMenu)
        {
            int studentNumber = studentMenu.RunMenu();
            Student student = studentMenu.MenuItems[studentNumber];
            int resourceNumber = resourceMenu.RunMenu();
            Resource resource = resourceMenu.MenuItems[resourceNumber];
            resource.Status = null;
            Console.WriteLine($"{resource.Title} has been returned by {student.FullName}.");
        }
        #endregion
    }
}
