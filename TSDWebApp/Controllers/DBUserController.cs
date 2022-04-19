using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTuserAPI.Models;
using TSDWebApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TSDWebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DBUserController : ControllerBase
    {
        private static DatabaseService DBservice;

        public DBUserController(DatabaseService databaseService)
        {
            DBservice = databaseService;
        }

        // GET: api/<DBUserController>
        [HttpGet]
        public int Get()
        {
            int userCount = DBservice.GetUserCount();
            return userCount;// new string[] { "value1", "value2" };
        }

        // GET api/<DBUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            switch (id)
            {
                case 1:
                    return DBservice.GetUserList();

                case 2:
                    return string.Empty;
            }

            return "value";
        }

        // POST api/<DBUserController>
        [HttpPost]
        public void Post([FromBody] User value)
        {
            DBservice.InsertNewUser(value);
        }

        // PUT api/<DBUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
            DBservice.Update(value);
        }

        // DELETE api/<DBUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] User value)
        {
            DBservice.Delete(value);
        }
    }
}
