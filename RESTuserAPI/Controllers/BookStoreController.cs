using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTuserAPI.Models;
using RESTuserAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTuserAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        static List<BookStore> bookStore = new List<BookStore>();

        // GET: api/<BookStoreController>
        [HttpGet]
        public List<BookStore> Get()
        {
            if (bookStore.Count == 0)
            {
                bookStore = JsonBookStore.LoadJsonFile<BookStore>("BookList.json");

                if (bookStore == null)
                {
                    bookStore = new List<BookStore>();
                }
            }

            return bookStore;  /*new string[] { "value1", "value2" };*/
        }

        // GET api/<BookStoreController>/5
        [HttpGet("{id}/FindBook")]
        public BookStore Get(int id, string bookname)
        {
            return bookStore.Find(a => a.AuthorID == id && a.Title == bookname);
        }

        // POST api/<BookStoreController>
        [HttpPost]
        public void Post([FromBody] BookStore value)
        {
            if (bookStore.Count == 0)
            {
                bookStore = JsonBookStore.LoadJsonFile<BookStore>("BookList.json");

                if (bookStore == null)
                {
                    bookStore = new List<BookStore>();
                }
            }
            bookStore.Add(value);
            JsonBookStore.SaveJsonFile<BookStore>(bookStore, "BookList.json");
        }

        // PUT api/<BookStoreController>/5
        [HttpPut("{id}")]
        public void Put(int id, string bookname, [FromBody] BookStore value)
        {
            BookStore bookUpdate = bookStore.Find(x => x.AuthorID == id && x.Title == bookname);

            if (bookUpdate != null)
            {
                if (value.Price != -1)
                    bookUpdate.Price = value.Price;
            }

            JsonBookStore.SaveJsonFile<BookStore>(bookStore, "BookList.json");
        }

        // DELETE api/<BookStoreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id, string bookname)
        {
            BookStore bookDelete = bookStore.Find(x => x.AuthorID == id && x.Title == bookname);

            if (bookDelete != null)
            {
                bookStore.Remove(bookDelete);
            }

            JsonBookStore.SaveJsonFile<BookStore>(bookStore, "BookList.json");
        }
    }
}
