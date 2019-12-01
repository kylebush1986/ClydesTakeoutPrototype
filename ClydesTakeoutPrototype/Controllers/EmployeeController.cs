using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Models.SystemModels;
using ClydesTakeoutPrototype.Data;
using ClydesTakeoutPrototype.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ClydesTakeoutPrototype.Controllers
{
    /// <summary>
    /// Controller providing access to Employee functions
    /// </summary>
    [Authorize(Roles = "Employee, Admin")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        //private readonly DataContext _context;
        private readonly ILocalDataContext _context;
        /// <summary>
        /// Get UserID from Cookie
        /// </summary>
        private ulong UserID
        {
            get
            {
                HttpContext.Session.TryGetValue("UserID", out byte[] userID);
                if (userID != null)
                {
                    ulong.TryParse(Encoding.ASCII.GetString(userID), out ulong uid);
                    return uid;
                }
                else
                    return 0;
            }
        }

        public EmployeeController(ILogger<EmployeeController> logger, ILocalDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// GET Index view of orders in queue.
        /// </summary>
        /// <returns>Order View</returns>
        public IActionResult Index()
        {
            return View(_context.OrderDB.OrderBy(x => x.PickupTime).ToList());
        }

        /// <summary>
        /// GET Order details for an order in the queue.
        /// </summary>
        /// <param name="id">An order ID</param>
        /// <returns>Order Details View</returns>
        public IActionResult OrderDetails(ulong id)
        {
            return View(_context.OrderDB.FirstOrDefault(x => x.ID == id));
        }

        /// <summary>
        /// GET order ready action to send notification email to customer
        /// </summary>
        /// <param name="id">An order ID</param>
        /// <returns>Redirect to Order Index</returns>
        public IActionResult OrderReady(ulong id)
        {
            Order order = _context.OrderDB.SingleOrDefault(o => o.ID == id);

            // Send message telling user their order is ready for pickup
            string[] emailArgs = CreateOrderReadyEmail(_context.UserDB.FirstOrDefault(u => u.ID == order.UserID), order);
            NotificationService.SendEmail(_context.UserDB.FirstOrDefault(u => u.ID == order.UserID)?.Email, emailArgs[0], emailArgs[1]);

            _context.OrderDB.Remove(_context.OrderDB.FirstOrDefault(o => o.ID == id));
            _context.SaveDatabase(_context.OrderDB);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET reject order action to send a notification email to the customer. 
        /// </summary>
        /// <param name="id">An order ID</param>
        /// <returns>Redirect to Order Index</returns>
        public IActionResult RejectOrder(ulong id)
        {
            Order order = _context.OrderDB.SingleOrDefault(o => o.ID == id);

            // Send message telling user their order has been rejected
            string[] emailArgs = CreateOrderRejectedEmail(_context.UserDB.FirstOrDefault(u => u.ID == order.UserID), order);
            NotificationService.SendEmail(_context.UserDB.FirstOrDefault(u => u.ID == order.UserID)?.Email, emailArgs[0], emailArgs[1]);

            _context.OrderDB.Remove(_context.OrderDB.FirstOrDefault(u => u.ID == id));
            _context.SaveDatabase(_context.OrderDB);
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Create the subject and body for an email message to notify a user that an order is ready for pickup.
        /// </summary>
        /// <param name="user">A user</param>
        /// <param name="order">An order</param>
        /// <returns>Subject and body arguments for an email</returns>
        private string[] CreateOrderReadyEmail(User user, Order order)
        {
            string[] emailArgs = {
                "Your Order is Ready For Pickup!",
                $"Hi {user.FirstName}," +
                $"\nYour order scheduled for {order.PickupTime} is ready for pickup!\n\n" +
                $"Order Number: {order.ID}\n\n" +
                $"Items: \n" +
                $"{order.ItemListAsString()}\n\n" +
                $"Your order total is ${order.Total.ToString("c2")}. Please be prepared to pay for your food at Clyde's when you arrive."
            };
            return emailArgs;
        }

        /// <summary>
        /// Create the subject and body for an email to notify a customer their order has been rejected. 
        /// </summary>
        /// <param name="user">A user</param>
        /// <param name="order">An order</param>
        /// <returns>Subject and body arguments for an email</returns>
        private string[] CreateOrderRejectedEmail(User user, Order order)
        {
            string[] emailArgs = {
                "Your Order has been Rejected",
                $"Hi {user.FirstName}," +
                $"\nWe apologize, but your order has been rejected by Clyde's staff. "
            };
            return emailArgs;
        }
    }
}