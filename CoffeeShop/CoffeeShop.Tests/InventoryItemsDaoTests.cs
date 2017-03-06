using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeShop.Inventory.dao;
using System.Threading.Tasks;

namespace CoffeeShop.Tests
{
    [TestClass]
    public class InventoryItemsDaoTests
    {
        [TestMethod]
        public void SingletonTest()
        {
            int N = 10;
            InventoryItemsDao[] dao = new InventoryItemsDao[N];
            Task[] task = new Task[N];
            

            for (int i = 0; i < N; i++)
            {
                int arg = i;
                task[i] = new Task(() => { dao[arg] = InventoryItemsDao.Singleton; });
            }

            for (int i = 0; i < N; i++)
            {
                task[i].Start();
            }

            for (int i = 0; i < N; i++)
            {
                task[i].Wait();
            }

            for (int i=1; i<N; i++)
            {
                Assert.IsNotNull(dao[i]);
                Assert.AreEqual(dao[i-1], dao[i]);
            }
        }
    }
}
