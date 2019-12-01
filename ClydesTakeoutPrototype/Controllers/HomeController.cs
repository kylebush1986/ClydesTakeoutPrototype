using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClydesTakeoutPrototype.Models;
using ClydesTakeoutPrototype.Models.SystemModels;
using ClydesTakeoutPrototype.Data;

namespace ClydesTakeoutPrototype.Controllers
{
    /// <summary>
    /// A controller providing access to views on the home page.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly DataContext _context;
        private readonly ILocalDataContext _context;

        public HomeController(ILogger<HomeController> logger, ILocalDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// GET Landing page View
        /// </summary>
        /// <returns>Home view</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET Privacy details View
        /// </summary>
        /// <returns>Privacy view</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// GET Error view
        /// </summary>
        /// <returns>Error view</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
