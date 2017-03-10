using CoffeeShop.Inventory.dao;
using CoffeeShop.Inventory.model;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Inventory.bo
{
    public class InventoryBo
    {
        private InventoryDao dao;

        public List<InventoryItem> Items { get { return dao.Items; } }

        public InventoryBo()
        {
            dao = InventoryDao.Singleton;
        }
        
        public InventoryItem GetItem(string itemKey)
        {
            return dao.Items.Find((item) => { return item.Key == itemKey; });
        }

        public InventoryItemOption GetItemOption(InventoryItem item, string optionKey)
        {
            return item.Options.Find((option) => { return option.Key == optionKey; });
        }

        public InventoryItemOption GetItemOption(string itemKey, string optionKey)
        {
            return GetItem(itemKey).Options.Find((option) => { return option.Key == optionKey; });
        }
    }
}
