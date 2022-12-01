using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fazbearz_Pizza
{
    internal class Model
    {
        //vairable: currentorder, database, currentuser
        private Order currentOrder;
        private Database database = new Database();
        private dataBaseObject currentUser;
        public bool IsManager;
        private string managerUsername = "manager";
        private string managerPassword = "password";
        //functions:
        //Login()

        public void Login(string username, string password)
        {
            if(username == managerUsername && password == managerPassword)
            {
                currentUser = null;
                IsManager = true;
            }
            else
            {
                currentUser = database.getDataObject(username, password);
                IsManager = false;
            }
                
            
            
        }

        public void Logout()
        {
            currentUser = null;
        }

        public bool LoginCheck()
        {
            return currentUser != null;
        }

        public void CreateAccount(string username, string password, string name, string address, string directions, string city, string state, string zip)
        {
            database.CreateCustomerAccount(new Customer(username, password, name, address, directions, city, state, zip));
            currentUser = database.getDataObject(username, password);
        }


        //getCustomerOrders()
        public string[] GetCustomerOrders()
        {
            return currentUser.Orders.ToArray();
        }

        //GetNumCustomerOrders()
        public int GetNumCustomerOrders()
        {
            return database.GetTotalOrders(currentUser.customer.username, currentUser.customer.password);
 
        }

        //StartNewOrder()
        //Fix order index
        public void StartNewOrder()
        {
            currentOrder = new Order();
            //database.AddOrder(order, currentUser.customer.username);
        }

        //addItem(item)
        public void addItem(Item item)
        {
            currentOrder.addItem(item);
        }

        public void AddOrderToCustomer()
        {
            database.AddOrder(currentOrder.ReceiptInfo(), currentUser.customer.username);
        }

        //ReceiptInfo()
        public string ReceiptInfo()
        {
            return currentOrder.ReceiptInfo();
        }

        //PrintReceipt()

    }


}
