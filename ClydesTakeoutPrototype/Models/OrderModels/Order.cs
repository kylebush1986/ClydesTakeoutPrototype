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
        [DataType(DataType.Currency)]
        public float Total { get; set; }
        public ICollection<Item> Items { get; set; }

        #endregion

        #region Constructors
        public Order() 
        {
            ID = Helpers.Utilities.GenerateGuid();
            Items = new List<Item>();
        }
        public Order(User user)
        {
            ID = Helpers.Utilities.GenerateGuid();
            Items = new List<Item>();
            User = user;
        }
        public Order(User user, DateTime ptime, float subTotal, ICollection<Item> items)
        {
            ID = Helpers.Utilities.GenerateGuid();
            User = user;
            PickupTime = ptime;
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
