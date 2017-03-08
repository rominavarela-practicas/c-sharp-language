using CoffeeShop.Inventory.dao;
using CoffeeShop.Inventory.model;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Inventory.bo
{
    public class InventoryBo
    {
        InventoryDao dao;

        public List<InventoryItem> Items { get { return dao.Items; } }

        public InventoryBo()
        {
            dao = InventoryDao.Singleton;
        }
        
        public InventoryItem GetItem(string ItemKey)
        {
            return dao.Items.Find((item) => { return item.Key == ItemKey; });
        }

        public InventoryItemOption GetItemOption(InventoryItem Item, string OptionKey)
        {
            return Item.Options.Find((option) => { return option.Key == OptionKey; });
        }

        public InventoryItemOption GetItemOption(string ItemKey, string OptionKey)
        {
            return GetItem(ItemKey).Options.Find((option) => { return option.Key == OptionKey; });
        }
    }
}
