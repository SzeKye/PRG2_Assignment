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
    class Cone: IceCream
    {
        private bool dipped;
        public bool Dipped {  get { return dipped; } set {  dipped = value; } }
        public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped) : base(option, scoops, flavours, toppings)
        {
            Dipped = dipped;
        }
        public override double CalculatePrice()
        {
            double total_price = 0;
            if(Scoops == 1)
            {
                total_price += 4;
                foreach(Flavour f in Flavours)
                {
                    if(f.Premium == true)
                    {
                        total_price += 2;
                    }
                }
                total_price += Toppings.Count();
                if(dipped == true)
                {
                    total_price += 2;
                }
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
                if (dipped == true)
                {
                    total_price += 2;
                }
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
                if (dipped == true)
                {
                    total_price += 2;
                }
                return total_price;
            }
            else
            {
                return 0;
            }
        }
        public override string ToString()
        {
            return base.ToString()+$"\nDipped: {Dipped}";
        }
    }
}
