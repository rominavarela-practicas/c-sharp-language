using CoffeeShop.Inventory.model;
using System.Collections.Generic;

namespace CoffeeShop.Menu.model
{
    public class Ingredient
    {
        public string Group { get; set; }

        public InventoryItem Item { get; set; }

        public List<InventoryItem> Options { get; set; }

        public decimal Quantity { get; set; }
    }
}
