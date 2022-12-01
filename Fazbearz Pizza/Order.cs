using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Globalization;

namespace Fazbearz_Pizza
{
    class Order : ObjectIDGenerator
    {
        public long id;
        public DateTime date;
        public bool isPickUp;
        public List<Item> items;
        public PaymentTypeEnum paymentType;
        private bool firstTime = true;
        
        public Order()
        {
            this.id = GetId(this, out bool firstTime);
            date = DateTime.Now;
            items = new List<Item>();
        }

        public string ReceiptInfo() // used as part of the recceipt.  
        /*
         Example:
        "
Date & Time:11/29/2022 5:28:55 PM: Order Number: 69420
 Pizza: 19$ Size: Extra Large
  Toppings- 
   Extra Cheese
   pepeprs
   Olives
 drink: Dr.Pepper Size:Large price: 4$
subTotal:23$
Tax:1.38$
Total:24.38$
        "
         */
        {
            string temp;
            float subTotal = getPrice();
            float Tax = (0.06f * subTotal);     // assuming a 6% sales tax
            float Total = Tax + subTotal;
            temp = "Date & Time: "+date + ": " + OrderSlip() +
                "\nSubTotal: "+ Math.Round(subTotal, 2).ToString("C2", new CultureInfo("en-US")) +
                "\nTax: " + Math.Round(Tax, 2).ToString("C2", new CultureInfo("en-US")) +
                "\nTotal: " + Math.Round(Total, 2).ToString("C2", new CultureInfo("en-US")) + " \npayment type: "+ paymentType;
            if (isPickUp) temp = temp + " Order Type: Pickup";
            else temp = temp + " Order Type: Delivery";
            return temp;

        }


        /*
             Exmple:
            "
Order Number: 69420
 Pizza: 19$ Size: Extra Large
  Toppings- 
   Extra Cheese
   pepeprs
   Olives
 drink: Dr.Pepper Size:Large price: 4$
            "
             */
        public string OrderSlip() // used as part of the recceipt.  
        {

            string temp = "";

            foreach (Item item in items)
            {
                temp = temp+"\n "+item.ReceiptInfo();
            }

            return temp;
        }

        public float getPrice()// get the prices of the enter order
        {
            float temp =0;
            foreach(Item item in items)
            {
                temp += item.GetPrice();
            }
            return temp;
        }
        public void addItem(Item item) //adds the give Item to The Item list
        {
            items.Add(item);
        }
        public void removeItem(int I)// removes the Item at the given index from the item list
        {
            if (items.Count > I) items.RemoveAt(I);
        }
        public string getDateTime()
        {
            return date.ToString();
        }
    }

    enum PaymentTypeEnum
    {
        Card,
        Check,
        Cash
    } 
}
