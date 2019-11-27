using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        public static Order CurrentOrder { get; set; }
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
        public IActionResult AddToOrder()
        {
            if(CurrentOrder == null)
            {
                // Need to add user to Order on creation
                CurrentOrder = new Order();
            }
            ulong id = Convert.ToUInt64(Request.Form["ID"]);
            var entree = Helpers.MenuBuilder.MenuItems.FirstOrDefault(x => x.ID == id);
            var dID = Request.Form["DrinkID"];
            var sID = Request.Form["SideID"];
            CurrentOrder.Items.Add(entree);

           
            // Return to the Menu
            return RedirectToAction("Index", "Menus");
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}