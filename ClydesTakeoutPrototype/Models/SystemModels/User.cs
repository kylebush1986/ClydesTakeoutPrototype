using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Helpers;

namespace ClydesTakeoutPrototype.Models.SystemModels
{
    public class User
    {
        #region Properties
        public ulong ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Order> PendingOrders { get; set; }
        #endregion

        #region Constructors
        public User() {
            ID = Helpers.Utilities.GenerateGuid();
        }

        public User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = Utilities.GenerateDjb264Hash(password).ToString();
        }

        #endregion

        #region Methods

        #endregion
    }


}
