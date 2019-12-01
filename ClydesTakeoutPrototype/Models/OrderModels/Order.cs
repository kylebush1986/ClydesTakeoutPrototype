using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClydesTakeoutPrototype.Models.SystemModels;
using System.ComponentModel.DataAnnotations;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    /// <summary>
    /// An order. 
    /// </summary>
    public class Order
    {
        #region Properties
        public ulong ID { get; set; }
        public ulong UserID { get; set; }
        public string CustomerName { get; set; }
        [Display(Name = "Pickup Time")]
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
            CustomerName = user.GetFullName();
        }
        public Order(User user, DateTime ptime, float subTotal, ICollection<Item> items)
        {
            ID = Helpers.Utilities.GenerateGuid();
            UserID = user.ID;
            CustomerName = user.GetFullName();
            PickupTime = ptime;
            CalculateTotal(subTotal);
            Items = items;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calculate total order cost from subtotal.
        /// </summary>
        /// <param name="subTotal">The subtotal</param>
        public void CalculateTotal(float subTotal)
        {
            float taxRate = 1.0825f;
            Total = subTotal * taxRate;
        }

        /// <summary>
        /// Get the time intervals between a given start and end time with the specified intervals.
        /// </summary>
        /// <param name="start">The start time</param>
        /// <param name="end">The end time</param>
        /// <param name="interval">The interval</param>
        /// <returns></returns>
        public List<DateTime> GetTimeIntervals(DateTime start, DateTime end, TimeSpan interval)
        {
            //Your list of intervals
            List<DateTime> intervals = new List<DateTime>();

            //Rounds your start time to the nearest Interval
            start = RoundUp(start, interval);

            //Begin incrementing until you have reached your end Date
            while (start <= end)
            {
                intervals.Add(start);
                start = start.Add(interval);
            }
            //Returns your Intervals
            return intervals;
        }

        /// <summary>
        /// Get a list of this orders item names as a string.
        /// </summary>
        /// <returns>String of order item names</returns>
        public string ItemListAsString()
        {
            string items = ""; 
            foreach(Item i in Items)
            {
                items += i.Name + "\n";
            }
            return items;
        }

        /// <summary>
        /// Rounds a DateTime up to the nearest interval.
        /// </summary>
        /// <param name="dt">A DateTime</param>
        /// <param name="d">A TimeSpan</param>
        /// <returns>The rounded DateTime</returns>
        DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
        }
        #endregion
    }
}
