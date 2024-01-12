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
            if (Scoops == 1)
            {
                if (WaffleFlavour == "red velvet" || WaffleFlavour == "charcoal" || WaffleFlavour == "pandan") 
                {
                    return 7 + Toppings.Count() * 1 + 3;
                }
                else if(WaffleFlavour == "durian" || WaffleFlavour == "ube" || WaffleFlavour == "sea salt")
                {
                    return 7 + Toppings.Count();
                }
                return 7 + Toppings.Count() * 1;
            }
            else if (Scoops == 2)
            {
                if (WaffleFlavour == "red velvet" || WaffleFlavour == "charcoal" || WaffleFlavour == "pandan")
                {
                    return 8.5 + Toppings.Count() * 1 + 3;
                }
                return 8.5 + Toppings.Count() * 1;
            }
            else if(Scoops == 3)
            {
                if (WaffleFlavour == "red velvet" || WaffleFlavour == "charcoal" || WaffleFlavour == "pandan")
                {
                    return 9.5 + Toppings.Count() * 1 + 3;
                }
                return 9.5 + Toppings.Count() * 1;
            }
            return 0;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
