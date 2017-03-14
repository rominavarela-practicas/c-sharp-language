using CoffeeShop.Cashier.model;
using CoffeeShop.Inventory.bo;
using CoffeeShop.Inventory.model;
using CoffeeShop.Menu.bo;
using CoffeeShop.Menu.model;
using System;
using System.Collections.Generic;

namespace CoffeeShop.Cashier.bo
{
    public class CashierBo
    {
        private MenuBo menu;

        public CashierBo()
        {
            menu = new MenuBo();
        }

        public Concept CalcConcept(MenuItem selectedItem, string optionKey, Dictionary<String, String> recipeOptions)
        {
            MenuItemOption selectedOption = selectedItem.Options[0];
            Concept concept = new model.Concept();
            
            // Custom menu option
            if (optionKey != null)
            {
                int selectedIndex = selectedItem.Options.FindIndex(option => { return option.Key == optionKey; });
                if (selectedIndex > 0)
                {
                    selectedOption = selectedItem.Options[selectedIndex];
                }
            }
            concept.Name = selectedItem.Value + " " + selectedOption.Value;
            concept.Total = selectedOption.Concept;

            // Custom recipe options
            foreach (Ingredient recipeOption in selectedOption.Recipe)
            {
                if (recipeOptions.ContainsKey(recipeOption.Item.Key))
                {
                    string ingredientKey = recipeOptions[recipeOption.Item.Key];
                    InventoryItemOption selectedIngredientOption = recipeOption.Options.Find(option => { return option.Key == ingredientKey; });
                    if (selectedIngredientOption != null && selectedIngredientOption.Concept > 0)
                    {
                        concept.Name += " ( " + selectedIngredientOption.Value + " " + recipeOption.Item.Value + " )";
                        concept.Total += selectedIngredientOption.Concept;
                    }
                }
            }

            return concept;
        }
    }
}
