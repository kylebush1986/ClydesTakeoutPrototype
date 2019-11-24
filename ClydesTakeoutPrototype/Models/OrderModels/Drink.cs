using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
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
    public class Drink : Item
    {
        #region Properties
        public DrinkType Type { get; set; }
        public Size DrinkSize { get; set; }
        #endregion

        #region Constructors
        public Drink() : base() { }
        public Drink(DrinkType type, Size size, string name, TimeSpan prepTime, string description, float price, string imageURL, string spInst)
            : base(name, prepTime, description, price, imageURL, spInst)
        {
            Type = type;
            DrinkSize = size;
        }
        
        #endregion

        #region Methods
        
        #endregion
    }
}
