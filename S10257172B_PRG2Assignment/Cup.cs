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
            Dictionary<int, double> price = new Dictionary<int, double>();
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
                    if (str[0] == "Cup")
                    {
                        price.Add(Convert.ToInt32(str[1]), Convert.ToDouble(str[4]));
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            foreach (int scCount in price.Keys)
            {
                if (Scoops.Equals(scCount))
                {
                    total_price += price[scCount];
                    foreach (Flavour f in Flavours)
                    {
                        if (f.Premium == true)
                        {
                            total_price += (f.Quantity * 2);
                        }
                    }


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
