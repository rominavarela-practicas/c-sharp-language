using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Menu.model
{
    public class MenuItem
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<Ingredient> Recipe { get; set; }
    }
}
