using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClydesTakeoutPrototype.Models.SystemModels;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Models.MenuModels;

namespace ClydesTakeoutPrototype.Data
{
    /// <summary>
    /// Interface specifying LocalDataContext Property and Method requirements. 
    /// </summary>
    public interface ILocalDataContext
    {
        List<User> UserDB { get; set; }
        List<Order> OrderDB { get; set; }
        List<Item> ItemDB { get; set; }

        void SaveDatabase<T>(List<T> Database);
    }
}
