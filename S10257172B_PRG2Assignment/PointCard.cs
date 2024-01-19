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
    class PointCard
    {
        private int points;
        private int punchCard;
        private string tier;

        public int Points {  get { return points; } set { points = value; } }
        public int PunchCard { get {  return punchCard; } set {  punchCard = value; } }
        public string Tier { get { return tier; } set { tier = value; } }
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
        }

        public void AddPoints(int points)
        {
            Points += points;
        }
        public void RedeemPoints(int points)
        {
            Points -= points;
        }
        public void Punch()
        {

        }
        public override string ToString()
        {
            return "";
        }
    }
}
