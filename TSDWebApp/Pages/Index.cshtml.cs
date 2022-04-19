using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RESTuserAPI.Models;
using RESTuserAPI.Services;
using TSDWebApp.Models;

namespace TSDWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileServices jsonService;

        public List<User> Users { get; set; }
        public List<Monkey> Monkeys { get; set; }

        public IndexModel(ILogger<IndexModel> logger, JsonFileServices json)
        {
            _logger = logger;
            jsonService = json;
        }

        public void OnGet()
        {
            Users = jsonService.LoadJsonFile<User>("data\\UserList.json");

            Monkeys = jsonService.LoadJsonFile<Monkey>("data\\monkeys.json");
        }
    }
}
