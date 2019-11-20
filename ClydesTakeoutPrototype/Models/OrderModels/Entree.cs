using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public class Entree : Item
    {
        #region Properties
        public EntreeCategories Category { get; set; }
        public ulong SideID { get; set; }
        public ulong DrinkID { get; set; }
        #endregion


        #region Constructors
        public Entree() : base() {}

        #endregion

        #region Methods
        public enum EntreeCategories
        {
            Fried,
            Grilled,
            Salad
        }
        #endregion
    }
}
