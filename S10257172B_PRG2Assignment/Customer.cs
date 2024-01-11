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
            Order newOrder = new Order(Memberid, DateTime.Now);
            return newOrder; 
        }
        public bool IsBirthday()
        {
            if (Dob == DateTime.Now) {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return "";
        }
    }
}
