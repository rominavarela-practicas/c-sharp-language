using CoffeeShop.Menu.dao;
using CoffeeShop.Menu.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Menu.bo
{
    public class MenuBo
    {
        MenuDao dao;
        
        public List<MenuItem> Items { get { return dao.Items; } }

        public MenuBo()
        {
            dao = MenuDao.Singleton;
        }

        public MenuItem GetItem(string ItemKey)
        {
            return dao.Items.Find((item) => { return item.Key == ItemKey; });
        }

        public MenuItemOption GetItemOption(MenuItem Item, string OptionKey)
        {
            return Item.Options.Find((option) => { return option.Key == OptionKey; });
        }

        public MenuItemOption GetItemOption(string ItemKey, string OptionKey)
        {
            return GetItem(ItemKey).Options.Find((option) => { return option.Key == OptionKey; });
        }
    }
}
