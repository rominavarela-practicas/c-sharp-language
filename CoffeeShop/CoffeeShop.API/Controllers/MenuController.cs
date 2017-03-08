using CoffeeShop.API.Models;
using CoffeeShop.Inventory.dao;
using CoffeeShop.Menu.bo;
using CoffeeShop.Menu.dao;
using CoffeeShop.Menu.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public KeyValue<String,String>[] GetMenuItems()
        {
            var Count = Menu.Items.Count;
            KeyValue<String, String>[] Options = new KeyValue<String, String>[Count];

            for(int i = 0; i < Count; i++ )
            {
                Options[i] = new KeyValue<string, string> { Key = Menu.Items[i].Key, Value = Menu.Items[i].Value };
            }
            return Options;
        }

        [HttpGet]
        [Route("api/menu/{ItemKey}")]
        public KeyValue<String, String>[] GetMenuItems(string ItemKey)
        {
            MenuItem Item = Menu.GetItem(ItemKey);
            var Count = Item.Options.Count;
            KeyValue<String, String>[] Options = new KeyValue<String, String>[Count];

            for(int i = 0; i < Count; i++ )
            {
                Options[i] = new KeyValue<string, string> { Key = Item.Options[i].Key, Value = Item.Options[i].Value };
            }
            return Options;
        }
    }
}
