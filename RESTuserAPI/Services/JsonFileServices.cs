using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RESTuserAPI.Models;

namespace RESTuserAPI.Services
{
    public static class JsonFileServices
    {
        private static StreamReader srArray;

        //Generic Function
        public static void SaveJsonFile<T>(List<T> inputList, string filename) where T : new()
        {
            string listOutput = JsonConvert.SerializeObject(inputList, Formatting.Indented);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            File.WriteAllText(filename, listOutput);
        }
        //public static void SaveJsonFile(List<User> userList)
        //{
        //    string listOutput = JsonConvert.SerializeObject(userList, Formatting.Indented);

        //    if (File.Exists("UserList.json"))
        //    {
        //        File.Delete("UserList.json");
        //    }

        //    File.WriteAllText("UserList.json", listOutput);
        //}
        public static List<T> LoadJsonFile<T>(string jsonFilename) where T : new()
        {
            try
            {
                srArray = new StreamReader(jsonFilename);
                string outputFromFile = srArray.ReadToEnd();

                List<T> usrArray = JsonConvert.DeserializeObject<List<T>>(outputFromFile);

                srArray.Close();

                return usrArray;
            }
            catch (Exception)
            {
                return null;
                /*throw*/
                ;
            }
        }

        //public static List<User> LoadJsonFile(string jsonFileName)
        //{
        //    try
        //    {
        //        srArray = new StreamReader(jsonFileName);
        //        string outputFromFile = srArray.ReadToEnd();

        //        List<User> usrArray = JsonConvert.DeserializeObject<List<User>>(outputFromFile);

        //        srArray.Close();

        //        return usrArray;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //        /*throw*/;
        //    }
        //}
    }
}
