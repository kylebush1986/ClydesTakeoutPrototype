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
        public Order() 
        {
            ID = Helpers.Utilities.GenerateGuid();
        }
        public Order(User user, DateTime ptime, string spInst, float subTotal, ICollection<Item> items)
        {
            ID = Helpers.Utilities.GenerateGuid();
            User = user;
            PickupTime = ptime;
            SpecialInstructions = spInst;
            CalculateTotal(subTotal);
            Items = items;
        }
        #endregion

        #region Methods
        public void CalculateTotal(float subTotal)
        {
            float taxRate = 1.0825f;
            Total = subTotal + (subTotal * taxRate);
        }
        #endregion
    }
}
