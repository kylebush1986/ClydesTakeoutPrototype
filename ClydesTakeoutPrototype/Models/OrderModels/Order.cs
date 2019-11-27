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
        public ulong UserID { get; set; }
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
            UserID = user.ID;
        }
        public Order(User user, DateTime ptime, float subTotal, ICollection<Item> items)
        {
            ID = Helpers.Utilities.GenerateGuid();
            UserID = user.ID;
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
