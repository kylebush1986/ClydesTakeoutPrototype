using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public enum DrinkType
    {
        None = 1,
        Coke = 2,
        Pepsi = 3,
        Lemonade = 4,
        Sprite = 5,
        [Display(Name = "Dr. Pepper")]
        DrPepper = 6,
        Water = 7,
    }

    public enum Size
    {
        Small = 1,
        Medium = 2,
        Large = 3,
    }
    public class Drink : Item
    {
        #region Properties
        [Display(Name = "Drink")]
        public DrinkType Type { get; set; }
        [Display(Name = "Size")]
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
        public void AddDrinkToSpcInst()
        {
            SpecialInstructions = SpecialInstructions + " Drink Size: " + DrinkSize.ToString();
        }
        
        #endregion
    }
}
