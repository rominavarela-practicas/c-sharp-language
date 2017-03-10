using CoffeeShop.Inventory.bo;
using CoffeeShop.Inventory.model;
using CoffeeShop.Menu.model;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CoffeeShop.Menu.dao
{
    public partial class MenuDao
    {
        private InventoryBo inventory;

        public List<MenuItem> Items { get; set; }

        private MenuDao()
        {
            inventory = new InventoryBo();

            var document = XDocument.Load(_Path).Root;

            Items = (from item in document.Elements("menu-item")
                          select new MenuItem
                          {
                              Key = (string)item.Element("key"),
                              Value = (string)item.Element("value"),
                              Options = (from option in item.Elements("menu-item-options").Elements("menu-item-option")
                                       select new MenuItemOption
                                       {
                                           Key = (string)option.Element("key"),
                                           Value = (string)option.Element("value"),
                                           Concept = (decimal)option.Element("concept"),
                                           Recipe = (from ingredient in option.Elements("recipe").Elements("ingredient")
                                                     select new Ingredient
                                                     {
                                                         Item = inventory.GetItem((string)ingredient.Element("item")),
                                                         Quantity = (decimal)ingredient.Element("quantity"),
                                                         Options = ingredient.Element("item-option") == null ?
                                                            inventory.GetItem((string)ingredient.Element("item")).Options : 
                                                            new List<InventoryItemOption>(new InventoryItemOption[]{ inventory.GetItemOption((string)ingredient.Element("item"), (string)ingredient.Element("item-option")) })
                                                     }).ToList()
                                       }).ToList()
                          }).ToList();
        }
    }
}
