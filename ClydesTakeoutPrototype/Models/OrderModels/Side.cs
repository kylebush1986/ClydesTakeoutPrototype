using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public enum SideType
    {
        Fries,
        SweetPotatoFries,
        Chips,
        Salad,
        None
    }
    public class Side : Item
    {
        #region Properties
        public SideType Type { get; set; }
        #endregion

        #region Constructors
        public Side() : base() { }
        public Side(SideType type, string name, TimeSpan prepTime, string description, float price, string imageURL)
            : base(name, prepTime, description, price, imageURL)
        {
            Type = type;
        }
        #endregion

        #region Methods
      
        #endregion
    }
}
