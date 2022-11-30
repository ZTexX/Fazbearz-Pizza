using System;

public class Customer : Person
{
	private string name;
	private string address;
	private bool isVisaCard;

	public Customer()
	{
		
	}

	public override string ReceiptInfo()
	{
		return base.ReceiptInfo(); //placeholder
	}
	
		
	
}
