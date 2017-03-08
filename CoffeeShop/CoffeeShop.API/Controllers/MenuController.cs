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
        [HttpGet]
        public List<MenuGroup> GetMenu()
        {
            MenuBo Menu = new MenuBo();
            return Menu.ItemGroups;
        }
    }
}
