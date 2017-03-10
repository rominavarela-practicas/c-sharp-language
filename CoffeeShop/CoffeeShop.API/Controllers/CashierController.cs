using CoffeeShop.API.Models;
using CoffeeShop.Cashier.bo;
using CoffeeShop.Cashier.model;
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
        CashierBo Cashier;

        public CashierController()
        {
            Cashier = new CashierBo();
        }

        private Item GetConcept(string ItemKey, ICollection<Item> Options )
        {
            string ItemOption = null;
            Dictionary<String, String> RecipeOptions = new Dictionary<String, String>();

            if (Options != null)
            {
                foreach (var QueryValue in Options)
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

            Concept concept = Cashier.CalcConcept(ItemKey, ItemOption, RecipeOptions);
            return new Item { Value = concept.Name, Concept = concept.Total };
        }

        [AcceptVerbs(WebRequestMethods.Http.Get)]
        [Route("api/cashier/pricing/{ItemKey}")]
        public Item GetItemPricing(string ItemKey)
        {
            List<Item> QueryItems = (from pair in this.Request.GetQueryNameValuePairs()
                                    select new Item { Key = pair.Key, Value = pair.Value }).ToList();

            return GetConcept(ItemKey, QueryItems);
        }

        [AcceptVerbs(WebRequestMethods.Http.Post)]
        [Route("api/cashier/bill")]
        public List<Item> GetItemsBill([FromBody] List<Item> MenuItems)
        {
            List<Item> Bill = new List<Item>();
            
            foreach(Item SelectedMenuItem in MenuItems)
            {
                Bill.Add(GetConcept(SelectedMenuItem.Key, SelectedMenuItem.Children));
            }

            return Bill;
        }
    }
}
