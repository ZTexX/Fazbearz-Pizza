
namespace Fazbearz_Pizza
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Pizza test = new Pizza(sizeEnme.XL, CrustEnme.Stuffed,new TopingsEnme[3] {TopingsEnme.ExCheese,TopingsEnme.pepeprs,TopingsEnme.Olives});
            Drink test2 = new Drink(DrinkType.DrPepper, DrinkSize.Large);

            Order Test3 = new Order(69420);

            Test3.addItem(test);
            Test3.addItem(test2);

            string tem = Test3.OrderSlip();
            int T = 0;
        }
    } 
}