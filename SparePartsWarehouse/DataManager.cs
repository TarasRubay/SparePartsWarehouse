using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsWarehouse
{
    internal class DataManager
    {
        public string Path { get; private set; } = "";
        public DataManager(string path)
        {
            Path = path;
        }

        
        public void SaveDataJSON(List<UserModel> users)
        {
            try
            {
                //using FileStream fileStream = File.Create(PathJSON);
                string jsonUs = System.Text.Json.JsonSerializer.Serialize(users);
                File.WriteAllText(Path, jsonUs);
                Console.WriteLine($"Save data in {Path}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public List<UserModel> LoadDataJSON()
        {
            List<UserModel> users = new();
            try
            {
                using (StreamReader r = new StreamReader(Path))
                {
                    string json = r.ReadToEnd();
                    users = JsonConvert.DeserializeObject<List<UserModel>>(json);
                }
                foreach (var user in users) user.LastMessage = "empty";
                Console.WriteLine($"Load data from {Path}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return users;
        }
        
    }
}
