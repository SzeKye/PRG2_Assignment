//==========================================================
// Student Number : S10257172B
// Student Name : Loh Sze Kye
// Partner Name : Liew Yong Hong
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
            if(Scoops == 1)
            {
                total_price += 4;
                foreach (Flavour f in Flavours)
                {
                    if (f.Premium == true)
                    {
                        total_price += 2;
                    }
                }
                total_price += Toppings.Count();
                return total_price;
            }
            else if(Scoops == 2)
            {
                total_price += 5.5;
                foreach (Flavour f in Flavours)
                {
                    if (f.Premium == true)
                    {
                        total_price += 2;
                    }
                }
                total_price += Toppings.Count();
                return total_price;
            }
            else if(Scoops == 3)
            {
                total_price += 6.5;
                foreach (Flavour f in Flavours)
                {
                    if (f.Premium == true)
                    {
                        total_price += 2;
                    }
                }
                total_price += Toppings.Count();
                return total_price;
            }
            else
            {
                return 0;
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
