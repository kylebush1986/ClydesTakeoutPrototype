using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClydesTakeoutPrototype.Models.OrderModels;

namespace ClydesTakeoutPrototype.Models.MenuModels
{
    public class Menu
    {
        #region Properties
        public ulong ID { get; set; }
        public ICollection<Item> Items { get; set; }

        #endregion

        #region Constructors
        public Menu() { }

        #endregion

        #region Methods

        #endregion
    }
}
