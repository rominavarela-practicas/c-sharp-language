using CoffeeShop.Cashier.model;
using CoffeeShop.Inventory.bo;
using CoffeeShop.Inventory.model;
using CoffeeShop.Menu.bo;
using CoffeeShop.Menu.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Cashier.bo
{
    public class CashierBo
    {
        InventoryBo Inventory;
        MenuBo Menu;

        public CashierBo()
        {
            Inventory = new InventoryBo();
            Menu = new MenuBo();
        }

        public Concept GetMenuItemConcept(string ItemKey, string OptionKey, Dictionary<String, String> RecipeOptions)
        {
            MenuItem Item = Menu.GetItem(ItemKey);
            Concept Concept = new model.Concept();
            
            // Custom menu option
            MenuItemOption SelectedMenuOption = Item.Options[0];
            if (OptionKey != null)
            {
                int SelectedIndex = Item.Options.FindIndex(OptionItem => { return OptionItem.Key == OptionKey; });
                if (SelectedIndex > 0)
                {
                    SelectedMenuOption = Item.Options[SelectedIndex];
                }
            }
            Concept.Name = Item.Value + " " + SelectedMenuOption.Value;
            Concept.Total = SelectedMenuOption.Concept;

            // Custom recipe options
            foreach (Ingredient IngredientOption in SelectedMenuOption.Recipe)
            {
                if (RecipeOptions.ContainsKey(IngredientOption.Item))
                {
                    string QueriedOptionKey = RecipeOptions[IngredientOption.Item];
                    InventoryItemOption SelectedRecipeOption = IngredientOption.Options.Find(OptionItem => { return OptionItem.Key == QueriedOptionKey; });
                    if (SelectedRecipeOption != null && SelectedRecipeOption.Concept > 0)
                    {
                        Concept.Name += " ( " + SelectedRecipeOption.Value + " " + Inventory.GetItem(IngredientOption.Item).Value + " )";
                        Concept.Total += SelectedRecipeOption.Concept;
                    }
                }
            }

            return Concept;
        }
    }
}
