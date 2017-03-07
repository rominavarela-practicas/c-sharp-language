using CoffeeShop.Inventory.dao;
using CoffeeShop.Inventory.model;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Inventory.bo
{
    public class InventoryBo
    {
        InventoryDao dao;

        public List<InventoryGroup> ItemGroups { get { return dao.ItemGroups; } }

        public InventoryBo()
        {
            dao = InventoryDao.Singleton;
        }

        public InventoryGroup GetGroup(string GroupName)
        {
            return ItemGroups.Find((group) => { return group.Name == GroupName; });
        }

        public InventoryItem GetItem(InventoryGroup Group, string ItemName)
        {
            return Group.Items.Find((item) => { return item.Name == ItemName; });
        }

        public InventoryItem GetItem(string GroupName, string ItemName)
        {
            return GetGroup(GroupName).Items.Find((item) => { return item.Name == ItemName; });
        }
    }
}
