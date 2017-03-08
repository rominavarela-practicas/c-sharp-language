using CoffeeShop.Inventory.model;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CoffeeShop.Inventory.dao
{
    public partial class InventoryDao
    {
        public List<InventoryGroup> ItemGroups { get; set; }

        private InventoryDao()
        {
            var XGroups = XDocument.Load("C:\\Users\\rominavarela\\workspace\\demo\\c-sharp-lang\\CoffeeShop\\CoffeeShop\\Inventory.xml").Root.Elements("group");
            ItemGroups = (from itemGroup in XGroups
                           select new InventoryGroup
                           {
                               Name = (string)itemGroup.Element("name"),
                               Unit = (string)itemGroup.Element("unit"),
                               Items = (from item in itemGroup.Elements("items").Elements("item")
                                            select new InventoryItem
                                            {
                                                Name = (string)item.Element("name"),
                                                PackSize = (decimal)item.Element("pack-size"),
                                                PackCost = (decimal)item.Element("pack-cost")
                                            }).ToList()
                           }).ToList();
        }
    }
}
