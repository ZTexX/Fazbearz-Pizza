using System;

public class Person
{
	private string username;
	private string password;


	public Person()
	{
	}

	public string Username
	{
		get { return username; }
		set { username = value; }
	}

	public string Password
	{
		get { return password; }
		set { password = value; }
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
