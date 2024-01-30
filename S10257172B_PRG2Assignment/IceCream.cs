//==========================================================
// Student Number : S10257564A
// Student Name : Liew Yong Hong
// Partner Name : Loh Sze Kye
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
            string flavoursStr = string.Join("", Flavours.Select(f => f.ToString())); //Let the flavour list in flavour class to be a string
            string toppingsStr = string.Join(", ", Toppings.Select(t => t.ToString())); //Let the topping list in topping class to be a string
            return $"Option: {Option.ToUpper()} Scoops: {Scoops} \nFlavours Details:\n{flavoursStr} \nToppings: {toppingsStr}\n";
        }
    }
}
