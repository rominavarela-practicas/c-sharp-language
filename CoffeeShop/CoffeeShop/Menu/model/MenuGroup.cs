using System.Collections.Generic;

namespace CoffeeShop.Menu.model
{
    public class MenuGroup
    {
        public string Name { get; set; }

        public decimal BasePrice { get; set; }

        public List<MenuItem> Items { get; set; }
    }
}
