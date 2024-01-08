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

        }
        public void DeleteIceCream(int id)
        {

        }
        public double CalculateTotal()
        {
            return 0;
        }
        public override string ToString()
        {
            return "";
        }


    }
}
