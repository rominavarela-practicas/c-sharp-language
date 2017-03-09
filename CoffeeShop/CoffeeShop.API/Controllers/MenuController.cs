using CoffeeShop.API.Models;
using CoffeeShop.Cashier.bo;
using CoffeeShop.Cashier.model;
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
        CashierBo Cashier;

        public MenuController()
        {
            Menu = new MenuBo();
            Cashier = new CashierBo();
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
        public Concept GetMenuItemPrice(string ItemKey, [FromUri] string[] option)
        {
            string ItemOption = null;
            Dictionary<String, String> RecipeOptions = new Dictionary<String, String>();

            foreach (String o in option)
            {
                String[] oSplit = o.Split(':');
                if(oSplit.Length == 2 && !RecipeOptions.ContainsKey(oSplit[0]))
                {
                    RecipeOptions.Add(oSplit[0], oSplit[1]);
                }
                else if (oSplit.Length == 1 && ItemOption == null)
                {
                    ItemOption = oSplit[0];
                }
            }

            return Cashier.GetMenuItemConcept(ItemKey, ItemOption, RecipeOptions);
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
            Options.Add(new KeyValueTree { Options = OptionsBranch });

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
