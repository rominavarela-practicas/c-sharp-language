using CoffeeShop.Enums;

namespace CoffeeShop.Ingredients
{
    class Milk : Ingredient
    {
        public Milk()
        {
            this._Name = "milk";
            this._Unit = MeasurementUnit.Liter;
            this._CostPerUnit = 1.0M;
            this.Type = MilkType.Regular;
        }

        public Milk(MilkType milkType)
        {
            this._Name = "milk";
            this._Unit = MeasurementUnit.Liter;
            this._CostPerUnit = 1.0M;
            this.Type = milkType;
        }

        private MilkType _Type;
        public MilkType Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                switch(value)
                {
                    case MilkType.Light:
                        this._CostPerUnit = 1.2M;
                        break;
                    case MilkType.Soy:
                        this._CostPerUnit = 1.2M;
                        break;
                    case MilkType.Coconout:
                        this._CostPerUnit = 1.5M;
                        break;
                    default:
                        this._CostPerUnit = 1.0M;
                        break;
                }
                this._Type = value;
            }
        }

        public override string getDetail()
        {
            return this._Type + " milk";
        }
    }
}
