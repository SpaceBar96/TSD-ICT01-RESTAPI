using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTuserAPI;
using RESTuserAPI.Models;
using RESTuserAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTuserAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        static List<User> usrNew = new List<User>();

        private JsonFileServices jsonService { get; }

        public UserController(JsonFileServices json)
        {
            jsonService = json;
        }

        [HttpGet]
        public List<User> Get()
        {
            if (usrNew.Count == 0)
            {
                usrNew = jsonService.LoadJsonFile<User>("data\\UserList.json");

                if (usrNew == null)
                {
                    usrNew = new List<User>();
                }
            }

            return usrNew; //new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return usrNew.Find(x => x.UserID == id);
        }

        [HttpGet]
        [Route("byname")]
        public User Get(string name)
        {
            return usrNew.Find(x => x.Name.ToUpper() == name.ToUpper());
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] User value)
        {
            if (usrNew.Count == 0)
            {
                usrNew = jsonService.LoadJsonFile<User>("data\\UserList.json");

                if (usrNew == null)
                {
                    usrNew = new List<User>();
                }
            }

            int MaxId = usrNew.Max(x => x.UserID);
            MaxId++;
            value.UserID = MaxId;

            usrNew.Add(value);
            jsonService.SaveJsonFile<User>(usrNew, "data\\UserList.json");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, User value)
        {        
            int existingUser = usrNew.FindIndex(x => x.UserID == id);

            if (existingUser == -1) return;
            if (value.Name != null)
            {
                if (value.Name != null)
                usrNew.ElementAt(existingUser).Name = value.Name;

                if (value.NRIC != null)
                usrNew.ElementAt(existingUser).NRIC = value.NRIC;

                if (value.DOB != null)
                usrNew.ElementAt(existingUser).DOB = value.DOB;
            }

            jsonService.SaveJsonFile<User>(usrNew, "data\\UserList.json");
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User existingUser = usrNew.Find(x => x.UserID == id);

            if (existingUser != null)
            {
                usrNew.Remove(existingUser);
            }

            jsonService.SaveJsonFile<User>(usrNew, "data\\UserList.json");
        }
    }
}
