using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Menu.model
{
    public class MenuItemOption
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public List<Ingredient> Recipe { get; set; }
    }
}
