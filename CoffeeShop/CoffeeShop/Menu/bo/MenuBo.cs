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
        private MenuDao dao;
        
        public List<MenuItem> Items { get { return dao.Items; } }

        public MenuBo()
        {
            dao = MenuDao.Singleton;
        }

        public MenuItem GetItem(string itemKey)
        {
            return dao.Items.Find((item) => { return item.Key == itemKey; });
        }

        public MenuItemOption GetItemOption(MenuItem item, string optionKey)
        {
            return item.Options.Find((option) => { return option.Key == optionKey; });
        }

        public MenuItemOption GetItemOption(string itemKey, string optionKey)
        {
            return GetItem(itemKey).Options.Find((option) => { return option.Key == optionKey; });
        }
    }
}
