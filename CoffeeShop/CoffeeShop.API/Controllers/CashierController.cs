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

        [AcceptVerbs(WebRequestMethods.Http.Get)]
        [Route("api/cashier/pricing/{ItemKey}")]
        public Item GetConcept(string ItemKey)
        {
            string ItemOption = null;
            Dictionary<String, String> RecipeOptions = new Dictionary<String, String>();

            foreach (KeyValuePair<string,string> QueryValue in this.Request.GetQueryNameValuePairs())
            {
                if(QueryValue.Value.Length == 0)
                {
                    if( ItemOption == null)
                    {
                        ItemOption = QueryValue.Key;
                    }
                }
                else if (!RecipeOptions.ContainsKey(QueryValue.Key))
                {
                    RecipeOptions.Add(QueryValue.Key, QueryValue.Value);
                }
            }

            Concept concept = Cashier.GetMenuItemConcept(ItemKey, ItemOption, RecipeOptions);
            return new Item { Value = concept.Name, Concept = concept.Total };
        }

        /*[AcceptVerbs(WebRequestMethods.Http.Post)]
        [Route("api/cashier/bill")]
        public List<Concept> GetBill([FromBody] Dictionary<String, String> MenuItems)
        {
            List<Concept> Bill = new List<Concept>();
            
            foreach(String ItemKey in MenuItems.Keys)
            {
                Bill.Add(new Concept { Name = ItemKey });
            }

            return Bill;
        }*/
    }
}
