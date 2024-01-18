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
            double total_price = 0;
            using (StreamReader sr = new StreamReader("options.csv"))
            {
                bool z = false;
                string? s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (!z)
                    {
                        z = true;
                        continue;
                    }
                    string[] str = s.Split(",");
                    if (str[0] == "Cone")
                    {
                        if (Scoops == (Convert.ToInt32(str[1])))
                        {
                            if (Dipped == Convert.ToBoolean(str[2]))
                            {
                                total_price += Convert.ToDouble(str[4]);

                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            total_price += Toppings.Count();
            foreach (Flavour f in Flavours)
            {
                if (f.Premium == true)
                {
                    total_price += (f.Quantity * 2);
                }
            }
            return total_price;
        }
        public override string ToString()
        {
            return base.ToString()+$"\nDipped: {Dipped}";
        }
    }
}
