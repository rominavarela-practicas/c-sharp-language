using CoffeeShop.Inventory.dao;
using CoffeeShop.Inventory.model;
using System;

namespace CoffeeShop
{
    class Program
    {
        static void Main(string[] args)
        {
            InventoryItemsDao _InventoryItemsDao = InventoryItemsDao.Singleton;

            ItemGroup itemGroup;
            ItemVariety itemVariety;

            while(true)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("Cofee Shop Inventory");
                Console.WriteLine("-----------------------");

                itemGroup = getItemGroup();
                itemVariety = getItemVariety(itemGroup);
                
                Console.WriteLine("-----------------------");
                Console.WriteLine("Cost: ${0} / {1} {2} Pack", itemVariety.PackCost, itemVariety.PackSize, itemGroup.Unit);
                Console.WriteLine("\n\n\n");
            }
        }

        static ItemGroup getItemGroup()
        {
            InventoryItemsDao _InventoryItemsDao = InventoryItemsDao.Singleton;
            string itemGroup;
            
            foreach (ItemGroup group in _InventoryItemsDao.ItemGroups)
            {
                Console.Write("[ {0} ] ", group.Name);
            }

            itemGroup = Console.ReadLine();

            return _InventoryItemsDao[itemGroup];
        }

        static ItemVariety getItemVariety(ItemGroup itemGroup)
        {
            InventoryItemsDao _InventoryItemsDao = InventoryItemsDao.Singleton;
            string itemVariety;


            foreach (ItemVariety variety in itemGroup.Varieties)
            {
                Console.Write("[ {0} ] ", variety.Name);
            }

            itemVariety = Console.ReadLine();
            return _InventoryItemsDao[itemGroup, itemVariety];
        }
    }
}
