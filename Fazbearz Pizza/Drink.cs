using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fazbearz_Pizza
{
    internal class Drink: Item
    {
        private DrinkSize Size;
        private DrinkType Type;
        public Drink(DrinkType type, DrinkSize size)
        {

            this.Type = type;
            this.Size = size;

            //derive price 
            switch (size)
            {
                case DrinkSize.Small:
                    this.prise = 2;
                    break;
                case DrinkSize.Medium:
                    this.prise = 3;
                    break;
                case DrinkSize.Large:
                    this.prise = 4;
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
                case DrinkType.DrPepper:
                    return "drink: Dr.Pepper Size:" + Size + " price: " + prise + "$" ;
                case DrinkType.MountainDew:
                    return "drink: Mountain Dew Size:" + Size + " price: " + prise + "$";
                default:
                    return "drink: " + Type + " Size:" + Size + " price: " + prise + "$";
            }
        }
    }
    enum DrinkType// represenst Tpye of a Drink
    {
        DrPepper,   // Dr.Pepper
        Coke,
        MountainDew,//Mountain Dew
        Sprite,
        Monster
    }
    enum DrinkSize // represenst Size of a Drink
    {
        Small,
        Medium,
        Large
    }
}
