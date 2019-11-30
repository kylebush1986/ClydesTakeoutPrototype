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
    [Authorize(Roles = "Employee, Admin")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        //private readonly DataContext _context;
        private readonly ILocalDataContext _context;
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
        public IActionResult Index()
        {
            return View(_context.OrderDB.OrderBy(x => x.PickupTime).ToList());
        }
        public IActionResult OrderDetails(ulong id)
        {
            return View(_context.OrderDB.FirstOrDefault(x => x.ID == id));
        }

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

        private string[] CreateOrderReadyEmail(User user, Order order)
        {
            string[] emailArgs = {
                "Your Order is Ready For Pickup!",
                $"Hi {user.FirstName}," +
                $"\nYour order scheduled for {order.PickupTime} is ready for pickup!\n" +
                $"Order Number: {order.ID}\n" +
                $"Items: \n" +
                $"{order.ItemListAsString()}"
            };
            return emailArgs;
        }

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