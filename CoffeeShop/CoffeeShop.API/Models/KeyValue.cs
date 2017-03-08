using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.API.Models
{
    public class KeyValue<KeyType,ValueType>
    {
        public KeyType Key { get; set; }
        public ValueType Value { get; set; }
    }
}