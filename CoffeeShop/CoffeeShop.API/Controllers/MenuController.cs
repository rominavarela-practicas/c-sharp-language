using CoffeeShop.API.Models;
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
        public Dictionary<String, List<KeyValue>> GetMenuItemOptions(string ItemKey)
        {
            OptionMap<KeyValue> Map = new OptionMap<KeyValue>();
            MenuItem Item = Menu.GetItem(ItemKey);
            
            foreach(MenuItemOption option in Item.Options)
            {
                Map.Add("option", new KeyValue { Key = option.Key, Value = option.Value });
            }

            foreach (Ingredient ingredient in Item.Options[0].Recipe)
            {
                if(ingredient.Options.Count > 1)
                {
                    foreach(InventoryItemOption option in ingredient.Options)
                    {
                        Map.Add(ingredient.Item, new KeyValue { Key = option.Key, Value = option.Value });
                    }
                }
            }

            return Map.GetSendableObject();
        }
    }
}
