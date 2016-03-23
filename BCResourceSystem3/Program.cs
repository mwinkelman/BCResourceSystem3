using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BCResourceSystem3
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Menu Instantiation
            List<string> mainMenuItems = new List<string> { "View Resources", "View Students", "View Student Account", "Checkout/Return Resource", "Edit Resource (WARNiNG: BUGGY!)", "Exit" };
            StringMenu mainMenu = new StringMenu("MAIN MENU", mainMenuItems);
            List<string> resourceMenuItems = new List<string> { "All Resources", "Available Resources", "Checked Out Resources","Individual Resource" };
            StringMenu resourceOptionsMenu = new StringMenu("RESOURCES MENU", resourceMenuItems);
            List<string> checkoutReturnMenuItems = new List<string> { "Checkout Resource", "Return Resource" };
            StringMenu chktRtrnMenu = new StringMenu("ACTIONS MENU", checkoutReturnMenuItems);
            #endregion
            #region Student Instantiation
            List<Student> studentList = new List<Student>
            {
                new Student ("Mary","Winkelman"),
                new Student ("Cadale", "Thomas"),
                new Student ("Imari", "Childress"),
                new Student ("Quinn","Bennett")
            };
            DataTable studentDataTable = new DataTable();
            DataColumn firstName = new DataColumn();
            DataColumn lastName = new DataColumn();
              
            StudentMenu studentMenu = new StudentMenu("STUDENTS", studentList);
            #endregion
            #region Resource Instantiation
            string file = @"ResourceData.bin";
            var resourceList = new List<Resource>();
            ResourceMenu resourceMenu;
            ResourceCollection collection1;
            if (!File.Exists(file))
            {
                resourceList.Add(new Book("Book One", 355));
                resourceList.Add(new Book("Book Two", 67));
                resourceList.Add(new Book("Book Three", 125));
                resourceList.Add(new DVD("DVD One", 100));
                resourceList.Add(new DVD("DVD Two", 120));
                resourceList.Add(new DVD("DVD Three", 90));
                resourceList.Add(new Magazine("Magazine One", 120));
                resourceList.Add(new Magazine("Magazine Two", 60));
                resourceList.Add(new Magazine("Magazine Three", 110));
                try
                {
                    using (Stream stream = File.Open(file, FileMode.Create))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, resourceList);
                    }
                }
                catch (IOException)
                {
                }           
                resourceMenu = new ResourceMenu("RESOURCES", resourceList);
                collection1 = new ResourceCollection(resourceList);
            }
            else
            {
                try
                {
                    using (Stream stream = File.Open(file, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        resourceList = (List<Resource>)formatter.Deserialize(stream);
                    }
                }
                catch (IOException)
                {
                }
                resourceMenu = new ResourceMenu("RESOURCES", resourceList);
                collection1 = new ResourceCollection(resourceList);
            }
            #endregion            

            #region Main Program
            bool repeat = true;
            do
            {
                int mainChoice = mainMenu.RunMenu();
                switch (mainChoice)
                {
                    #region View Resources
                    case 1: //View Resources
                        ViewResources(resourceOptionsMenu, collection1,resourceMenu);
                        break;
                    #endregion
                    #region View Students
                    case 2: //View Students
                        ViewStudents(studentList);
                        Console.Read();
                        break;
                    #endregion
                    #region View Student Acct
                    case 3: //View Student Account
                        ViewStudentAccount(studentMenu, resourceList);
                        Console.WriteLine("\nPress ENTER to return to Main Menu.");
                        Console.Read();
                        break;
                    #endregion
                    #region Checkout/Return
                    case 4:
                        int chktRtrnChoice = chktRtrnMenu.RunMenu();
                        switch (chktRtrnChoice)
                        {
                            case 1:
                                Checkout(collection1, studentMenu, resourceMenu);
                                Console.Read();
                                break;
                            case 2:
                                Return(collection1, studentMenu, resourceMenu);
                                Console.Read();
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region Edit Resource
                    case 5:
                        EditResource(resourceMenu, collection1);
                        Console.Read();
                        break;
                    #endregion
                    #region Exit
                    case 6:
                        try
                        {
                            using (Stream stream = File.Open(file, FileMode.Create))
                            {
                                BinaryFormatter formatter = new BinaryFormatter();
                                formatter.Serialize(stream, resourceList);
                            }
                        }
                        catch (IOException)
                        {
                        }
                        Console.WriteLine("BYE");
                        repeat = false;
                        break;
                    #endregion
                    default:
                        break;
                }
            }
            while (repeat == true);
            #endregion

            Console.Read();
        }
        
        static void Checkout(ResourceCollection collection, StudentMenu studentMenu, ResourceMenu resourceMenu)
        {
            collection.CheckOut(studentMenu, resourceMenu);
            Console.WriteLine("Checkout Successful");
            Console.Read();
        }
        static void Return(ResourceCollection collection, StudentMenu studentMenu, ResourceMenu resourceMenu)
        {
            collection.Return(studentMenu, resourceMenu);
            Console.WriteLine("Return Successful");
            Console.Read();
        }
        static void ViewResources(StringMenu resourceOptionsMenu, ResourceCollection collection, ResourceMenu resourceMenu)
        {
            int resourceChoice = resourceOptionsMenu.RunMenu();
            switch (resourceChoice)
            {
                case 1: //All
                    Console.Clear();
                    collection.ViewAll();
                    Console.Read();
                    break;
                case 2: //Available
                    Console.Clear();
                    collection.ViewAvailable();
                    Console.Read();
                    break;
                case 3: //Checked Out
                    Console.Clear();
                    collection.ViewCheckedOut();
                    Console.Read();
                    break;
                case 4:
                    Console.Clear();
                    bool valid=collection.ViewIndividual(resourceMenu, collection.Items);
                    if (valid==false)
                        Console.WriteLine("Error: not a valid entry\nPress ENTER to return to Main Menu");
                    Console.Read();
                    break;
                default:
                    break;
            }
        }
        static void ViewStudents(List<Student> students)
        {
            Console.Clear();
            Console.WriteLine("STUDENTS:");
            foreach (Student student in students)
            {
                Console.WriteLine(student.FullName);
            }
        }
        static void ViewStudentAccount(StudentMenu studentMenu, List<Resource> resourceList)
        {
            int studentChoice = studentMenu.RunMenu();
            if (studentChoice == -1)
                return;
            Student student = studentMenu.MenuItems[studentChoice];
            student.ViewStudentInfo(resourceList);
        }
        static void EditResource(ResourceMenu resourceMenu, ResourceCollection collection)
        {
            List<string> editOptions = new List<string> { "Title", "Length", "ISBN" };
            StringMenu editMenu = new StringMenu("EDITING OPTIONS", editOptions);
            Console.WriteLine("which resource would you like to edit?");
            int editChoice = resourceMenu.RunMenu();
            Resource resource = resourceMenu.MenuItems[editChoice];
            Console.WriteLine("What property would you like to edit?");
            int editProperty = editMenu.RunMenu();
            switch (editProperty)
            {
                case 1://edit title
                    Console.WriteLine("Enter new Title: ");
                    string newTitle = Console.ReadLine();
                    resource.Title = newTitle;
                    Console.WriteLine($"Title changed to \"{resource.Title}\"");
                    break;
                case 2://edit length
                    Console.WriteLine("Enter new Length: ");
                    int newLength;
                    bool valid1 = int.TryParse(Console.ReadLine(), out newLength) && newLength >= 0;
                    if (valid1)
                    {
                        resource.Length = newLength;
                        Console.WriteLine("Length Changed.");
                    }
                    else
                    { Console.WriteLine("Not a valid length\nPress ENTER to return to Main Menu"); }
                    Console.Read();
                    break;
                case 3://edit ISBN
                    Console.WriteLine("Enter new ISBN: ");
                    int newISBN;
                    bool valid2 = int.TryParse(Console.ReadLine(),out newISBN);
                    if (valid2)
                    {
                        char[] newIsbnAsArray = newISBN.ToString().ToCharArray();
                        bool valid3 = newIsbnAsArray.Length == 13;
                        if (valid3)
                        {
                            resource.ISBN = newISBN;
                            Console.WriteLine("ISBN Changed.");
                        }
                    }
                    else
                    { Console.WriteLine("Not a valid ISBN. ISBN must be 13 characters long.\nPress ENTER to return to Main Menu"); }
                    Console.Read();
                    break;
                default:
                    break;
            }

        }
    }
}
