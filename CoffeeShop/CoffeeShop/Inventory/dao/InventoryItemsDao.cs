using CoffeeShop.Inventory.model;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CoffeeShop.Inventory.dao
{
    class InventoryItemsDao
    {
        static private InventoryItemsDao _Singleton;
        static public InventoryItemsDao Singleton
        {
            get
            {
                if (_Singleton == null)
                {
                    _Singleton = new InventoryItemsDao();
                }
                return _Singleton;
            }
        }

        List<ItemGroup> _ItemGroups;
        public List<ItemGroup> ItemGroups { get { return _ItemGroups; } }

        private InventoryItemsDao()
        {
            var XGroups = XDocument.Load("InventoryItems.xml").Root.Elements("group");
            _ItemGroups = (from itemGroup in XGroups
                           select new ItemGroup
                           {
                               Name = (string)itemGroup.Element("name"),
                               Unit = (string)itemGroup.Element("unit"),
                               Varieties = (from itemVariety in itemGroup.Elements("varieties").Elements("variety")
                                            select new ItemVariety
                                            {
                                                Name = (string)itemVariety.Element("name"),
                                                PackSize = (decimal)itemVariety.Element("pack-size"),
                                                PackCost = (decimal)itemVariety.Element("pack-cost")
                                            }).ToList()
                           }).ToList();
        }

        public ItemGroup this[string Group]
        {
            get
            {
                return (from itemGroup in _ItemGroups
                        where itemGroup.Name == Group
                        select itemGroup).First();
            }
        }

        public ItemVariety this[ItemGroup Group, string Variety]
        {
            get
            {
                return (from itemVariety in Group.Varieties
                        where itemVariety.Name == Variety
                        select itemVariety).First();
            }
        }

        public ItemVariety this[string Group, string Variety]
        {
            get
            {
                return this[this[Group], Variety];
            }
        }

    }
}
