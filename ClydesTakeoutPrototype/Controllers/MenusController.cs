using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ClydesTakeoutPrototype.Models.MenuModels;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Data;

namespace ClydesTakeoutPrototype.Controllers
{
    /// <summary>
    /// Controller providing Menu related actions.
    /// </summary>
    [Authorize]
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

        /// <summary>
        /// GET Menu Index view.
        /// </summary>
        /// <returns>Menu View</returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            Menu menu = new Menu()
            {
                Items = _context.ItemDB
            };
            return View(menu);
        }

        /// <summary>
        /// GET Entree item details view
        /// </summary>
        /// <param name="id">An Entree ID</param>
        /// <returns>EntreeItem View</returns>
        public IActionResult EntreeItem(ulong id)
        {
            return View(_context.ItemDB.FirstOrDefault(x => x.ID == id));
        }

        /// <summary>
        /// GET Side item details view.
        /// </summary>
        /// <param name="id">A Side ID</param>
        /// <returns>SideItem View</returns>
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

        /// <summary>
        /// GET a Drink item details view.
        /// </summary>
        /// <param name="id">A Drink ID</param>
        /// <returns>DrinkItem View</returns>
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