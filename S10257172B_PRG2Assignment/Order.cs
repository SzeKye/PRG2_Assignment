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
    class Order
    {
        private int id;
        private DateTime timeReceived;
        private DateTime? timeFulfilled;
        private List<IceCream> iceCreamList;

        public int Id { get { return id; } set { id = value; } }
        public DateTime TimeReceived { get {  return timeReceived; } set {  timeReceived = value; } }
        public DateTime? TimeFulfilled { get {  return timeFulfilled; } set { timeFulfilled = value; } }
        public List<IceCream> IceCreamList { get; set; } = new List<IceCream>();

        public Order(int id, DateTime timeReceived)
        {
            
            Id = id;
            TimeReceived = timeReceived;
            
        }
        public void ModifyIceCream(int Id)
        {

        }
        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }
        public void DeleteIceCream(int id)
        {

        }
        public double CalculateTotal()
        {
            double total_cost = 0;
            foreach(IceCream cream in IceCreamList)
            {
                if(cream is Cup)
                {
                    Cup cup = (Cup)cream;
                    double c_price = cup.CalculatePrice();
                    total_cost += c_price;
                }
                else if (cream is Cone)
                {
                    Cone cone = (Cone)cream;
                    double co_price = cone.CalculatePrice();
                    total_cost += co_price;
                }
                else if (cream is Waffle)
                {
                    Waffle waffle = (Waffle)cream;
                    double waf_price = waffle.CalculatePrice();
                    total_cost += waf_price;
                }
            }
            return total_cost;
        }
        public override string ToString()
        {
            string tr = TimeReceived.ToString("dd/MM/yyyy h:mm:ss tt");
            string tf = null;
            if(TimeFulfilled != null)
            {
                tf += TimeFulfilled.Value.ToString("dd/MM/yyyy h:mm:ss tt");
            }
            else
            {
                tf += $"Not Fulfilled";
            }
            return $"OrderID: {Id,-5} TimeReceived: {tr,-25} TimeFulfilled: {tf}";
        }


    }
}
