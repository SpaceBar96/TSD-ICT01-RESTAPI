using Newtonsoft.Json;
using RESTuserAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TSDWebApp.Services
{
    public class HttpService
    {
        private HttpClient httpClient = new HttpClient();

        public async void PostNewUser(User user)
        {
            string jsonObj = JsonConvert.SerializeObject(user, Formatting.Indented);
            StringContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            //string url = @"http://192.168.223.141/TSDWebApp/dbuser";
            string url = @"https://localhost:44367/dbuser";

            HttpResponseMessage response = await httpClient.PostAsync(url, content);
        }

        public async void UpdateUser(User user)
        {
            string jsonObj = JsonConvert.SerializeObject(user, Formatting.Indented);
            StringContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            //string url = @"http://192.168.223.141/TSDWebApp/dbuser";
            string url = @"https://localhost:44367/dbuser";

            HttpResponseMessage response = await httpClient.PutAsync(url, content);
        }

        public async void DeleteUser(User user)
        {
            string jsonObj = JsonConvert.SerializeObject(user, Formatting.Indented);
            StringContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            //string url = @"http://192.168.223.141/TSDWebApp/dbuser";
            string url = @"https://localhost:44367/dbuser";

            HttpResponseMessage response = await httpClient.PutAsync(url, content);
        }

        public async Task<string> GetUserList()
        {
            //string url = @"http://192.168.223.141/TSDWebApp/dbuser/1";
            string url = @"https://localhost:44367/dbuser/1";

            HttpResponseMessage response = await httpClient.GetAsync(url);

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
