using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RESTuserAPI.Services
{
    public class JsonBookStore
    {
        private static StreamReader bsArray;

        public static void SaveJsonFile<T>(List<T> inputlist, string bookstore) where T : new()
        {
            string listOutput = JsonConvert.SerializeObject(inputlist, Formatting.Indented);

            if (File.Exists(bookstore))
            {
                File.Delete(bookstore);
            }

            File.WriteAllText(bookstore, listOutput);
        }

        public static List<T> LoadJsonFile<T>(string jsonBookStore) where T : new()
        {
            try
            {
                bsArray = new StreamReader(jsonBookStore);
                string outputBookStore = bsArray.ReadToEnd();

                List<T> bookArray = JsonConvert.DeserializeObject<List<T>>(outputBookStore);

                bsArray.Close();

                return bookArray;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
