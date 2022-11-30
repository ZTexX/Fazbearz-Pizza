using System;

public class Customer
{
    public string name { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zipcode { get; set; }
    public string username { get; set; }
    public string password { get; set; }



    public Customer(string c, string u, string p, string n, string a, string s, string z)
    {
        username = u;
        password = p;
        name = n;
        address = a;
        city = c;
        state = s;
        zipcode = z;

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

    public string ReceiptInfo()
    /*
       Example:
    name:bob
    Address: address, city, state zipcode

     */
    {
        return "name:" + name +
               "\nAddress:" + address + ", " + city + ", " + state + " " + zipcode;
    }
}
