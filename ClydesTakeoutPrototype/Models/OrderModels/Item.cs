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
        #endregion

        #region Constructors
        public Item() { }
        public Item(string name, TimeSpan prepTime, string description, float price, string imageURL)
        {
            Name = name;
            PrepTime = prepTime;
            Description = description;
            Price = price;
            ImageURL = imageURL;
        }
        #endregion

        #region Methods

        #endregion
    }
}
