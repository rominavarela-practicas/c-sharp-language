using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Inventory.dao
{
    public partial class InventoryItemsDao
    {
        static private Task _SingletonTask;
        static private InventoryItemsDao _Singleton;
        static public InventoryItemsDao Singleton
        {
            get
            {
                if (_Singleton == null)
                {
                    if (_SingletonTask == null)
                    {
                        _SingletonTask = new Task(() => { _Singleton = new InventoryItemsDao(); });
                        _SingletonTask.Start();
                    }
                    _SingletonTask.Wait();
                }
                return _Singleton;
            }
        }
    }
}
