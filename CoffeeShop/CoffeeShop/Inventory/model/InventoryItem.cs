using System.Collections.Generic;

namespace CoffeeShop.Inventory.model
{
    public class InventoryItem
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string Unit { get; set; }

        public List<InventoryItemOption> Options { get; set; }
    }
}
