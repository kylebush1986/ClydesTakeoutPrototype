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
    /// <summary>
    /// Handles the connection with User, Order, and Items repository files. Serialized objects are stored in the file repositories.
    /// Managed by the dependecy injection container. 
    /// </summary>
    public class LocalDataContext : ILocalDataContext
    {
        /// <summary>
        /// User repository
        /// </summary>
        public List<User> UserDB { get; set; }
        /// <summary>
        /// Order repository
        /// </summary>
        public List<Order> OrderDB { get; set; }
        /// <summary>
        /// Item repository
        /// </summary>
        public List<Item> ItemDB { get; set; }

        private readonly string _filePath = Directory.GetCurrentDirectory() + "/Data/";
        
        /// <summary>
        /// Default constructor called by the Dependency Injection container when the first
        /// request is received. 
        /// </summary>
        public LocalDataContext()
        {
            // Initialize repository lists
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
                CreateDefaultUsers();
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

        /// <summary>
        /// Loads a list database from local file repository.
        /// </summary>
        /// <typeparam name="T">Type of repository objects</typeparam>
        /// <param name="Database">A list</param>
        /// <returns>The database list</returns>
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

        /// <summary>
        /// Saves a list database to a local file repository.
        /// </summary>
        /// <typeparam name="T">The type of objects in the database</typeparam>
        /// <param name="Database">The database</param>
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

        /// <summary>
        /// Create default admin and employee users and add them to the User repository.
        /// </summary>
        public void CreateDefaultUsers()
        {
            UserDB.Add(new User("admin", "", "admin@clydes.com", "test", Permissions.Admin));
            UserDB.Add(new User("employee", "", "employee@clydes.com", "test", Permissions.Employee));
        }
    }
}
