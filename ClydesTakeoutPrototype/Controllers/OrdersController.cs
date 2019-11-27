using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Data;
using ClydesTakeoutPrototype.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ClydesTakeoutPrototype.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        //private readonly DataContext _context;
        private readonly ILocalDataContext _context;
        private ulong UserID { 
            get {
                HttpContext.Session.TryGetValue("UserID", out byte[] userID);
                ulong.TryParse(Encoding.ASCII.GetString(userID), out ulong uid);
                return uid;
            } 
        }
        
        public OrdersController(ILogger<OrdersController> logger, ILocalDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEntreeToOrder([Bind("ID,Type,SpecialInstructions")] Entree entree)
        {
            // Add Entree to Order
            return RedirectToAction("SideItem", "Menus");
        }

        [HttpPost]
        public IActionResult AddSideToOrder([Bind("ID,Type,SpecialInstructions")] Side side)
        {
            if (UserID != 0)
            {
                if (side.ID == 0)
                {
                    Side temp = _context.ItemDB.Where(i => i.GetType() == typeof(Side)).Cast<Side>().FirstOrDefault(s => s.Type == side.Type);
                    side.ID = Helpers.Utilities.GenerateGuid();
                    side.Name = temp.Name;
                    side.PrepTime = temp.PrepTime;
                    side.Price = temp.Price;

                    _context.UserDB.FirstOrDefault(u => u.ID == UserID).ActiveOrder.Items.Add(side);
                    _context.SaveDatabase(_context.UserDB);

                    return RedirectToAction("DrinkItem", "Menus");
                }
                else
                {
                    side.ID = Helpers.Utilities.GenerateGuid();

                    _context.UserDB.FirstOrDefault(u => u.ID == UserID).ActiveOrder.Items.Add(side);
                    _context.SaveDatabase(_context.UserDB);

                    return RedirectToAction("Index", "Menus");
                }
            }
            return RedirectToAction("Logout", "Account");
        }

        [HttpPost]
        public IActionResult AddDrinkToOrder([Bind("ID,Type,DrinkSize")] Drink drink)
        {
            return RedirectToAction("Index", "Menus");
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}