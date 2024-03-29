﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClydesTakeoutPrototype.Models.MenuModels;
using ClydesTakeoutPrototype.Models.OrderModels;

namespace ClydesTakeoutPrototype.Helpers
{
    static class MenuBuilder
    {
        /// <summary>
        /// Initializes MenuItems list with the default menu items. 
        /// </summary>
        static MenuBuilder()
        {
            MenuItems = new List<Item>
            {
                new Entree()
                {
                    Name = "Clyde's Burger",
                    Description = "A Clyde's favorite! This delicious 1/4 lb burger comes topped with lettuce, tomato, onion, and a pickle. Sure to delight any burger lover.",
                    ImageURL = "burger.jpeg",
                    Price = 7.50f,
                },

                new Entree()
                {
                    Name = "Classic Chicken Sandwich",
                    Description = "Grilled Red Bird chicken breast, lettuce, tomato, red onion, pickle, avocado garlic aioli.",
                    ImageURL = "chicken_sandwich.jpg",
                    Price = 8.50f,
                },

                new Entree()
                {
                    Name = "BLTA",
                    Description = "Sourdough, bacon, leaf-lettuce, tomato, avocado garlic aioli.",
                    ImageURL = "blt.jpg",
                    Price = 7.50f,
                },

                new Entree()
                {
                    Name = "Chicken Tenders",
                    Description = "Fried ABF chicken with choice of dipping sauce - BBQ, buffalo, ranch, honey mustard, blue cheese.",
                    ImageURL = "chicken_tenders.jpg",
                    Price = 8.00f,
                },

                new Side()
                {
                    Name = "Fries",
                    Description = "Best fries on campus.",
                    ImageURL = "fries.jpg",
                    Price = 2.50f,
                    Type = SideType.Fries
                },

                new Side()
                {
                    Name = "Onion Rings",
                    Description = "Super tasty onion rings",
                    ImageURL = "onion-rings.jpg",
                    Price = 3.50f,
                    Type = SideType.OnionRings
                },

                new Side()
                {
                    Name = "Soup",
                    Description = "Tomato basil or soup of the day.",
                    ImageURL = "soup.jpg",
                    Price = 3.50f,
                    Type = SideType.Soup
                },

                new Side()
                {
                    Name = "Salad",
                    Description = "Tomato, cucumber, and red onion with ranch, balsamic dressing, or vinaigrette.",
                    ImageURL = "salad.jpg",
                    Price = 3.50f,
                    Type = SideType.Salad
                },

                new Drink()
                {
                    Name = "Coke",
                    Price = 2.0f,
                    Type = DrinkType.Coke,
                },

                new Drink()
                {
                    Name = "Pepsi",
                    Price = 2.0f,
                    Type = DrinkType.Pepsi,
                },

                new Drink()
                {
                    Name = "Dr. Pepper",
                    Price = 2.0f,
                    Type = DrinkType.DrPepper,
                },

                new Drink()
                {
                    Name = "Lemonade",
                    Price = 1.5f,
                    Type = DrinkType.Lemonade,
                },

                new Drink()
                {
                    Name = "Sprite",
                    Price = 1.5f,
                    Type = DrinkType.Sprite,
                },

                new Drink()
                {
                    Name = "Water",
                    Price = 0.0f,
                    Type = DrinkType.Water,
                },
            };
        }

        /// <summary>
        /// Default menu items list.
        /// </summary>
        public static List<Item> MenuItems { get; }

        /// <summary>
        /// Convert all items in MenuItems to Item base class.
        /// </summary>
        /// <returns>A list of Items</returns>
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
