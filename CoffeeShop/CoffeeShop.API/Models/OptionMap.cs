using System;
using System.Collections.Generic;

namespace CoffeeShop.API.Models
{
    public class OptionMap<T>
    {
        private Dictionary<String, List<T>> Options;

        public OptionMap()
        {
            Options = new Dictionary<String, List<T>>();
        }

        public void Add(String OptionKey, T Option)
        {
            if(!Options.ContainsKey(OptionKey))
            {
                Options.Add(OptionKey, new List<T>());
            }
            Options[OptionKey].Add(Option);
        }

        public Dictionary<String, List<T>> GetSendableObject()
        {
            return Options;
        }
    }
}