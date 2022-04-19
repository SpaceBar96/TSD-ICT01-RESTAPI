using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RESTuserAPI.Models;
using TSDWebApp.Services;

namespace TSDWebApp.Pages
{
    public class UserListModel : PageModel
    {
        private DatabaseService dbService { get; set; }
        public UserListModel(DatabaseService service)
        {
            dbService = service;
        }

        [BindProperty]

        public User usr { get; set; }
        public List<User> usrList { get; set; }
        public User Usr { get; private set; }

        public void OnGet()
        {
            string list = dbService.GetUserList();
            usrList = JsonConvert.DeserializeObject<List<User>>(list);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                dbService.Update(usr);              
            }

            return RedirectToPage();
        }
    }
}
