using CoffeeShop.Enums;

namespace CoffeeShop.Ingredients
{
    class Coffee : Ingredient
    {
        public Coffee()
        {
            this._Name = "coffee";
            this._Unit = MeasurementUnit.Kilogram;
            this._CostPerUnit = 10.0M;
            this._Type = RoastLevel.Medium;
        }

        public Coffee(RoastLevel roastLevel)
        {
            this._Name = "coffee";
            this._Unit = MeasurementUnit.Kilogram;
            this._CostPerUnit = 10.0M;
            this.Type = roastLevel;
        }

        private RoastLevel _Type;
        public RoastLevel Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                switch (value)
                {
                    case RoastLevel.Dark:
                        this._CostPerUnit = 20.0M;
                        break;
                    case RoastLevel.Light:
                        this._CostPerUnit = 9.0M;
                        break;
                    default:
                        this._CostPerUnit = 10.0M;
                        break;
                }
                this._Type = value;
            }
        }

        public override string getDetail()
        {
            return this._Type + " roast coffee";
        }
    }
}
