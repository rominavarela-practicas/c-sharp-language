using CoffeeShop.Cashier.bo;
using CoffeeShop.Cashier.model;
using System;
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
        [Route("api/cashier/{ItemKey}")]
        public Concept GetConcept(string ItemKey, [FromUri] string[] option)
        {
            string ItemOption = null;
            Dictionary<String, String> RecipeOptions = new Dictionary<String, String>();

            foreach (String o in option)
            {
                String[] oSplit = o.Split(':');
                if (oSplit.Length == 2 && !RecipeOptions.ContainsKey(oSplit[0]))
                {
                    RecipeOptions.Add(oSplit[0], oSplit[1]);
                }
                else if (oSplit.Length == 1 && ItemOption == null)
                {
                    ItemOption = oSplit[0];
                }
            }

            return Cashier.GetMenuItemConcept(ItemKey, ItemOption, RecipeOptions);
        }
    }
}
