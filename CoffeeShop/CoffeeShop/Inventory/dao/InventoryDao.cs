using CoffeeShop.Inventory.model;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CoffeeShop.Inventory.dao
{
    public partial class InventoryDao
    {
        public List<InventoryItem> Items { get; set; }

        private InventoryDao()
        {
            var XInventory = XDocument.Load(_Path).Root;
            Items = (from itemGroup in XInventory.Elements("inventory-item")
                          select new InventoryItem
                          {
                              Key = (string)itemGroup.Element("key"),
                              Value = (string)itemGroup.Element("value"),
                              Unit = (string)itemGroup.Element("unit"),
                              Options = (from option in itemGroup.Elements("inventory-item-options").Elements("inventory-item-option")
                                            select new InventoryItemOption
                                            {
                                                Key = (string)option.Element("key"),
                                                Value = (string)option.Element("value"),
                                                PackSize = (decimal)option.Element("pack-size"),
                                                PackCost = (decimal)option.Element("pack-cost")
                                            }).ToList()
                           }).ToList();
        }
    }
}
