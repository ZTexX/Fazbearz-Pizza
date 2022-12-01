namespace Fazbearz_Pizza
{
	
	public class Item// this class represents an item 
	{
		protected float price;

		public float GetPrice()
        {
			return price;
        }
		virtual public string ReceiptInfo()   // used as part of the recceipt. !!must be redefined !!
		{
			throw new NotImplementedException();
		}
	}
}