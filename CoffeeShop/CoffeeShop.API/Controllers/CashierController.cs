using CoffeeShop.API.Models;
using CoffeeShop.Cashier.bo;
using CoffeeShop.Cashier.model;
using CoffeeShop.Menu.bo;
using CoffeeShop.Menu.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeShop.API.Controllers
{
    public class CashierController : ApiController
    {
        private MenuBo menu;
        private CashierBo Cashier;

        public CashierController()
        {
            menu = new MenuBo();
            Cashier = new CashierBo();
        }

        private Item GetConcept(string itemKey, ICollection<Item> options )
        {
            MenuItem selectedItem = menu.GetItem(itemKey);
            if (selectedItem == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            string ItemOption = null;
            Dictionary<String, String> RecipeOptions = new Dictionary<String, String>();

            if (options != null)
            {
                foreach (var QueryValue in options)
                {
                    if (QueryValue.Key == "option")
                    {
                        if (ItemOption == null)
                        {
                            ItemOption = QueryValue.Value;
                        }
                    }
                    else if (!RecipeOptions.ContainsKey(QueryValue.Key))
                    {
                        RecipeOptions.Add(QueryValue.Key, QueryValue.Value);
                    }
                }
            }

            Concept concept = Cashier.CalcConcept(selectedItem, ItemOption, RecipeOptions);
            return new Item { Value = concept.Name, Concept = concept.Total };
        }

        [AcceptVerbs(WebRequestMethods.Http.Get)]
        [Route("api/cashier/pricing/{itemKey}")]
        public Item GetItemPricing(string itemKey)
        {
            List<Item> queryItems = (from pair in this.Request.GetQueryNameValuePairs()
                                    select new Item { Key = pair.Key, Value = pair.Value }).ToList();

            return GetConcept(itemKey, queryItems);
        }

        [AcceptVerbs(WebRequestMethods.Http.Post)]
        [Route("api/cashier/bill")]
        public List<Item> GetItemsBill([FromBody] List<Item> menuItems)
        {
            List<Item> bill = new List<Item>();
            
            foreach(Item selectedMenuItem in menuItems)
            {
                bill.Add(GetConcept(selectedMenuItem.Key, selectedMenuItem.Children));
            }

            return bill;
        }
    }
}
