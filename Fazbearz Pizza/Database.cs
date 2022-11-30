using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
// using Newtonsoft.Json;
public class Database
{
    private string fileName = Application.LocalUserAppDataPath + @"\fazbase.txt";
    private static readonly JsonSerializerOptions _options =
       new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    public Database()
    {
        try
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

    }

    public void CreateCustomerAccount(Customer customer)
    {
        string customerInfo = "!" + JsonSerializer.Serialize(new dataBaseObject(customer));


        File.AppendAllText(fileName, customerInfo);

    }
    private dataBaseObject getObject()
    {

    }
}

public class dataBaseObject
{
    public dataBaseObject(Customer customer)
    {
        this.customer = customer;
        Orders = new List<string>();
    }
    public Customer customer { get; set; }
    public List<String> Orders { get; set; }
}
