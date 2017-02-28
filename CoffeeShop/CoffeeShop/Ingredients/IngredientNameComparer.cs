using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Ingredients
{
    class IngredientNameComparer : IEqualityComparer<Ingredient>
    {
        public bool Equals(Ingredient x, Ingredient y)
        {
            return x.Name == y.Name;
    }

        public int GetHashCode(Ingredient x)
        {
            return x.GetHashCode();
        }
    }
}
