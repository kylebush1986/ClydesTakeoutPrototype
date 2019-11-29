using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public enum DrinkType
    {
        None,
        Coke,
        Pepsi,
        Lemonade,
        Sprite,
        DrPepper,
        Water,
    }

    public enum Size
    {
        None,
        Small,
        Medium,
        Large,
    }
    public class Drink : Item
    {
        #region Properties
        [Display(Name = "Drink Type")]
        public DrinkType Type { get; set; }
        [Display(Name = "Drink Size")]
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
