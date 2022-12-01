using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace Fazbearz_Pizza
{
    internal class Drink: Item
    {
        private DrinkSizeEnum Size;
        private DrinkTypeEnum Type;
        public Drink(DrinkTypeEnum type, DrinkSizeEnum size)
        {

            this.Type = type;
            this.Size = size;

            //derive price 
            switch (size)
            {
                case DrinkSizeEnum.Small:
                    this.price = 1.99f;
                    break;
                case DrinkSizeEnum.Medium:
                    this.price = 2.99f;
                    break;
                case DrinkSizeEnum.Large:
                    this.price = 3.99f;
                    break;
                default:
                    break;
            }
        }
        public override string ReceiptInfo() // used as part of the recceipt.
        /*
         return Example: "drink: Coke Size: Small price: $2"
         */
        {
            
           return "Drink: " + Type + "\n  Size: " + Size + "\n  Price: " +Math.Round(price,2).ToString("C2", new CultureInfo("en-US"));
           
        }
    }
    enum DrinkTypeEnum// represenst Tpye of a Drink
    {
        CocaCola,
        CocaColaCherry,
        Sprite,
        DrPepper,   
        MountainDew,
        FantaOrange,
        MinuteMaidLemonade

        
    }
    enum DrinkSizeEnum // represenst Size of a Drink
    {
        Small,
        Medium,
        Large
    }
}
