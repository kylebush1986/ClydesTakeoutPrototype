using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    /// <summary>
    /// An Entree.
    /// </summary>
    public class Entree : Item
    {
       
        #region Constructors
        public Entree() : base() { }
        public Entree(string name, TimeSpan prepTime, string description, float price, string imageURL, string spInst)
            : base(name, prepTime, description, price, imageURL, spInst)
        {
            
        }

        #endregion

        #region Methods

        #endregion
    }
}