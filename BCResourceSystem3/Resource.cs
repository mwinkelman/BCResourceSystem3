using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace BCResourceSystem3
{
    [Serializable]
    abstract public class Resource
    {
        #region fields
        private string title;
        private long isbn;
        private int length;
        private Student status;
        #endregion

        #region properties
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public long ISBN
        {
            get { return isbn; }
            set { isbn = value; }
        }
        public int Length
        {
            get { return length; }
            set
            {
                if (value > 0)
                    length = value;
                else
                    Console.WriteLine("Length must be greater than zero pages.");
            }
        }
        public string Type
        {
            get
            {
                if(this is Book)
                { return "Book"; }
                else if(this is DVD)
                { return "DVD"; }
                else
                { return "Magazine"; }
            }
        }

        public Student Status
        {
            get { return status; }
            set
            {
                if (value is Student || value == null)
                    status = value;
            }
        }
        #endregion

        #region constructors
        public Resource()
        { }
        #endregion

        #region methods
        
        public virtual void ViewTitle()
        {
            string availability = "";
            if (Status == null)
                availability = "Available";
            if (Status is Student)
                availability = "Checked Out by " + status;

            Console.WriteLine($"Title: {Title}\nISBN: {ISBN}\nLength: {Length} Pages\nStatus: {availability}");
        }
        public virtual void CheckOut(Student student)
        {
            DateTime checkout = DateTime.Now;
            Status = student;
            DateTime dueDate = checkout.AddDays(3);
            Console.WriteLine($"{Title} has been checked out. It is due back on {dueDate}.");  
        }
        public virtual void Return()
        {
            Status = null;
        }
        #endregion

    }
}
