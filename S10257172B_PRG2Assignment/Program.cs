using System;
using S10257172B_PRG2Assignment;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<int,Customer> customerDict = new Dictionary<int,Customer>(); //easy access to each customer by calling memberid(key)
        Queue<Order>regQueue = new Queue<Order>();
        Queue<Order>goldQueue = new Queue<Order>();
        CreateCustomers();
        bool yes = false;
        while(!yes)
        {
            int choice = Menu();

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Thank You for shopping at I.C.Treats!");
                    yes = true;
                    break;

                case 1:
                    Console.WriteLine("{0,-10} {1,-10} {2,-10}", "Name", "MemberID", "Date Of Birth");
                    foreach (Customer customer in customerDict.Values)
                    {
                        Console.WriteLine("{0,-10} {1,-10} {2,-10}", customer.Name, customer.Memberid, customer.Dob.ToString("dd/MM/yyyy"));
                    }
                    break;

                case 2:
                    if (goldQueue.Count == 0)
                    {
                        Console.WriteLine("Gold queue is empty.");
                    }
                    if (regQueue.Count == 0)
                    {
                        Console.WriteLine("Regular queue is empty.");
                    }
                    Console.WriteLine("Gold member's queue: ");
                    foreach (Order gq in goldQueue)
                    {
                        Console.WriteLine(gq);
                    }
                    Console.WriteLine("Regular member's queue: ");
                    foreach(Order rq in regQueue)
                    {
                        Console.WriteLine(rq);
                    }
                    break;

                case 3:
                    RegisterCustomer();
                    break;

                case 4:
                    CreateOrder();
                    break;
                default: 
                    Console.WriteLine("Invalid Option selected.");
                    break;
            }
        }
        
        Int32 Menu()
        {
            Console.WriteLine("---------------- M E N U -----------------");
            List<string> options = new List<string>()
            {
                "List all customers","List all current orders","Register a new customer",
                "Create an order","Display customer's order details","Modify order details","Exit"
            };
            for (int i = 0; i < options.Count; i++)
            {
                if (i == options.Count - 1)
                {
                    Console.WriteLine($"[0] {options[i]}");
                }
                else Console.WriteLine($"[{i + 1}] {options[i]}");
            }
            Console.WriteLine("------------------------------------------");
            Console.Write("Enter your option : ");
            Int32 c = Convert.ToInt32(Console.ReadLine());
            return c;
        }
        void CreateCustomers()
        {
            using(StreamReader sr = new StreamReader("customers.csv"))
            {
                bool x = false;
                string? s;

                while((s = sr.ReadLine()) != null)
                {
                    string[] str = s.Split(',');
                    if (!x) // for the header
                    {
                        x = true;
                        continue;
                    }
                    customerDict.Add(Convert.ToInt32(str[1]), new Customer(str[0], Convert.ToInt32(str[1]), Convert.ToDateTime(str[2])));
                }
            }
        }
        void RegisterCustomer()
        {
            Console.Write("Enter name: ");
            string? c_name = Console.ReadLine();
            Console.Write("Enter id number: ");
            int id_num = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter date of birth: ");
            DateTime dob = Convert.ToDateTime(Console.ReadLine());
            Customer c1 = new Customer(c_name, id_num, dob);
            c1.Rewards = new PointCard(0, 0);
            customerDict.Add(id_num, c1);

            using(StreamWriter sw = new StreamWriter("customers.csv"))
            {
                sw.WriteLine(c1);
                Console.WriteLine("Application successful.");
            }
        }
        void CreateOrder()
        {
            Console.WriteLine("{0,-10} {1,-10} {2,-10}", "Name", "MemberID", "Date Of Birth");
            foreach (Customer customer in customerDict.Values)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-10}", customer.Name, customer.Memberid, customer.Dob.ToString("dd/MM/yyyy"));
            }
            Console.Write("Enter Member ID of customer: ");
            int chosenId = Convert.ToInt32(Console.ReadLine());
            Order newOrder;
            if(customerDict.ContainsKey(chosenId))
            {
                Customer temp_customer = customerDict[chosenId];
                newOrder = new Order(chosenId, DateTime.Now);
                
            }
            else
            {
                Console.WriteLine("Customer's member ID does not exist.");
            }
        }
        void OrderSelection()
        {
            Console.Write("Enter your ice cream option (Cup/Cone/Waffle): ");
            string? orderOption = Console.ReadLine();
            Console.Write("Enter choice of scoop: ");
            string? orderScoop = Console.ReadLine();
            Console.Write("Enter");
        }
    }
}