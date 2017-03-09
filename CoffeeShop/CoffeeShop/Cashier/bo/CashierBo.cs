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

        public Concept GetMenuItemConcept(string ItemKey, string ItemOption, Dictionary<String, String> RecipeOptions)
        {
            MenuItem Item = Menu.GetItem(ItemKey);
            Concept Concept = new model.Concept { Name = Item.Value , Total = Item.BasePrice };
            
            MenuItemOption SelectedMenuItem = Item.Options[0];
            if (ItemOption != null)
            {
                int SelectedIndex = Item.Options.FindIndex(OptionItem => { return OptionItem.Key == ItemOption; });
                if (SelectedIndex > 0)
                {
                    SelectedMenuItem = Item.Options[SelectedIndex];
                }
            }
            Concept.Name += " " + SelectedMenuItem.Value;

            foreach (Ingredient RecipeOption in SelectedMenuItem.Recipe)
            {
                InventoryItemOption SelectedIngredient = RecipeOption.Options[0];
                if (RecipeOptions.ContainsKey(RecipeOption.Item))
                {
                    string QueriedOptionKey = RecipeOptions[RecipeOption.Item];
                    int SelectedIndex = RecipeOption.Options.FindIndex(OptionItem => { return OptionItem.Key == QueriedOptionKey; });
                    if (SelectedIndex > 0)
                    {
                        SelectedIngredient = RecipeOption.Options[SelectedIndex];
                        Concept.Name += " with " + SelectedIngredient.Value + " " + Inventory.GetItem(RecipeOption.Item).Value;
                    }
                }
                Concept.Total += (RecipeOption.Quantity * SelectedIngredient.PackCost / SelectedIngredient.PackSize);
            }

            return Concept;
        }
    }
}
