using System;
using System.Text.Json.Serialization;

public class Customer
{
    public string name { get; set; }
    public string address { get; set; }

    public string directions{ get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zipcode { get; set; }
    public string username { get; set; }
    public string password { get; set; }

    [JsonConstructor]/// used as part of json
    public Customer()
    {
        
    }
    public Customer( string u, string p, string n, string a, string d, string c, string s, string z)
    {
        username = u;
        password = p;
        name = n;
        address = a;
        if (d.Equals("Address 2 / Delivery Instructions (Optional)"))
            directions = "";
        else directions = d;
        city = c;
        state = s;
        zipcode = z;

    }
}
