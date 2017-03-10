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
        InventoryBo Inventory;

        public MenuController()
        {
            Menu = new MenuBo();
            Inventory = new InventoryBo();
        }

        [AcceptVerbs(WebRequestMethods.Http.Get)]
        [Route("api/menu")]
        public List<Item> GetMenuItems()
        {
            List<Item> Options = new List<Item>();
            foreach(MenuItem item in Menu.Items)
            {
                Options.Add(new Item { Key = item.Key, Value = item.Value + " - " + item.Options[0].Value, Concept = item.Options[0].Concept });
            }
            return Options;
        }

        [AcceptVerbs(WebRequestMethods.Http.Get)]
        [Route("api/menu/{ItemKey}/options")]
        public List<Item> GetMenuItemOptions(string ItemKey)
        {
            List<Item> Options = new List<Item>();
            MenuItem SelectedMenuItem = Menu.GetItem(ItemKey);

            // Menu options
            List<Item> OptionsBranch = new List<Item>();
            foreach (MenuItemOption option in SelectedMenuItem.Options)
            {
                OptionsBranch.Add(new Item { Key = option.Key, Value = option.Value, Concept = option.Concept });
            }
            Options.Add(new Item { Children = OptionsBranch });

            // Recipe options
            foreach (Ingredient ingredient in SelectedMenuItem.Options[0].Recipe)
            {
                if(ingredient.Options.Count > 1)
                {
                    OptionsBranch = new List<Item>();
                    foreach (InventoryItemOption option in ingredient.Options)
                    {
                        OptionsBranch.Add(new Item { Key = option.Key, Value = option.Value, Concept = option.Concept });
                    }
                    InventoryItem item = Inventory.GetItem(ingredient.Item);
                    Options.Add(new Item { Key = item.Key, Value = item.Value, Children = OptionsBranch });
                }
            }

            return Options;
        }
    }
}
