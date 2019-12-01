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
    /// <summary>
    /// Controller providing order related functions.
    /// </summary>
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        //private readonly DataContext _context;
        private readonly ILocalDataContext _context;
        /// <summary>
        /// Get UserID from Cookie
        /// </summary>
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

        /// <summary>
        /// GET An Order confirmation view.
        /// </summary>
        /// <returns>Order Index View</returns>
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

        /// <summary>
        /// GET action that removes an item from an Active Order.
        /// </summary>
        /// <param name="id">An Item ID</param>
        /// <returns>Refresh View</returns>
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

        /// <summary>
        /// GET action that cancels an Active Order.
        /// </summary>
        /// <returns>Redirect to Menu Index</returns>
        public IActionResult CancelOrder()
        {
            if (UserID != 0) {
                int usrIndex = _context.UserDB.FindIndex(u => u.ID == UserID);
                _context.UserDB[usrIndex].ActiveOrder = new Order();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Orders");
        }

        /// <summary>
        /// POST action that adds an Entree to an Active Order.
        /// </summary>
        /// <param name="entree">An Entree</param>
        /// <returns>Redirect to SideItem View</returns>
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

        /// <summary>
        /// POST action that adds a Side to an Active Order.
        /// </summary>
        /// <param name="side">A Side</param>
        /// <returns>Redirect to DrinkItem View</returns>
        [HttpPost]
        public IActionResult AddSideToOrder([Bind("ID,Type,SpecialInstructions")] Side side)
        {
            if (UserID != 0)
            {
                if (side.ID == 0)
                {
                    Side temp = _context.ItemDB.Where(i => i.GetType() == typeof(Side)).Cast<Side>().FirstOrDefault(s => s.Type == side.Type);
                    if (temp == null)
                        return RedirectToAction("DrinkItem", "Menus");
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
                    Side temp = _context.ItemDB.Where(i => i.GetType() == typeof(Side)).Cast<Side>().FirstOrDefault(s => s.ID == side.ID);
                    side.Name = temp.Name;
                    side.PrepTime = temp.PrepTime;
                    side.Price = temp.Price;

                    _context.UserDB.FirstOrDefault(u => u.ID == UserID).ActiveOrder.Items.Add(side);
                    _context.SaveDatabase(_context.UserDB);

                    return RedirectToAction("Index", "Menus");
                }
            }
            return RedirectToAction("Logout", "Account");
        }

        /// <summary>
        /// POST action to add a Drink to an Active Order.
        /// </summary>
        /// <param name="drink">A Drink</param>
        /// <returns>Redirect to Menu Index View</returns>
        [HttpPost]
        public IActionResult AddDrinkToOrder([Bind("ID,Type,DrinkSize")] Drink drink)
        {
            if (UserID != 0)
            {
                if (drink.ID == 0)
                {
                    Drink temp = _context.ItemDB.Where(i => i.GetType() == typeof(Drink)).Cast<Drink>().FirstOrDefault(s => s.Type == drink.Type);
                    if (temp == null)
                        return RedirectToAction("Index", "Menus");
                    drink.ID = Helpers.Utilities.GenerateGuid();
                    drink.Name = temp.Name;
                    drink.PrepTime = temp.PrepTime;
                    drink.Price = temp.Price;
                    drink.AddDrinkToSpcInst();
                }
                else
                {
                    Drink temp = _context.ItemDB.Where(i => i.GetType() == typeof(Drink)).Cast<Drink>().FirstOrDefault(x => x.ID == drink.ID);
                    if (temp == null)
                        return RedirectToAction("Index", "Menus");
                    drink.ID = Helpers.Utilities.GenerateGuid();
                    drink.Name = temp.Name;
                    drink.PrepTime = temp.PrepTime;
                    drink.Price = temp.Price;
                    drink.AddDrinkToSpcInst();
                }

                _context.UserDB.FirstOrDefault(u => u.ID == UserID).ActiveOrder.Items.Add(drink);
                _context.SaveDatabase(_context.UserDB);

                return RedirectToAction("Index", "Menus");
            }

            return RedirectToAction("Logout", "Account");
        }

        /// <summary>
        /// POST action to submit an order.
        /// </summary>
        /// <param name="finalOrder">An Order</param>
        /// <returns>Redirect to Order Success View</returns>
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

                string[] emailArgs = CreateSubmitOrderEmail(user, finalOrder);
                NotificationService.SendEmail(user.Email, emailArgs[0], emailArgs[1]);

                return RedirectToAction("OrderSuccess", "Orders");
            }
            return View();
        }

        /// <summary>
        /// GET Order Success view.
        /// </summary>
        /// <returns>OrderSuccess View</returns>
        public IActionResult OrderSuccess()
        {
            return View();
        }

        /// <summary>
        /// Create subject and body arguments for an email to notify a customer their order has been received.
        /// </summary>
        /// <param name="user">A user</param>
        /// <param name="order">An order</param>
        /// <returns>Email arguments</returns>
        private string[] CreateSubmitOrderEmail(User user, Order order)
        {
            return new string[]
            {
                "Your Order has been Received!",
                $"{user.FirstName},\n" +
                $"Your order set for {order.PickupTime} has been received!\n\n" +
                $"Order Number: {order.ID}\n\n" +
                $"You will receive an email notification when your order is complete."
            };
        }

    }
}