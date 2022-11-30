using System;

public class Customer : Person
{
	private string name;
	private string address;
	private string
	private bool isVisaCard;

	public Customer()
	{
		
	}

    public bool Login(string str)
    {
        if (str.Equals(password))
        {
            return true;
        }
        else
            return false;
    }

    public virtual string ReceiptInfo()
    {
        return Username; //placeholder
    }



}
