namespace Fazbearz_Pizza
{
	public class Pizza : Item
	{
		private sizeEnme size;
		private CrustEnme crust;
		private TopingsEnme[] topings;

		public Pizza(sizeEnme Tempsize, CrustEnme TempCrust, TopingsEnme[] TempToppings)
		{
			this.size = Tempsize;
			this.crust = TempCrust;
			this.topings = TempToppings;

			//derive price 
			int temPrise = 0;

			switch (Tempsize)//base cost
			{
				case sizeEnme.Small:
					temPrise = 6;
					break;

				case sizeEnme.Meddem:
					temPrise = 8;
					break;

				case sizeEnme.Large:
					temPrise = 10;
					break;

				case sizeEnme.XL:
					temPrise = 12;
					break;

				default:
					break;
			}

			if (crust == CrustEnme.Stuffed) temPrise += 4;//extra for cost stuffed crust

			temPrise += topings.Length;//+1 per-topping


			this.prise = temPrise;// apply price
		}

		public override string ReceiptInfo() // used as part of the recceipt.  
		/*
		 return Example:
		 "Pizza: 19$ Size: Extra Large
		   Toppings- 
			Extra Cheese
			pepeprs
			Olives"
		 */
		{
			string temp = "Pizza: " + this.prise+"$ Size:";
			if (size == sizeEnme.XL) temp = temp + " Extra Large"; //change XL means  Extra Large
			else temp = temp + size;

			if (topings.Length != 0)        // The if there are topping add them the receipt
			{
				temp = temp + "\n  Toppings- ";
				for (int i = 0; i < topings.Length; i++)
				{
					if (topings[i] == TopingsEnme.ExCheese) temp = temp + "\n   Extra Cheese";
					else temp = temp + "\n   " + topings[i];
				}
			}
			return temp;
		}

	}

	public enum sizeEnme // represenst Size of a pizza
	{
		Small,      
		Meddem,     
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
		Anchovies,
		ExCheese,   //extra Cheese      
		Olives,           
		pepeprs,
		Saussage
	}
}