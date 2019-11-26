using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClydesTakeoutPrototype.Models.SystemModels;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Models.MenuModels;
using ClydesTakeoutPrototype.Helpers;
using Newtonsoft.Json;

namespace ClydesTakeoutPrototype.Data
{
    public class LocalDataContext : ILocalDataContext
    {
        public List<User> UserDB { get; set; }
        public List<Order> OrderDB { get; set; }
        public List<Item> ItemDB { get; set; }
        private readonly string _filePath = Directory.GetCurrentDirectory() + "/Data/";
        
        public LocalDataContext()
        {
            UserDB = new List<User>();
            OrderDB = new List<Order>();
            ItemDB = new List<Item>();

            // Load User Database if it exists. If not then create one.
            if (File.Exists(_filePath + "UserDB.json"))
            {
                UserDB = LoadDatabase(UserDB);
            }
            else
            {
                SaveDatabase(UserDB);
            }

            // Load Orders Database if it exists. If not then create one.
            if (File.Exists(_filePath + "OrderDB.json"))
            {
                OrderDB = LoadDatabase(OrderDB);
            }
            else
            {
                SaveDatabase(OrderDB);
            }

            // Load Menu Database if it exists. If not then create one.
            if (File.Exists(_filePath + "ItemDB.json"))
            {
                ItemDB = LoadDatabase(ItemDB);
            }
            else
            {
                ItemDB = Helpers.MenuBuilder.MenuItems;
                SaveDatabase(ItemDB);
            }
        }
        ~LocalDataContext()
        {
            SaveDatabase(UserDB);
            SaveDatabase(OrderDB);
            SaveDatabase(ItemDB);
        }
        private List<T> LoadDatabase<T>(List<T> Database)
        {
            try
            {
                using (Stream stream = File.Open(_filePath + Database.GetType().GetGenericArguments().Single().ToString().Split(".").Last() + "DB.json", FileMode.Open))
                {
                    Database = stream.CreateFromJsonStream<List<T>>().ConvertAll(x => (T)x);
                }
                return Database;
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoadUsers: " + ex.Message);
                throw;
            }
        }
        public void SaveDatabase<T>(List<T> Database)
        {
            try
            {
                using (Stream stream = File.Open(_filePath + Database.GetType().GetGenericArguments().Single().ToString().Split(".").Last() + "DB.json", FileMode.Create))
                {
                    JsonHelpers.Serialize(Database, stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SaveUsers: " + ex.Message);
                throw;
            }
        }
    }
}
