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
    class Waffle: IceCream
    {
        private string waffleFlavour;
        
        public string WaffleFlavour { get { return waffleFlavour;} set { waffleFlavour = value; } }
        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings,string waffleFlavour) : base(option, scoops, flavours, toppings)
        {
            
            WaffleFlavour = waffleFlavour;
        }
        public override double CalculatePrice()
        {
            double total_price = 0;
            if (Scoops == 1)
            {
                total_price += 7.00;
                foreach (Flavour f in Flavours)
                {
                    total_price += f.Quantity * 2;
                }

            }
            else if (Scoops == 2)
            {
                total_price += 8.50;
                foreach (Flavour f in Flavours)
                {
                    total_price += f.Quantity * 2;
                }
            }
            else if (Scoops == 3)
            {
                total_price += 9.50;
                foreach (Flavour f in Flavours)
                {
                    total_price += f.Quantity * 2;
                }
            }
            total_price += Toppings.Count();
            if (WaffleFlavour != "nil")  
            {
                total_price += 3;
            }
            return total_price;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
