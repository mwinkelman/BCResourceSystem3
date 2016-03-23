using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
namespace BCResourceSystem3
{
    [Serializable]
    public class DVD : Resource
    {
        #region constructors
        public DVD()
        { }
        public DVD(string title, int length)
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 13; i++)
            {
                sb.Append(random.Next(10).ToString());
            }
            Title = title;
            ISBN = long.Parse(sb.ToString());
            Length = length;
            Status = null;
        }
        #endregion
        #region methods
        public override void ViewTitle()
        {
            string availability = "";
            if (Status is Student)
                availability = "Checked Out";
            if (Status == null)
                availability = "Available";

            Console.WriteLine($"Title: {Title}\nISBN: {ISBN}\nLength: {Length} Minutes\nStatus: {availability}");
        }
        
        #endregion
    }
}
