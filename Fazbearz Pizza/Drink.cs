using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    this.price = 2;
                    break;
                case DrinkSizeEnum.Medium:
                    this.price = 3;
                    break;
                case DrinkSizeEnum.Large:
                    this.price = 4;
                    break;
                default:
                    break;
            }
        }
        public override string ReceiptInfo() // used as part of the recceipt.
        /*
         return Example: "drink: Coke Size: Small price: 2$"
         */
        {
            switch (Type) // Fix the Type Name
            {
                case DrinkTypeEnum.DrPepper:
                    return "drink: Dr.Pepper Size:" + Size + " price: " + price + "$" ;
                case DrinkTypeEnum.MountainDew:
                    return "drink: Mountain Dew Size:" + Size + " price: " + price + "$";
                default:
                    return "drink: " + Type + " Size:" + Size + " price: " + price + "$";
            }
        }
    }
    enum DrinkTypeEnum// represenst Tpye of a Drink
    {
        DrPepper,   // Dr.Pepper
        Coke,
        MountainDew,//Mountain Dew
        Sprite,
        Monster
    }
    enum DrinkSizeEnum // represenst Size of a Drink
    {
        Small,
        Medium,
        Large
    }
}
