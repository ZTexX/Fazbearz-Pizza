
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
            string tem = test.ReceiptInfo();
            int T = 0;
        }
    }
}