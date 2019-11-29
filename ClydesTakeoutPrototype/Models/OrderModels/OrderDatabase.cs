using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public class OrderDatabase
    {
        public List<Order> AllOrders { get; set; }

        public OrderDatabase(List<Order> orders)
        {
            AllOrders = orders;
        }
    }
}
