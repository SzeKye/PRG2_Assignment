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
    abstract class IceCream
    {
        private string option;
        private int scoops;
        private List<Flavour> flavours;
        private List<Topping> toppings;

        public string Option { get { return option; } set { option = value; } }
        public int Scoops { get { return scoops; } set { scoops = value; } }
        public List<Flavour>Flavours = new List<Flavour>();
        public List<Topping>Toppings = new List<Topping>();

        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }
        public abstract double CalculatePrice();
        
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
