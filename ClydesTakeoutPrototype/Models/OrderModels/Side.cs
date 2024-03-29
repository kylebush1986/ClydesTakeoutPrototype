﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClydesTakeoutPrototype.Models.OrderModels
{
    public enum SideType
    {
        None,
        Fries,
        OnionRings,
        Soup,
        Salad,
    }

    /// <summary>
    /// A side.
    /// </summary>
    public class Side : Item
    {
        #region Properties
        [Display(Name = "Side Selection")]
        public SideType Type { get; set; }
        #endregion

        #region Constructors
        public Side() : base() { }
        public Side(SideType type, string name, TimeSpan prepTime, string description, float price, string imageURL, string spInst)
            : base(name, prepTime, description, price, imageURL, spInst)
        {
            Type = type;
        }
        #endregion

        #region Methods
      
        #endregion
    }
}
