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
    class Customer
    {
        private string name;
        private int memberid;
        private DateTime dob;
        private Order currentOrder;
        private List<Order> orderHistory;
        private PointCard rewards;

        public string Name {  get { return name; } set {  name = value; } }
        public int Memberid { get {  return memberid; } set {  memberid = value; } }
        public DateTime Dob { get {  return dob; } set {  dob = value; } }
        public Order CurrentOrder { get {  return currentOrder; } set {  currentOrder = value; } }
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public PointCard Rewards { get {  return rewards; } set {  rewards = value; } }

        public Customer(string name, int memberid, DateTime dob)
        {
            Name = name;
            Memberid = memberid;
            Dob = dob;
            
        }
        public Order MakeOrder() // returns a new order object associated to the customer
        {
            int id = 0;
            using (StreamReader sr = new StreamReader("orders.csv")) 
            {
                string? s;
                bool x = true;
                while((s = sr.ReadLine()) != null)
                {
                    if (x)
                    {
                        x=false;
                        continue;
                    }
                    string[] strings = s.Split(",");
                    id = int.Parse(strings[0]);
                }
            }
            id++;
            Order newOrder = new Order(id, DateTime.Now);
            return newOrder; 
        }
        public bool IsBirthday()
        {
            int counter = 0;
            bool isBirthdayToday =  Dob.Month == DateTime.Now.Month && Dob.Day == DateTime.Now.Day;
            if (isBirthdayToday && counter == 0) {
                counter++;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            string customerDetails = Name + "," + Memberid + "," + Dob.ToString("dd/MM/yyyy") + "," + Rewards.Tier + "," + Rewards.Points + "," + Rewards.PunchCard;
            return customerDetails;
        }
    }
}
