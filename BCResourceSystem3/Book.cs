using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
namespace BCResourceSystem3
{
    [Serializable]
    public class Book : Resource
    {
        #region constructors
        public Book()
        { }
        public Book(string title, int length)
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
    }
}
