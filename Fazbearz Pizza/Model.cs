using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IronPdf;
using Microsoft.Win32;

namespace Fazbearz_Pizza
{
    internal class Model
    {
        public Order currentOrder {get; set;}
        private Database database = new Database();
        private dataBaseObject currentUser {get; set;}
        public bool IsManager;
        private string managerUsername = "manager";
        private string managerPassword = "password";
        private string sig = "";
        

        /// <summary>
        /// Checks if entered username and password are for the manager. If so, isManager is true and currentUser is null.
        /// If entered username and password aren't for the manager, currentUser is pulled from the database and isManager is false.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
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

        /// <summary>
        /// Sets currentUser to null
        /// </summary>
        public void Logout()
        {
            currentUser = null;
        }


        /// <summary>
        /// Returns true if the currentUser is not null or loged in.
        /// </summary>
        /// <returns></returns>
        public bool LoginCheck()
        {
            return currentUser != null;
        }

        /// <summary>
        /// Takes in all parameters from text boxes in the Fazbearz_Pizza.cs class and converts them to a new Customer. 
        /// The new Customer is serialized to the json file in database.
        /// currentUser is then pulled from the newly created json object in the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="directions"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        public void CreateAccount(string username, string password, string name, string address, string directions, string city, string state, string zip)
        {
            database.CreateCustomerAccount(new Customer(username, password, name, address, directions, city, state, zip));
            currentUser = database.getDataObject(username, password);
        }

        /// <summary>
        /// Sets currentUser to the desired dataBaseObject.
        /// </summary>
        /// <param name="obj"></param>
        public void SetCurrentUser(dataBaseObject obj)
        {
            currentUser = obj;
        }


        /// <summary>
        /// Returns a string array of all orders for the currentUser.
        /// </summary>
        /// <returns></returns>
        public string[] GetCustomerOrders()
        {
            return currentUser.Orders.ToArray();
        }

        /// <summary>
        /// Returns an int of the total orders the currentUser has on their account.
        /// </summary>
        /// <returns></returns>
        public int GetNumCustomerOrders()
        {
            return database.GetTotalOrders(currentUser.customer.username, currentUser.customer.password);
 
        }

        /// <summary>
        /// Returns a dataBaseObject array of all dataBaseObjects in the json database.
        /// </summary>
        /// <returns></returns>
        public dataBaseObject[] GetDataBaseArray()
        {
            return database.getDataObjectArray();
        }

        /// <summary>
        /// Initializes currentOrder as a new order.
        /// </summary>
        public void StartNewOrder()
        {
            currentOrder = new Order();
            
        }

        /// <summary>
        /// Adds the desired item to the currentOrder.
        /// </summary>
        /// <param name="item"></param>
        public void addItem(Item item)
        {
            currentOrder.addItem(item);
        }

        /// <summary>
        /// Adds the currentOrder's ReceiptInfo to the json database.
        /// Searches by customer username.
        /// </summary>
        public void AddOrderToCustomer()
        {
            database.AddOrder(currentOrder.ReceiptInfo(), currentUser.customer.username);
        }

        /// <summary>
        /// Returns a string displaying the current username and all ReceiptInfo for the currentOrder.
        /// </summary>
        /// <returns></returns>
        public string ReceiptInfo()
        {
            return "Name: " + currentUser.customer.name + " " + currentOrder.ReceiptInfo();
        }

        /// <summary>
        /// Returns a string displaying the current username and all ReceiptInfo for the currentOrder.
        /// Adds signature to the Receipt.
        /// </summary>
        /// <returns></returns>
        public string ReceiptInfo(string signature)
        {
            sig = signature;
            return "Name: " + currentUser.customer.name + " " + currentOrder.ReceiptInfo() + Environment.NewLine + "Signature: " + signature;
        }

        /// <summary>
        /// Returns a string displaying the OrderSlip of the currentOrder.
        /// </summary>
        /// <returns></returns>
        public string OrderInfo()
        {
            return currentOrder.OrderSlip();
        }

        
        /// <summary>
        /// Converts ReceiptInfo() to rtf format.
        /// Establishes a pdf renderer that prints pdf file and saves pdf to downloads folder.
        /// </summary>
        public void PrintReceiptToPDF()
        {
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Text = ReceiptInfo(sig);
            string rtfFormattedReceipt = richTextBox.Rtf;

            ChromePdfRenderer renderer = new ChromePdfRenderer();
            
            PdfDocument pdf = renderer.RenderRtfStringAsPdf(rtfFormattedReceipt);
            string path = KnownFolders.GetPath(KnownFolder.Downloads);
            pdf.PrintToFile(path + @"\" + currentUser.customer.username + "Receipt.pdf");


        }

        /// <summary>
        /// Enum of all known folders on the system.
        /// </summary>
        private enum KnownFolder
        {
            Contacts,
            Downloads,
            Favorites,
            Links,
            SavedGames,
            SavedSearches
        }

        /// <summary>
        /// Static class of Knownfolders that returns the path of the desired folder.
        /// </summary>
        private static class KnownFolders
        {
            private static readonly Dictionary<KnownFolder, Guid> _guids = new()
            {
                [KnownFolder.Contacts] = new("56784854-C6CB-462B-8169-88E350ACB882"),
                [KnownFolder.Downloads] = new("374DE290-123F-4565-9164-39C4925E467B"),
                [KnownFolder.Favorites] = new("1777F761-68AD-4D8A-87BD-30B759FA33DD"),
                [KnownFolder.Links] = new("BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968"),
                [KnownFolder.SavedGames] = new("4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4"),
                [KnownFolder.SavedSearches] = new("7D1D3A04-DEBB-4115-95CF-2F29DA2920DA")
            };

            public static string GetPath(KnownFolder knownFolder)
            {
                return SHGetKnownFolderPath(_guids[knownFolder], 0);
            }

            [DllImport("shell32",
                CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
            private static extern string SHGetKnownFolderPath(
                [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags,
                nint hToken = 0);
        }

    }


}
