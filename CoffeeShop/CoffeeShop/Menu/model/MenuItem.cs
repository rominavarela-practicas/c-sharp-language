using System.Collections.Generic;

namespace CoffeeShop.Menu.model
{
    public class MenuItem
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public decimal BasePrice { get; set; }

        public List<MenuItemOption> Options { get; set; }
    }
}
