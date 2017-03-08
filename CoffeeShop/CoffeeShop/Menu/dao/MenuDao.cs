using CoffeeShop.Inventory.bo;
using CoffeeShop.Inventory.model;
using CoffeeShop.Menu.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoffeeShop.Menu.dao
{
    public partial class MenuDao
    {
        InventoryBo Inventory;
        public List<MenuGroup> ItemGroups { get; set; }

        private MenuDao()
        {
            Inventory = new InventoryBo();

            var XGroups = XDocument.Load("C:\\Users\\rominavarela\\workspace\\demo\\c-sharp-lang\\CoffeeShop\\CoffeeShop\\Menu.xml").Root.Elements("group");
            ItemGroups = (from itemGroup in XGroups
                          select new MenuGroup
                          {
                              Name = (string)itemGroup.Element("name"),
                              BasePrice = (decimal)itemGroup.Element("base-price"),
                              Items = (from item in itemGroup.Elements("items").Elements("item")
                                       select new MenuItem
                                       {
                                           Name = (string)item.Element("name"),
                                           Recipe = (from ingredient in item.Elements("recipe").Elements("ingredient")
                                                     select new Ingredient
                                                     {
                                                         Group = (string)ingredient.Element("group"),
                                                         Quantity = (decimal)ingredient.Element("quantity"),
                                                         Options = ingredient.Element("item") == null ? 
                                                            Inventory.GetGroup((string)ingredient.Element("group")).Items : 
                                                            new List<InventoryItem>(new InventoryItem[]{ Inventory.GetItem((string)ingredient.Element("group"), (string)ingredient.Element("item")) })
                                                     }).ToList()
                                       }).ToList()
                          }).ToList();
        }
    }
}
