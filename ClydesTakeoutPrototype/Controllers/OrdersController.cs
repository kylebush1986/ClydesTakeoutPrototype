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
            return RedirectToAction("Index", "Menus");
        }

        [HttpPost]
        public IActionResult AddSideToOrder([Bind("ID,Type,SpecialInstructions")] Side side)
        {
            if(side.ID == 0)
            {
                Side temp = _context.ItemDB.Where(i => i.GetType() == typeof(Side)).Cast<Side>().FirstOrDefault(s => s.Type == side.Type);
                side.ID = Helpers.Utilities.GenerateGuid();
                side.Name = temp.Name;
                side.PrepTime = temp.PrepTime;
                side.Price = temp.Price;


                return RedirectToAction("DrinkItem", "Menus");
            }
            else
            {
                
                return RedirectToAction("Index", "Menus");
            }
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