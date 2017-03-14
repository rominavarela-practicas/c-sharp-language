using CoffeeShop.API.Models;
using CoffeeShop.Inventory.bo;
using CoffeeShop.Inventory.model;
using CoffeeShop.Menu.bo;
using CoffeeShop.Menu.model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeShop.API.Controllers
{
    public class MenuController : ApiController
    {
        private MenuBo menu;

        public MenuController()
        {
            menu = new MenuBo();
        }

        [AcceptVerbs(WebRequestMethods.Http.Get)]
        [Route("api/menu")]
        public List<Item> GetMenuItems()
        {
            List<Item> options = new List<Item>();
            foreach(MenuItem item in menu.Items)
            {
                options.Add(new Item { Key = item.Key, Value = item.Value + " - " + item.Options[0].Value, Concept = item.Options[0].Concept });
            }
            return options;
        }

        [AcceptVerbs(WebRequestMethods.Http.Get)]
        [Route("api/menu/{itemKey}/options")]
        public List<Item> GetMenuItemOptions(string itemKey)
        {
            List<Item> options = new List<Item>();
            MenuItem selectedItem = menu.GetItem(itemKey);
            if(selectedItem == null )
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            // Menu options
            List<Item> optionsChildren = new List<Item>();
            foreach (MenuItemOption option in selectedItem.Options)
            {
                optionsChildren.Add(new Item { Key = option.Key, Value = option.Value, Concept = option.Concept });
            }
            options.Add(new Item { Key = "option", Children = optionsChildren });

            // Recipe options
            foreach (Ingredient ingredient in selectedItem.Options[0].Recipe)
            {
                if(ingredient.Options.Count > 1)
                {
                    optionsChildren = new List<Item>();
                    foreach (InventoryItemOption option in ingredient.Options)
                    {
                        optionsChildren.Add(new Item { Key = option.Key, Value = option.Value, Concept = option.Concept });
                    }
                    options.Add(new Item { Key = ingredient.Item.Key, Value = ingredient.Item.Value, Children = optionsChildren });
                }
            }

            return options;
        }
    }
}
