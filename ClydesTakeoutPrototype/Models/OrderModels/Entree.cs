using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public enum EntreeCategories
    {
        Fried,
        Grilled,
        Salad
    }
    public class Entree : Item
    {
        #region Properties
        public EntreeCategories Category { get; set; }
        public ulong SideID { get; set; }
        public ulong DrinkID { get; set; }
        #endregion


        #region Constructors
        public Entree() : base() { }
        public Entree(EntreeCategories cat, ulong sID, ulong dID, string name, TimeSpan prepTime, string description, float price, string imageURL)
            : base(name, prepTime, description, price, imageURL)
        {
            Category = cat;
            SideID = sID;
            DrinkID = dID;
        }

        #endregion

        #region Methods

        #endregion
    }
}
