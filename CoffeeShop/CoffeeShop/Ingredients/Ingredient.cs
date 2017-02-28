using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Enums;

namespace CoffeeShop.Ingredients
{
    abstract class Ingredient
    {
        protected string _Name;
        public virtual string Name { get { return _Name; } }

        protected MeasurementUnit _Unit;
        public virtual MeasurementUnit Unit { get { return _Unit; } }

        protected decimal _CostPerUnit;
        public virtual decimal CostPerUnit { get { return _CostPerUnit; } }

        public abstract string getDetail();
    }
}
