using CoffeeShop.API.Models;
using CoffeeShop.Inventory.bo;
using CoffeeShop.Inventory.model;
using CoffeeShop.Menu.bo;
using CoffeeShop.Menu.model;
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

        [AcceptVerbs(WebRequestMethods.Http.Get)]
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
