using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.API.Models
{
    public class Item
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public decimal Concept { get; set; }

        public List<Item> Children { get; set; }
    }
}