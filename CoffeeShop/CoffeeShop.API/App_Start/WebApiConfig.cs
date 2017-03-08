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
            InventoryDao.SetPath(HttpContext.Current.Server.MapPath("~/App_Data/Inventory.xml"));
            MenuDao.SetPath(HttpContext.Current.Server.MapPath("~/App_Data/Menu.xml"));

            // Web API routes
            config.MapHttpAttributeRoutes();
            
            /*config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/
        }
    }
}
