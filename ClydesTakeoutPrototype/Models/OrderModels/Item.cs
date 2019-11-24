using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public abstract class Item
    {
        #region Properties
        public ulong ID { get; set; }
        public string Name { get; set; }
        public TimeSpan PrepTime { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        public string ImageURL { get; set; }
        public ulong ContainerID { get; set; }
        public string SpecialInstructions { get; set; }
        #endregion

        #region Constructors
        public Item() 
        {
            ID = Helpers.Utilities.GenerateGuid();
        }
        public Item(string name, TimeSpan prepTime, string description, float price, string imageURL, string spInst)
        {
            ID = Helpers.Utilities.GenerateGuid();
            Name = name;
            PrepTime = prepTime;
            Description = description;
            Price = price;
            ImageURL = imageURL;
            SpecialInstructions = spInst;
        }
        #endregion

        #region Methods

        #endregion
    }
}
