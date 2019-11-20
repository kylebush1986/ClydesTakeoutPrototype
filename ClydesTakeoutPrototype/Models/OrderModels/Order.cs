using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClydesTakeoutPrototype.Models.SystemModels;
using System.ComponentModel.DataAnnotations;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public class Order
    {
        #region Properties
        public ulong ID { get; set; }
        public User User { get; set; }
        public DateTime PickupTime { get; set; }
        public string SpecialInstructions { get; set; }
        [DataType(DataType.Currency)]
        public float Total { get; set; }
        public ICollection<Item> Items { get; set; }

        #endregion

        #region Constructors
        public Order() { }
        #endregion

        #region Methods
        
        #endregion
    }
}
