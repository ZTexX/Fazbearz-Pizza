using System;

public class Class1
{
	private string username;
	private string password;


	public Class1()
	{
	}

	public string Username()
	{
		get { return username; }
		set { return username; }
	}

	public string Password()
	{
		get { return password; }
		set { return password; }
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
}
