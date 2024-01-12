//==========================================================
// Student Number : S10257172B
// Student Name : Loh Sze Kye
// Partner Name : 
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257172B_PRG2Assignment
{
    class Cup: IceCream 
    {
        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings)
        {

        }
        public override double CalculatePrice()
        {
            if(Scoops == 1)
            {
                return 4 * Toppings.Count() * 1;
            }
            else if(Scoops == 2)
            {
                return 5.5 * Toppings.Count() * 1;
            }
            else if (Scoops == 3)
            {
                return 6.5 * Toppings.Count() * 1;
            }
            return 0;  
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
