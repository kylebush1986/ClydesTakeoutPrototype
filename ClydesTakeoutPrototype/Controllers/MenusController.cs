using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ClydesTakeoutPrototype.Models.MenuModels;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Data;

namespace ClydesTakeoutPrototype.Controllers
{
    public class MenusController : Controller
    {
        private readonly ILogger<MenusController> _logger;
        //private readonly DataContext _context;
        private readonly ILocalDataContext _context;

        public MenusController(ILogger<MenusController> logger, ILocalDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            Menu menu = new Menu()
            {
                Items = _context.ItemDB
            };
            return View(menu);
        }

        public IActionResult EntreeItem(ulong id)
        {
            return View(_context.ItemDB.FirstOrDefault(x => x.ID == id));
        }
        public IActionResult SideItem(ulong id)
        {
            if (id == 0)
            {
                return View(new Side()
                {
                    ID = 0
                });
            }
            else
            {
                return View(_context.ItemDB.FirstOrDefault(x => x.ID == id));
            }
        }
        public IActionResult DrinkItem(ulong id)
        {
            if(id == 0)
            {
                return View(new Drink()
                {
                    ID = 0
                });
            }
            else
            {
                return View(_context.ItemDB.FirstOrDefault(x => x.ID == id));
            }
        }
    }
}