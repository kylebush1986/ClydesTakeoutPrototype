using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Models.SystemModels;
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
                if (userID != null)
                {
                    ulong.TryParse(Encoding.ASCII.GetString(userID), out ulong uid);
                    return uid;
                }
                else
                    return 0;
            } 
        }
        
        public OrdersController(ILogger<OrdersController> logger, ILocalDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            float subTotal = 0f;
            Order activeOrder = _context.UserDB.FirstOrDefault(u => u.ID == UserID).ActiveOrder;
            foreach(var item in activeOrder.Items)
            {
                subTotal += item.Price;
            }
            activeOrder.CalculateTotal(subTotal);
            activeOrder.PickupTime = DateTime.Now.AddMinutes(15);
            _context.UserDB.FirstOrDefault(u => u.ID == UserID).ActiveOrder = activeOrder;
            _context.SaveDatabase(_context.UserDB);
            return View(activeOrder);
        }

        public IActionResult RemoveItemFromOrder(string id)
        {
            if (UserID != 0)
            {
                ulong.TryParse(id, out ulong itemID);
                try
                {
                    int usrIndex = _context.UserDB.FindIndex(u => u.ID == UserID);
                    User usr = _context.UserDB[usrIndex];
                    List<Item> items = usr?.ActiveOrder.Items.ToList();
                    items.RemoveAll(i => i.ID == itemID);
                    usr.ActiveOrder.Items = items;
                    _context.UserDB[usrIndex] = usr;
                    _context.SaveDatabase(_context.UserDB);
                }
                catch
                {
                }
            }
            return RedirectToAction("Index", "Orders");
        }

        public IActionResult CancelOrder()
        {
            if (UserID != null) {
                int usrIndex = _context.UserDB.FindIndex(u => u.ID == UserID);
                _context.UserDB[usrIndex].ActiveOrder = new Order();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Orders");
        }

        [HttpPost]
        public IActionResult AddEntreeToOrder([Bind("ID,Type,SpecialInstructions")] Entree entree)
        {
            if (UserID != 0)
            {
                Entree temp = _context.ItemDB.Where(i => i.GetType() == typeof(Entree)).Cast<Entree>().FirstOrDefault(s => s.ID == entree.ID);
                entree.ID = Helpers.Utilities.GenerateGuid();
                entree.Name = temp.Name;
                entree.PrepTime = temp.PrepTime;
                entree.Price = temp.Price;
                _context.UserDB.FirstOrDefault(u => u.ID == UserID).ActiveOrder.Items.Add(entree);
                _context.SaveDatabase(_context.UserDB);
                return RedirectToAction("SideItem", "Menus");
            }
            return RedirectToAction("Logout", "Account");
            
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
            if (UserID != 0)
            {
                Drink temp = _context.ItemDB.Where(i => i.GetType() == typeof(Drink)).Cast<Drink>().FirstOrDefault(s => s.Type == drink.Type);
                drink.ID = Helpers.Utilities.GenerateGuid();
                drink.Name = temp.Name;
                drink.PrepTime = temp.PrepTime;
                drink.Price = temp.Price;
                drink.AddDrinkToSpcInst();

                _context.UserDB.FirstOrDefault(u => u.ID == UserID).ActiveOrder.Items.Add(drink);
                _context.SaveDatabase(_context.UserDB);

                return RedirectToAction("Index", "Menus");
            }

            return RedirectToAction("Logout", "Account");
        }

        [HttpPost]
        public IActionResult SubmitOrder([Bind("ID,PickupTime")] Order finalOrder)
        {
            if (UserID != 0)
            {
                User user = _context.UserDB.FirstOrDefault(u => u.ID == UserID);
                user.ActiveOrder.PickupTime = finalOrder.PickupTime;
                _context.OrderDB.Add(user.CommitActiveOrder());
                _context.UserDB.RemoveAll(x => x.ID == UserID);
                _context.UserDB.Add(user);

                _context.SaveDatabase(_context.UserDB);
                _context.SaveDatabase(_context.OrderDB);

                return RedirectToAction("OrderSuccess", "Orders");
            }
            return View();
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

       

    }
}