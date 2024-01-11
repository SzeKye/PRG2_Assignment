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
    class Topping
    {
        private string type;
        public string Type {  get { return type; } set { type = value; } }
        public Topping(string type)
        {
            Type = type;
        }
        public override string ToString()
        {
            return "";
        }
    }
}
