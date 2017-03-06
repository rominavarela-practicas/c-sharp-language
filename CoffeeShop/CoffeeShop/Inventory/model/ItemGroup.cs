using System.Collections.Generic;

namespace CoffeeShop.Inventory.model
{
    public class ItemGroup
    {
        public string Name { get; set; }

        public string Unit { get; set; }

        public List<ItemVariety> Varieties { get; set; }
    }
}
