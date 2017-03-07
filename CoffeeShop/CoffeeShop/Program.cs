using CoffeeShop.Inventory.bo;
using CoffeeShop.Inventory.model;
using CoffeeShop.Menu.bo;
using CoffeeShop.Menu.dao;
using CoffeeShop.Menu.model;
using System;
using System.Collections.Generic;

namespace CoffeeShop
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuGroup menuGroup;
            MenuItem menuItem;

            while (true)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("Cofee Shop Menu");
                Console.WriteLine("-----------------------");

                menuGroup = getMenuGroup();
                menuItem = getMenuItem(menuGroup);
                menuItem = getPersonalizedMenuItem(menuGroup, menuItem);

                Console.WriteLine("-----------------------");
                Console.WriteLine("{0} {1} ..... ${2}", menuGroup.Name, menuItem.Name, menuItem.Price);
                Console.WriteLine("\n\n\n");
            }
        }

        static MenuGroup getMenuGroup()
        {
            MenuBo Menu = new MenuBo();
            string groupName;

            foreach(MenuGroup group in Menu.ItemGroups)
            {
                Console.Write("[ {0} ] ", group.Name);
            }

            groupName = Console.ReadLine();
            return Menu.GetGroup(groupName);
        }

        static MenuItem getMenuItem(MenuGroup Group)
        {
            MenuBo Menu = new MenuBo();
            string itemName;

            foreach (MenuItem item in Group.Items)
            {
                Console.Write("[ {0} ] ", item.Name);
            }

            itemName = Console.ReadLine();
            return Menu.GetItem(Group, itemName);
        }

        static MenuItem getPersonalizedMenuItem(MenuGroup Group, MenuItem Item)
        {
            MenuItem PersonalizedItem = new MenuItem();

            PersonalizedItem.Name = Item.Name;
            PersonalizedItem.Price = Group.BasePrice;
            PersonalizedItem.Recipe = new List<Ingredient>();

            InventoryItem item;
            foreach (Ingredient ingredient in Item.Recipe)
            {
                if(ingredient.Options.Count == 1)
                {
                    item = ingredient.Options[0];
                }
                else
                {
                    item = getInventoryItem(ingredient.Group);
                    if(item != ingredient.Options[0])
                    {
                        PersonalizedItem.Name += " with " + item.Name + " " + ingredient.Group;
                    }
                }
                PersonalizedItem.Recipe.Add(new Ingredient { Group = ingredient.Group, Item = item, Quantity = ingredient.Quantity });
            }

            foreach(Ingredient ingredient in PersonalizedItem.Recipe)
            {
                PersonalizedItem.Price += ((ingredient.Quantity * ingredient.Item.PackCost) / ingredient.Item.PackSize);
            }

            return PersonalizedItem;
        }

        static InventoryItem getInventoryItem(string groupName)
        {
            InventoryBo Inventory = new InventoryBo();
            InventoryGroup Group = Inventory.GetGroup(groupName);
            string itemName;


            foreach (InventoryItem item in Group.Items)
            {
                Console.Write("[ {0} ] ", item.Name);
            }

            itemName = Console.ReadLine();
            return Inventory.GetItem(Group, itemName);
        }
    }
}
