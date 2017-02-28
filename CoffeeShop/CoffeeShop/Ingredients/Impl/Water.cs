using CoffeeShop.Enums;

namespace CoffeeShop.Ingredients
{
    class Water : Ingredient
    {
        public Water()
        {
            this._Name = "water";
            this._Unit = MeasurementUnit.Liter;
            this._CostPerUnit = 0.1M;
        }

        public override string getDetail()
        {
            return "Water";
        }
    }
}
