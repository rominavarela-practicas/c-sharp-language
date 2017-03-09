using CoffeeShop.API.Models;
using CoffeeShop.Inventory.bo;
using CoffeeShop.Inventory.dao;
using CoffeeShop.Inventory.model;
using CoffeeShop.Menu.bo;
using CoffeeShop.Menu.model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace CoffeeShop.API.Controllers
{
    public class MenuController : ApiController
    {
        MenuBo Menu;

        public MenuController()
        {
            Menu = new MenuBo();
        }

        [AcceptVerbs(WebRequestMethods.Http.Get)]
        [Route("api/menu")]
        public List<KeyValue> GetMenuItems()
        {
            List<KeyValue> Options = new List<KeyValue>();
            foreach(MenuItem item in Menu.Items)
            {
                Options.Add(new KeyValue { Key = item.Key, Value = item.Value });
            }
            return Options;
        }
        
        [HttpGet]
        [Route("api/menu/{ItemKey}")]
        public KeyValue[] GetMenuItemPrice(string ItemKey, [FromUri] string[] option)
        {
            MenuItem Item = Menu.GetItem(ItemKey);
            string Concept = Item.Value;
            decimal Total = Item.BasePrice;
            InventoryBo Inventory = new InventoryBo();
            
            // Map Options Query
            Dictionary<String, String> OptionsQuery = new Dictionary<String, String>();
            foreach (String o in option)
            {
                String[] oSplit = o.Split(':');
                if(oSplit.Length == 2 && !OptionsQuery.ContainsKey(oSplit[0]))
                {
                    OptionsQuery.Add(oSplit[0], oSplit[1]);
                }
            }

            // Set Item Option
            MenuItemOption SelectedMenuItem = Item.Options[0];
            if (OptionsQuery.ContainsKey("item-option"))
            {
                string QueriedOptionKey = OptionsQuery["item-option"];
                int SelectedIndex = Item.Options.FindIndex( OptionItem => { return OptionItem.Key == QueriedOptionKey; });
                if(SelectedIndex > 0)
                {
                    SelectedMenuItem = Item.Options[SelectedIndex];
                }
            }
            Concept += " " + SelectedMenuItem.Value;

            // Set Ingredient Options
            foreach (Ingredient ingredient in SelectedMenuItem.Recipe)
            {
                InventoryItemOption SelectedIngredient = ingredient.Options[0];
                if (OptionsQuery.ContainsKey(ingredient.Item))
                {
                    string QueriedOptionKey = OptionsQuery[ingredient.Item];
                    int SelectedIndex = ingredient.Options.FindIndex(OptionItem => { return OptionItem.Key == QueriedOptionKey; });
                    if (SelectedIndex > 0)
                    {
                        SelectedIngredient = ingredient.Options[SelectedIndex];
                        Concept += " with " + SelectedIngredient.Value + " " + Inventory.GetItem(ingredient.Item).Value;
                    }
                }
                Total += ingredient.Quantity * SelectedIngredient.PackCost / SelectedIngredient.PackSize;
            }

            return new KeyValue[] {
                new KeyValue { Key = "concept", Value = Concept },
                new KeyValue { Key= "total", Value = Total.ToString() }
            };
        }

        [HttpGet]
        [Route("api/menu/{ItemKey}/options")]
        public List<KeyValue> GetMenuItemOptions(string ItemKey)
        {
            List<KeyValue> Options = new List<KeyValue>();
            MenuItem Item = Menu.GetItem(ItemKey);
            InventoryBo Inventory = new InventoryBo();

            List<KeyValue> OptionsBranch = new List<KeyValue>();
            foreach (MenuItemOption option in Item.Options)
            {
                OptionsBranch.Add(new KeyValue { Key = option.Key, Value = option.Value });
            }
            Options.Add(new KeyValueTree { Key = "item-option", Value = Item.Value + " Option", Options = OptionsBranch });

            foreach (Ingredient ingredient in Item.Options[0].Recipe)
            {
                if(ingredient.Options.Count > 1)
                {
                    OptionsBranch = new List<KeyValue>();
                    foreach (InventoryItemOption option in ingredient.Options)
                    {
                        OptionsBranch.Add(new KeyValue { Key = option.Key, Value = option.Value });
                    }
                    InventoryItem item = Inventory.GetItem(ingredient.Item);
                    Options.Add(new KeyValueTree { Key = item.Key, Value = item.Value, Options = OptionsBranch });
                }
            }

            return Options;
        }
    }
}
