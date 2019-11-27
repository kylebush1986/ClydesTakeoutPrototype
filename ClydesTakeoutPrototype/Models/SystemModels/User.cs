using System;
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
        public ICollection<Order> PendingOrders { get; set; }
        public Permissions UserPermissions { get; set; }
        public Order ActiveOrder { get; set; }
        #endregion

        #region Constructors
        public User() {
            ID = Helpers.Utilities.GenerateGuid();
            ActiveOrder = new Order();
            ActiveOrder.User = this;
            PendingOrders = new List<Order>();
        }

        public User(string firstName, string lastName, string email, string password, Permissions uPerm)
        {
            ID = Helpers.Utilities.GenerateGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = Utilities.GenerateDjb264Hash(password).ToString();
            UserPermissions = uPerm;
            PendingOrders = new List<Order>();
            ActiveOrder = new Order();
            ActiveOrder.User = this;
        }

        #endregion

        #region Methods

        #endregion
    }


}
