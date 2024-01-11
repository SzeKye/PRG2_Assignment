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
            double total_price = 0;
            if (Scoops == 1)
            {
                total_price += 4.00;
                foreach(Flavour f in Flavours)
                {
                    total_price += f.Quantity * 2;
                }

            }
            else if (Scoops == 2)
            {
                total_price += 5.50;
                foreach (Flavour f in Flavours)
                {
                    total_price += f.Quantity * 2;
                }
            }
            else if (Scoops == 3)
            {
                total_price += 6.50;
                foreach (Flavour f in Flavours)
                {
                    total_price += f.Quantity * 2;
                }
            }
            total_price += Toppings.Count();
            return total_price;
            
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
