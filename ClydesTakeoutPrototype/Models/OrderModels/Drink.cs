using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public class Drink : Item
    {
        #region Properties
        public DrinkType Type { get; set; }
        public Size DrinkSize { get; set; }
        #endregion

        #region Constructors
        public Drink() : base() { }
        #endregion

        #region Methods
        public enum DrinkType
        {
            Coke,
            Pepsi,
            Lemonade,
            Sprite,
            DrPepper,
            Water,
            None
        }

        public enum Size
        {
            Small,
            Medium,
            Large,
            None
        }
        #endregion
    }
}
