using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using Fazbearz_Pizza;
// using Newtonsoft.Json;
public class Database
{
    private string fileName = System.Windows.Forms.Application.LocalUserAppDataPath + @"\fazbase.txt";
    private static readonly JsonSerializerOptions _options =
       new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    /// <summary>
    /// Constructor that creates a new file if one does not exist and also catches other exceptions
    /// </summary>
    public Database()
    {
        
        try
        {
            if (!File.Exists(fileName))
            {
                CreateCustomerAccount(new Customer("example", "12345", "bob", "123 real street", "turn right at light", "Marietta", "GA", "30060"));
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
        }

    }

    /// <summary>
    /// Adds a new customer account as a dataBaseObject to the end of the json file
    /// </summary>
    /// <param name="customer"></param>
    public void CreateCustomerAccount(Customer customer)
    {
        string customerInfo = "!" + JsonSerializer.Serialize(new dataBaseObject(customer));


        File.AppendAllText(fileName, customerInfo);
       
    }

    /// <summary>
    /// Returns a customer object that matches given username
    /// </summary>
    /// <param name="UserName"></param>
    /// <returns></returns>
    public Customer GetCustomer(string UserName, string password)
    {
        dataBaseObject temp = getDataObject(UserName, password);
        if (temp == null){
            return null;
        }
        return temp.customer;
    }

    /// <summary>
    /// Returns an array of customers from the array of dataObjects
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// Returns the total number of orders on a customer account's history
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public int GetTotalOrders(string username, string password)
    {
        dataBaseObject temp = getDataObject(username, password);
        if (temp == null)
        {
            return 0;
        }
        return temp.Orders.Count;
    }


    /// <summary>
    /// Calls getDataObjectArray() to create an array of dataBaseObjects. If the object with the matching username is found, string order is added to the customer's order array.
    /// The new changes overwrite the file.
    /// </summary>
    /// <param name="order"></param>
    /// <param name="UserName"></param>
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


    /// <summary>
    /// Splits every databaseObject inside of the file into an array.
    /// Returns the deserialized objects as a dataBaseObject array.
    /// </summary>
    /// <returns></returns>
    public dataBaseObject[] getDataObjectArray()
    {
        string temp = File.ReadAllText(fileName);
        string[] objects = temp.Split('!');
        List<dataBaseObject> goal = new List<dataBaseObject>();
        foreach (string obj in objects)
        {
            if (obj.Equals("")) continue;
            dataBaseObject temp2 = JsonSerializer.Deserialize<dataBaseObject>(obj)!;
            goal.Add(temp2);
        }
        
        return goal.ToArray();
    }


    /// <summary>
    /// Returns a single dataBaseObject by searching through the file and deserializing the object with matching username.
    /// Returns null if matching username is not found.
    /// </summary>
    /// <param name="UserName"></param>
    /// <returns></returns>
    public dataBaseObject getDataObject(string UserName, string password)
    {
        string temp = File.ReadAllText(fileName);
        string[] objects = temp.Split('!');
        dataBaseObject goal = null;
        foreach (string obj in objects)
        {
            if (obj != null) {

                if (obj.Equals("")) continue;
                dataBaseObject temp2 = JsonSerializer.Deserialize<dataBaseObject>(obj)!; 
                if(temp2.customer.username == UserName && temp2.customer.password == password)
                {
                    goal = temp2;
                    break;

                }
            }
        }

        return goal;
      
    }
}

/// <summary>
/// A dataBaseObject is a customer object that also holds a string list of orders.
/// </summary>
public class dataBaseObject
{
    public Customer customer { get; set; }
    public List<string> Orders { get; set; }

    [JsonConstructor]
    public dataBaseObject()
    {
        
    }
    public dataBaseObject(Customer customer)
    {
        this.customer = customer;
        Orders = new List<string>();
    }
    
}
