using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClydesTakeoutPrototype.Models.MenuModels;
using ClydesTakeoutPrototype.Models.OrderModels;

namespace ClydesTakeoutPrototype.Helpers
{
    static class MenuBuilder
    {
        static MenuBuilder()
        {
            MenuItems = new List<Item>
            {
                new Entree()
                {
                    Name = "Clyde's Burger",
                    Description = "A Clyde's favorite! This delicious 1/4 lb burger comes topped with lettuce, tomato, onion, and a pickle. Sure to delight any burger lover.",
                    ImageURL = "burger.jpeg",
                    Price = 12.00f,
                    Category = EntreeCategories.Grilled
                },

                new Entree()
                {
                    Name = "Clyde's Chicken Sandwich",
                    Description = "Clyde's campus famous chicken sandwich comes with letuce, tomato, a pickle, and Clyde's secret sauce. This is one tasty sandwich.",
                    ImageURL = "chicken_sandwich.jpg",
                    Price = 9.50f,
                    Category = EntreeCategories.Fried
                }
            };
        }
        public static List<Item> MenuItems { get; }

        public static List<Item> GetListAsItem()
        {
            List<Item> items = new List<Item>();
            foreach (var item in MenuItems)
            {
                items.Add((Item)item);
            }
            return items;
        }
    }
}
