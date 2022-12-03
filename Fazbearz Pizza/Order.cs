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
        
        public DateTime date;
        public bool isPickUp;
        public List<Item> items;
        public PaymentTypeEnum paymentType;
        

        ///<summary>
        ///repesents an order
        ///<summary>
        public Order()
        {
            date = DateTime.Now;
            items = new List<Item>();
        }



        ///<summary>
        ///Retuns the relivent portion of the Receipt
        ///
		///<summary>
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
        public string ReceiptInfo() // used as part of the recceipt.  
        
        {
            string temp;
            float subTotal = getPrice();
            float Tax = (0.06f * subTotal);     // assuming a 6% sales tax
            float Total = Tax + subTotal;
            temp = "Date & Time: "+date + ": " + OrderSlip() +
                "\nSubTotal: "+ Math.Round(subTotal, 2).ToString("C2", new CultureInfo("en-US")) +
                "\nTax: " + Math.Round(Tax, 2).ToString("C2", new CultureInfo("en-US")) +
                "\nTotal: " + Math.Round(Total, 2).ToString("C2", new CultureInfo("en-US")) + " \nPayment type: "+ paymentType;
            if (isPickUp) temp = temp + " Order Type: Pickup";
            else temp = temp + " Order Type: Delivery";
            return temp;

        }


        ///<summary>
        ///Retuns a summerised portion of the Receipt
        ///<summary>
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
        public string OrderSlip() 
        {
            
            string temp = "";

            foreach (Item item in items)
            {
                temp = temp+"\n "+item.ReceiptInfo();
            }

            return temp;
        }


        ///<summary>
        ///Retuns the cost of an order
		///<summary>
        public float getPrice()
        {
            float temp =0;
            foreach(Item item in items)
            {
                temp += item.GetPrice();
            }
            return temp;
        }


        ///<summary>
        ///adds the give Item to The Item list
		///<summary>
        /// <param name="item"></param>
        public void addItem(Item item)
        {
            items.Add(item);
        }



        ///<summary>
        ///removes the Item at the given index from the item list
        ///<summary>
        /// <param name="I"></param>
        public void removeItem(int I)
        {
            if (items.Count > I) items.RemoveAt(I);
        }


        ///<summary>
        ///Retuns the time and date the order was ordered as a string
		///<summary>
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
