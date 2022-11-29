namespace Fazbearz_Pizza
{
	
	public class Item// this class represents an item 
	{
		protected float prise;

		public float GetPrice()
        {
			return prise;
        }
		virtual public string ReceiptInfo()   // used as part of the recceipt. !!must be redefined !!
		{
			throw new NotImplementedException();
		}
	}
}