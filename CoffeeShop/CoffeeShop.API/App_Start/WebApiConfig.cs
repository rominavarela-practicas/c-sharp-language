using CoffeeShop.Inventory.dao;
using CoffeeShop.Menu.dao;
using System.Web;
using System.Web.Http;

namespace CoffeeShop.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            InventoryDao.Path = HttpContext.Current.Server.MapPath("~/App_Data/Inventory.xml");
            MenuDao.Path = HttpContext.Current.Server.MapPath("~/App_Data/Menu.xml");

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
