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
            if (Points >= 50 && Tier == "Ordinary")
            {
                Tier = "Silver";
                Console.WriteLine("Congraulations! You are now a silver tier member and can start redeeming your points to offset your total bill!\n" +
                    "Achieve 100 points or more to become a gold tier member");
            }
            else if (Points >= 100 && Tier == "Silver")
            {
                Tier = "Gold";
                Console.WriteLine("Thank you for being such a loyal customer, as part of your gold tier member status, subsequent orders from this point forth will be added to the priority gold queue.");
            }
        }
        public void RedeemPoints(int points)
        {
            Points -= points;
            Console.WriteLine("Points redeemed successfully.");
        }
        public void Punch()
        {
            using (StreamWriter sw = new StreamWriter("customers.csv",true))
            {
                sw.WriteLine(PunchCard);
            }
            
        }
        public override string ToString()
        {
            return $"Point: {Points} PunchCard: {PunchCard} Tier: {Tier}";
        }
    }
}
