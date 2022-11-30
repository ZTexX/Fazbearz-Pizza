using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
// using Newtonsoft.Json;
public class Database
{
    private string fileName = System.Windows.Forms.Application.LocalUserAppDataPath + @"\fazbase.txt";
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
    public Customer GetCustomer(string UserName)
    {
        dataBaseObject temp = getDataObject(UserName);
        if (temp == null){
            return null;
        }
        return temp.customer;
    }
    public Customer[] GetCustomerArray()
    {
        dataBaseObject[] objects = getDataObjectArray();
        List<Customer> goal = new List<Customer>();
        foreach (dataBaseObject obj in objects)
        {
            goal.Add(obj.customer);
        }

        return goal.ToArray();
    }

    public int GetTotalOrders(string username)
    {
        dataBaseObject temp = getDataObject(username);
        if (temp == null)
        {
            return 0;
        }
        return temp.Orders.Count;
    }

    public void AddOrder(string order, string UserName)
    {
        dataBaseObject[] objects = getDataObjectArray();
        int index = 0;
        foreach (dataBaseObject obj in objects)
        {
            
            if (obj.customer.username == UserName) break;

            index++;
        }
        objects[index].Orders.Add(order);

        string customerInfo = "";
        foreach (dataBaseObject obj in objects)
        {
            customerInfo = customerInfo + "!" + JsonSerializer.Serialize(obj); 
        }
        File.WriteAllText(fileName, customerInfo);
     }

    private dataBaseObject[] getDataObjectArray()
    {
        string temp = File.ReadAllText(fileName);
        string[] objects = temp.Split('!');
        List<dataBaseObject> goal = new List<dataBaseObject>();
        foreach (string obj in objects)
        {
            dataBaseObject temp2 = JsonSerializer.Deserialize<dataBaseObject>(obj)!;
            goal.Add(temp2);
        }
        
        return goal.ToArray();
    }

    private dataBaseObject getDataObject(string UserName)
    {
        string temp = File.ReadAllText(fileName);
        string[] objects = temp.Split('!');
        dataBaseObject goal = null;
        foreach (string obj in objects)
        {
            if (obj != null) {
                dataBaseObject temp2 = JsonSerializer.Deserialize<dataBaseObject>(obj)!; 
                if(temp2.customer.username == UserName)
                {
                    goal = temp2;
                    break;

                }
            }
        }

        return goal;
      
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
    public List<string> Orders { get; set; }
}
