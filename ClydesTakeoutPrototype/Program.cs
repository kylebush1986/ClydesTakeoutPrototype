using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ClydesTakeoutPrototype.Models.MenuModels;
using ClydesTakeoutPrototype.Models.SystemModels;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Helpers;

namespace ClydesTakeoutPrototype
{
    public class Program
    {
        public static Menu ClydesMenu = new Menu();
        public static void Main(string[] args)
        {
            // Load Clyde's Menu if it exists. If not then build it and create a menu database.
            ClydesMenu.Items = MenuBuilder.MenuItems;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
