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
            if (Scoops == 1)
            {
                if (dipped)
                {
                    return 4 + Toppings.Count() * 1 + 2;
                }
                return 4 + Toppings.Count() * 1;
            }
            else if (Scoops == 2)
            {
                if (dipped)
                {
                    return 5.5 + Toppings.Count() * 1 + 2;
                }
                return 5.5 + Toppings.Count() * 1;
            }
            else if(Scoops == 3)
            {
                if (dipped)
                {
                    return 6.5 + Toppings.Count() * 1 + 2;
                }
                return 6.5 + Toppings.Count() * 1;
            }
            return 0;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
