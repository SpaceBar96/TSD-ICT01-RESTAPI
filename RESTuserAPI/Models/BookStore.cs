using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTuserAPI.Services;

namespace RESTuserAPI.Models
{
    public class BookStore
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }

        public int BookPublish { get => CalcBook(); }

        public int CalcBook()
        {
            int count = BookID;

            if (AuthorID != 0)
            {
                return count;
            }
            else
            {
                return -1;
            }
        }
    }
}
