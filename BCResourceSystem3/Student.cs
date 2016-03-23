using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BCResourceSystem3
{
    [Serializable]
    public class Student
    {
        #region FIELDS
        private string firstName;
        private string lastName;
        private List<Resource> resources;
        #endregion

        #region PROPERTIES 
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string FullName
        {
            get { return firstName + " " + lastName; }
        }
        public List<Resource> Resources
        {
            get { return resources; }
            set { resources = value; }
        }
        //public string FilePath
        //{
        //    get { return filePath; }
        //}
        //public List<Resource> Resources
        //{
        //    get { return resources; }
        //    set { resources = value; }
        //}
        #endregion

        #region CONSTRUCTORS
        public Student(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            Random idGenerator = new Random();
        }
        public Student() { }
        #endregion

        #region METHODS      
        public void ViewStudentInfo(List<Resource> resourceList)
        {
            Console.Clear();
            StringBuilder fileSB = new StringBuilder();
            fileSB.AppendLine($"Student: {LastName.ToUpper()}, {FirstName.ToUpper()}");
            fileSB.AppendLine($"Checked Out Resources:");
            foreach(Resource resource in resourceList)
            {
                if (resource.Status == this)
                    fileSB.AppendLine(resource.Title);
            }
            string studentFile = fileSB.ToString();
            Console.WriteLine(studentFile);
        }    
        public static Student ParseStudent(string name)
        {
            if (name == "")
                return null;
            string[] nameArray = name.Split();
            Student student = new Student(nameArray[0], nameArray[1]);
            return student;
        }
        #endregion
    }
}
