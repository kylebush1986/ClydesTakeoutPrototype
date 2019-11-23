using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ClydesTakeoutPrototype.Models.MenuModels;
using ClydesTakeoutPrototype.Data;

namespace ClydesTakeoutPrototype.Controllers
{
    public class MenusController : Controller
    {
        private readonly ILogger<MenusController> _logger;
        private readonly DataContext _context;

        public MenusController(ILogger<MenusController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            Menu menu = new Menu();
            return View(menu);
        }

        public IActionResult MenuItem(ulong id)
        {
            return View();
            //return View(Helpers.MenuBuilder.Items.FirstOrDefault(x => x.ID == id));
        }
    }
}