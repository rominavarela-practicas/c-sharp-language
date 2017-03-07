using System.Collections.Generic;

namespace CoffeeShop.Inventory.model
{
    public class InventoryGroup
    {
        public string Name { get; set; }

        public string Unit { get; set; }

        public List<InventoryItem> Items { get; set; }
    }
}
