namespace Fazbearz_Pizza
{
	public class Pizza : Item
	{
		private sizeEnme size;
		private CrustEnme crust;
		private TopingsEnme[] topings;

		///<summary>
		///Represents a Pizza in an order
		///</summary>
		public Pizza(sizeEnme Tempsize, CrustEnme TempCrust, TopingsEnme[] TempToppings)
		{
			this.size = Tempsize;
			this.crust = TempCrust;
			this.topings = TempToppings;

			//derive price 
			float temPrice = 0;

			switch (Tempsize)//base cost
			{
				case sizeEnme.Small:
					temPrice = 8.99f;
					break;

				case sizeEnme.Medium:
					temPrice = 12.99f;
					break;

				case sizeEnme.Large:
					temPrice = 15.99f;
					break;

				case sizeEnme.XL:
					temPrice = 19.99f;
					break;

				default:
					break;
			}

			if (crust == CrustEnme.Stuffed) temPrice += 3.99f;//extra for cost stuffed crust

			temPrice += topings.Length*0.99f;//+1 per-topping


			this.price = temPrice;// apply price
		}
        ///<summary>
        ///Retuns the relivent portion of the Receipt
        ///</summary>
        ///
        /*
		 return Example:
		 "Pizza: 19$ Size: Extra Large
		   Toppings- 
			Extra Cheese
			pepeprs
			Olives"
		 */
        public override string ReceiptInfo() // used as part of the recceipt.  
		
		{
			string temp = "Pizza: " + Math.Round(price,2).ToString("C2", new System.Globalization.CultureInfo("en-US")) + " Size: ";
			if (size == sizeEnme.XL) temp = temp + " Extra Large"; //change XL means  Extra Large
			else temp = temp + size;

			if (topings.Length != 0)        // The if there are topping add them the receipt
			{
				temp = temp + "\n  Toppings- ";
				for (int i = 0; i < topings.Length; i++)
				{
					if (topings[i] == TopingsEnme.ExCheese) temp = temp + "\n   Extra Cheese";
					else if (topings[i] == TopingsEnme.BananaPeppers) temp = temp + "\n   Banana Peppers";
					else temp = temp + "\n   " + topings[i];
				}
			}
			return temp;
		}

	}

	public enum sizeEnme // represenst Size of a pizza
	{
		Small,      
		Medium,     
		Large,      
		XL      //Extra Large
	}

	public enum CrustEnme //represenst pizza crust type 
	{
		Thin,
		Thick,
		Stuffed
	}
	public enum TopingsEnme  //represenst a pizza toping
	{
		Pepperoni,
		Mushrooms,
		Chicken,
		Onions,
		BananaPeppers, //BannaPeppers
		ExCheese,   //extra Cheese  
		Olives,           
		Peppers,
		Sausage,
		Bacon,
		Pinapple
	}
}