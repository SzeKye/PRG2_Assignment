﻿//==========================================================
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
    class Flavour
    {
        private string type;
        private bool premium;

        public string Type {  get { return type; } set { type = value; } }
        public bool Premium { get {  return premium; } set {  premium = value; } }
        public Flavour(string type, bool premium)
        {
            Type = type;
            Premium = premium;            
        }
        public override string ToString()
        {
            return $"\nType: {Type,-12} Premium: {Premium,-6}";
        }
    }
}
