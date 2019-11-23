using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClydesTakeoutPrototype.Models.MenuModels;

namespace ClydesTakeoutPrototype.Controllers
{
    public class MenusController : Controller
    {
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