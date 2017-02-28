using CoffeeShop.Enums;
using CoffeeShop.Ingredients;
using System;

namespace CoffeeShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Ingredient[] ingredients =
            {
                new Water(),
                new Coffee(RoastLevel.Light),
                new Coffee(RoastLevel.Medium),
                new Coffee(RoastLevel.Dark),
                new Milk(MilkType.Regular),
                new Milk(MilkType.Light),
                new Milk(MilkType.Soy),
                new Milk(MilkType.Coconout)
            };

            Console.WriteLine("Coffee Shop Inventory\n");
            foreach (Ingredient ingredient in ingredients)
            {
                Console.WriteLine("{0} ${1}/{2}" , ingredient.getDetail(), ingredient.CostPerUnit, ingredient.Unit);
            }

            Console.ReadLine();
        }
    }
}
