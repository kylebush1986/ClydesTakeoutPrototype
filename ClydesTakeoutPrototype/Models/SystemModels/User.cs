﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Helpers;

namespace ClydesTakeoutPrototype.Models.SystemModels
{
    public enum Permissions
    {
        Customer,
        Employee,
        Admin
    }

    /// <summary>
    /// A user.
    /// </summary>
    public class User
    {
        #region Properties
        public ulong ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Permissions UserPermissions { get; set; }
        public Order ActiveOrder { get; set; }
        public List<ulong> PendingOrders { get; set; }
        #endregion

        #region Constructors
        public User() {
            ID = Helpers.Utilities.GenerateGuid();
            ActiveOrder = new Order();
            ActiveOrder.UserID = ID;
            PendingOrders = new List<ulong>();
        }

        public User(string firstName, string lastName, string email, string password, Permissions uPerm)
        {
            ID = Helpers.Utilities.GenerateGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = Utilities.GenerateDjb264Hash(password).ToString();
            UserPermissions = uPerm;
            ActiveOrder = new Order();
            ActiveOrder.UserID = ID;
            PendingOrders = new List<ulong>();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Commit an Active Order to the order queue.
        /// </summary>
        /// <returns>The committed order</returns>
        public Order CommitActiveOrder()
        {
            if (ActiveOrder.Items.Any())
            {
                PendingOrders.Add(ActiveOrder.ID);
                Order temp = ActiveOrder;
                ActiveOrder = new Order();
                temp.UserID = ID;
                temp.CustomerName = GetFullName();
                return temp;
            }
            return null;
        }

        /// <summary>
        /// Gets the full name of the user. 
        /// </summary>
        /// <returns>The full name</returns>
        public string GetFullName() => FirstName + " " + LastName;

        #endregion
    }


}
