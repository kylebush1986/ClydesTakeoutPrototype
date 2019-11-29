using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Models.SystemModels;
using ClydesTakeoutPrototype.Data;
using ClydesTakeoutPrototype.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ClydesTakeoutPrototype.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        //private readonly DataContext _context;
        private readonly ILocalDataContext _context;
        private ulong UserID
        {
            get
            {
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

        public EmployeeController(ILogger<EmployeeController> logger, ILocalDataContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            OrderDatabase orders = new OrderDatabase(_context.OrderDB);
            return View(orders);
        }
        public IActionResult OrderDetails(ulong id)
        {
            return View(_context.OrderDB.FirstOrDefault(x => x.ID == id));
        }
    }
}