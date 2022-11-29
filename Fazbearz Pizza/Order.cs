using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fazbearz_Pizza
{
    internal class Order
    {
        private int number;
        private DateTime date;
        public bool isPickUp;
        private List<Item> items;
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
}
