using CoffeeShop.Inventory.model;
using System.Collections.Generic;

namespace CoffeeShop.Menu.model
{
    public class Ingredient
    {
        public InventoryItem Item { get; set; }

        public List<InventoryItemOption> Options { get; set; }

        public decimal Quantity { get; set; }
    }
}
