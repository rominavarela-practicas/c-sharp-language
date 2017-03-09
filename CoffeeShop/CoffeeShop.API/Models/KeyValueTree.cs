using System.Collections.Generic;

namespace CoffeeShop.API.Models
{
    public class KeyValueTree : KeyValue
    {
        public List<KeyValue> Options { get; set; }

        public KeyValueTree()
        {
            this.Options = new List<KeyValue>();
        }
    }
}