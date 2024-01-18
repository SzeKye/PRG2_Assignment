﻿//==========================================================
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
    class Flavour
    {
        private string type;
        private bool premium;
        private int quantity;

        public string Type {  get { return type; } set { type = value; } }
        public bool Premium { get {  return premium; } set {  premium = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public Flavour(string type, bool premium, int quantity)
        {
            Type = type;
            Premium = premium;
            Quantity = quantity;
            
        }
        public override string ToString()
        {
            return $"Type: {Type,-12} Premium: {Premium,-6} Quantity: {Quantity}\n";
        }
    }
}
