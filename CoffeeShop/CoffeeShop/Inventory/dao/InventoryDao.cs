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
            var XInventory = XDocument.Load(_Path).Root;
            ItemGroups = (from itemGroup in XInventory.Elements("group")
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
