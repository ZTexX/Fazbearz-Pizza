namespace Fazbearz_Pizza
{
	///<summary>
	///this class represents an item
	///</summary>
	public class Item// this class represents an item 
	{
		protected float price;
		///<summary>
		///Retuns the cost of an Item
		///<summary>
		public float GetPrice()
        {
			return price;
        }
		///<summary>
		/// used as part of the receipt. !!must be redefined !!
		///<summary>
		virtual public string ReceiptInfo()
		{
			throw new NotImplementedException();
		}
	}
}