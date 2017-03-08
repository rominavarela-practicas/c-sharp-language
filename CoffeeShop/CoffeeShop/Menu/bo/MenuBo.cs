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
        
        public List<MenuGroup> ItemGroups { get { return dao.ItemGroups; } }

        public MenuBo()
        {
            dao = MenuDao.Singleton;
        }

        public MenuGroup GetGroup(string GroupName)
        {
            return dao.ItemGroups.Find((group) => { return group.Name == GroupName; });
        }

        public MenuItem GetItem(MenuGroup Group, string ItemName)
        {
            return Group.Items.Find((item) => { return item.Name == ItemName; });
        }

        public MenuItem GetItem(string GroupName, string ItemName)
        {
            return GetGroup(GroupName).Items.Find((item) => { return item.Name == ItemName; });
        }
    }
}
