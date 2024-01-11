//==========================================================
// Student Number : S10257172B
// Student Name : Loh Sze Kye
// Partner Name : 
//==========================================================

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
            int choice = Menu(); // try catch blocks NOT needed for the option menu, presence of while loop for wrong int inputs

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
            while(true)
            {
                try
                {
                    Console.Write("Enter your option : ");
                    Int32 c = Convert.ToInt32(Console.ReadLine());  // try catch blocks for non integer inputs
                    return c;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter a numerical option found in the menu.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Enter a numerical option found in the menu.");
                }
            } 
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
                    if (!x) // ignores the header
                    {
                        x = true;
                        continue;
                    }
                    customerDict.Add(Convert.ToInt32(str[1]), new Customer(str[0], Convert.ToInt32(str[1]), Convert.ToDateTime(str[2])));
                }
            }
        }
        void RegisterCustomer() //Option 3
        {
            Console.Write("Enter name: "); // try catch blocks needed here
            string? c_name = Console.ReadLine();
            Console.Write("Enter id number: "); // try catch blocks needed here
            int id_num = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter date of birth (dd/MM/yyyy): ");
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
                Order custOrder = temp_customer.MakeOrder();
                bool check = false;
                while (!check)
                {
                    List<IceCream> icl = new List<IceCream>();
                    List<Flavour> icf = new List<Flavour>();
                    List<Topping> ict = new List<Topping>();

                    Console.Write("Enter your ice cream option (Cup/Cone/Waffle): "); // try catch blocks needed here
                    var orderOption = Console.ReadLine();
                    orderOption = orderOption.ToLower().Trim(); // removes all heading and trailing whietespace + converts all letters to lowercase.
                    
                    int premiumCount = 0;
                    try
                    {

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    Console.Write("Enter type of scoop (single/double/triple): "); // try catch blocks needed here
                    string? scoopType = Console.ReadLine();
                    scoopType = scoopType.ToLower().Trim();
                    scoopType = scoopType.Replace("single", "1");
                    scoopType = scoopType.Replace("double", "2");
                    scoopType = scoopType.Replace("triple", "3");
                    int numScoop = Convert.ToInt32(scoopType);

                    for(int i = 0; i < numScoop; i++) // if single loops once, double loops twice, triple loops thrice.
                    {
                        Console.Write($"Enter flavour of scoop {i+1}: ");
                        string? flavScoop = Console.ReadLine();
                        flavScoop = flavScoop.ToLower().Trim();
                        List<string> payFlavours = new List<string>() { "durian", "ube", "sea salt" }; // flavours that need the additional $2
                        if (payFlavours.Contains(flavScoop))
                        {
                            premiumCount += 1;
                        }
                    }
                    icf.Add(new Flavour(scoopType, true, premiumCount));


                    
                    Console.WriteLine("********Toppings Available********");
                    List<string> toppings = new List<string>() { "sprinkles", "mochi", "sago", "oreos" };
                    Console.WriteLine("{0,-10} {1,-10}", "Toppings", "Added Cost");
                    foreach (string s in toppings)
                    {
                        Console.WriteLine("{0,-10} {1,-10}", s, "$1.00");
                    }
                    for(int i = 0;i < 4; i++)
                    {
                        Console.Write("Enter topping you wish to add on (enter nil to skip): ");
                        string? tempTop = Console.ReadLine();
                        tempTop = tempTop.ToLower().Trim();
                        if (tempTop == "nil")
                        {
                            break;
                        }

                        else if (toppings.Contains(tempTop))
                        {
                            ict.Add(new Topping(tempTop));
                        }
                    }

                    if (orderOption == "cup")
                    {
                        icl.Add(new Cup(orderOption, numScoop, icf, ict));
                    }
                    else if(orderOption == "cone")
                    {
                        Console.Write("For $2, would you like to upgrade to a chocolate dipped cone? [Y/N]: ");
                        string? cCone = Console.ReadLine();
                        cCone = cCone.ToLower().Trim();
                        bool dipped = false;
                        if (cCone == "y")
                        {
                            dipped = true;
                        }
                        icl.Add(new Cone(orderOption, numScoop, icf, ict,dipped));
                    }
                    else if (orderOption == "waffle")
                    {
                        Console.WriteLine("*****Premium Waffle Flavours*****");
                        List<string> wFlavours = new List<string>() { "red velvet", "charcoal", "pandan" };
                        Console.WriteLine("{0,-10} {1,-10}", "Flavours", "Added Cost");
                        foreach (string s in wFlavours)
                        {
                            Console.WriteLine("{0,-10} {1,-10}", s, "$3.00");
                        }
                        Console.Write("Enter waffle flavour you would like to upgrade to (enter nil to skip): ");
                        string? chosenFlavour = Console.ReadLine();
                        chosenFlavour = chosenFlavour.ToLower().Trim();
                        if (wFlavours.Contains(chosenFlavour))
                        {
                            icl.Add(new Waffle(orderOption, numScoop, icf, ict, chosenFlavour));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect selection.");
                    }
                    
                    while (true)
                    {
                        Console.Write("Would you like to add another ice cream to the order [Y/N]: ");
                        string? secondOrder = Console.ReadLine();
                        secondOrder = secondOrder.ToLower().Trim();
                        if (secondOrder == "n")
                        {
                            check = true;
                            break;
                        }
                        else if (secondOrder == "y")
                        {
                            
                            break;
                        }
                    }

                    

                }

                }
            else
            {
                Console.WriteLine("Customer's member ID does not exist.");
            }
        }
        
    }
}