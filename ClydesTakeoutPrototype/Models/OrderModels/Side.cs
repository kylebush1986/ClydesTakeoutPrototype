using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public class Side : Item
    {
        #region Properties
        public SideType Type { get; set; }
        #endregion

        #region Constructors
        public Side() : base() { }
        #endregion

        #region Methods
        public enum SideType
        {
            Fries,
            SweetPotatoFries,
            Chips,
            Salad,
            None
        }
        #endregion
    }
}
