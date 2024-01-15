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
        Customer orderCust;
        Order custOrder;
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
                    Console.WriteLine("{0,-10} {1,-10} {2,-10}", "Name", "MemberID", "Date Of Birth");
                    foreach (Customer customer in customerDict.Values)
                    {
                        Console.WriteLine("{0,-10} {1,-10} {2,-10}", customer.Name, customer.Memberid, customer.Dob.ToString("dd/MM/yyyy"));
                    }
                    CreateOrder();
                    break;

                case 5:
                    orderDetail();
                    break;

                default: 
                    Console.WriteLine("Invalid Option selected.");
                    break;
            }
        }
        
        Int32 Menu() //done 
        {
            Console.WriteLine("---------------- M E N U -----------------");
            List<string> options = new List<string>()
            {
                "List all customers","List all current orders","Register a new customer",
                "Create an order","Display customer's order details","Modify order details","Exit"
            };
            for (int i = 0; i < options.Count; i++) //enumerates through the list of options
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
            bool check1 = false;
            while (!check1)
            {
                try //to catch inputs that are not int
                {
                    Console.Write("Enter Member ID of customer: ");
                    int chosenId = Convert.ToInt32(Console.ReadLine());
                    if(customerDict.ContainsKey(chosenId)) // to catch wrongly inputted ids which are int
                    {
                        check1 = true;
                        orderCust = customerDict[chosenId];
                        custOrder = orderCust.MakeOrder();
                        customerDict[chosenId].OrderHistory.Add(custOrder);
                        bool check = false;
                        while (!check)
                        {
                           
                            List<Flavour> icf = new List<Flavour>();
                            List<Topping> ict = new List<Topping>();
                            List<string> optionList = new List<string>() { "cup", "cone", "waffle" };
                            List<string> scoopCheck = new List<string>() { "single", "double", "triple" };

                            Console.Write("Enter your ice cream option (Cup/Cone/Waffle): "); // try catch blocks needed here
                            var orderOption = Console.ReadLine();
                            orderOption = orderOption.ToLower().Trim(); // removes all heading and trailing whietespace + converts all letters to lowercase.

                            int premiumCount = 0;
                            int numScoop = 0;
                            Console.Write("Enter type of scoop (single/double/triple): "); 
                            string? scoopType = Console.ReadLine();
                            scoopType = scoopType.ToLower().Trim();
                            if (optionList.Contains(orderOption) && scoopCheck.Contains(scoopType))
                            {
                                numScoop += scoopCheck.FindIndex(x => x.Contains(scoopType)) + 1;
                                for (int a = 0; a < numScoop; a++) // if single loops once, double loops twice, triple loops thrice.
                                {
                                    Console.Write($"Enter flavour of scoop: "); 
                                    string? flavScoop = Console.ReadLine();
                                    flavScoop = flavScoop.ToLower().Trim();
                                    List<string> payFlavours = new List<string>() { "durian", "ube", "sea salt" }; // flavours that need the additional 2
                                    List<string> freeFlavours = new List<string>() { "vanilla", "chocolate", "strawberry" }; // flavours that are free
                                    if (payFlavours.Contains(flavScoop))
                                    {
                                        premiumCount += 1;
                                    }
                                    else if (freeFlavours.Contains(flavScoop))
                                    {
                                        premiumCount += 0;
                                    }
                                    else // if invalid flavour is entered it adds one to numScoop, so that the wasted loop loops again
                                    {
                                        Console.WriteLine("Enter a valid scoop flavour");
                                        numScoop += 1;
                                    }
                                }
                                icf.Add(new Flavour(scoopType, true, premiumCount)); //calculate price will determine the total cost of flavours based on premiumCount

                                Console.WriteLine("********Toppings Available********"); //toppings menu
                                List<string> toppings = new List<string>() { "sprinkles", "mochi", "sago", "oreos" };
                                Console.WriteLine("{0,-10} {1,-10}", "Toppings", "Added Cost");
                                foreach (string s in toppings)
                                {
                                    Console.WriteLine("{0,-10} {1,-10}", s, "$1.00");
                                }
                                int i = 0;
                                while (i < 4) // class diagram shows that 1 ice cream can have at most 4 toppings
                                {
                                    Console.Write("Enter topping you wish to add on (enter nil to skip): ");
                                    string? tempTop = Console.ReadLine();
                                    tempTop = tempTop.ToLower().Trim();
                                    if (tempTop == "nil")
                                    {
                                        break;
                                    }

                                    else if (toppings.Contains(tempTop)) //only adds if valid topping is input
                                    {
                                        i++; 
                                        ict.Add(new Topping(tempTop));
                                    }
                                    else //otherwise it keeps looping
                                    {
                                        Console.WriteLine("Enter a valid topping.");
                                    }
                                    
                                }

                                if (orderOption == "cup")
                                {
                                    custOrder.AddIceCream(new Cup(orderOption, numScoop, icf, ict));
                                }
                                else if (orderOption == "cone")
                                {
                                    Console.Write("For $2, would you like to upgrade to a chocolate dipped cone? [Y/N]: ");
                                    string? cCone = Console.ReadLine();
                                    cCone = cCone.ToLower().Trim();
                                    bool dipped = false;
                                    if (cCone == "y")
                                    {
                                        dipped = true;
                                    }
                                    custOrder.AddIceCream(new Cone(orderOption, numScoop, icf, ict, dipped));
                                }
                                else if (orderOption == "waffle")
                                {
                                    Console.WriteLine("*****Premium Waffle Flavours*****"); //premium waffle menu
                                    List<string> wFlavours = new List<string>() { "red velvet", "charcoal", "pandan" };
                                    Console.WriteLine("{0,-10} {1,-10}", "Flavours", "Added Cost");
                                    foreach (string s in wFlavours)
                                    {
                                        Console.WriteLine("{0,-10} {1,-10}", s, "$3.00");
                                    }
                                    bool waffleCheck = false;
                                    while (!waffleCheck)
                                    {
                                        Console.Write("Enter waffle flavour you would like to upgrade to (enter nil to skip): ");
                                        string? chosenFlavour = Console.ReadLine();
                                        chosenFlavour = chosenFlavour.ToLower().Trim();
                                        if (wFlavours.Contains(chosenFlavour)) //checks whether waffle flavour is one of the upgraded ones
                                        {
                                            custOrder.AddIceCream(new Waffle(orderOption, numScoop, icf, ict, chosenFlavour)); //any waffle flavours passed in would be either the upgraded one or nil
                                            waffleCheck = true;
                                        }
                                        else if (chosenFlavour == "nil")
                                        {
                                            custOrder.AddIceCream(new Waffle(orderOption, numScoop, icf, ict, chosenFlavour));
                                            waffleCheck = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Enter a valid waffle flavour or nil.");
                                        }
                                    }
                                    
                                }

                                while (true) //done
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
                            else
                            {
                                Console.WriteLine("Invalid ice cream option selected.");
                            }
                        }
                        
                        Console.WriteLine(custOrder.CalculateTotal());
                        orderCust.OrderHistory.Add(custOrder);
                        
                         
                         



                    }
                    else
                    {
                        Console.WriteLine("Customer's member ID does not exist.");
                    }

                    
                }
                catch (Exception)
                {

                    Console.WriteLine("Invalid customer Member ID entered."); ;
                }
                
                
                
            }
            
            
            
        }

        void orderDetail()
        {
            
            foreach (Customer cust in customerDict.Values)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-10}", cust.Name, cust.Memberid, cust.Dob.ToString("dd/MM/yyyy"));
                cust.OrderHistory.Clear();
            }
            
            int ordercustID;
            string path = @"C:\NP\Year 1 Semester 2\Programming2\New folder (2)\S10257172B_PRG2Assignment\orders.csv";
            string[] csvLines = File.ReadAllLines(path);
            string path2 = @"C:\NP\Year 1 Semester 2\Programming2\New folder (2)\S10257172B_PRG2Assignment\customers.csv";
            string[] csvLines2 = File.ReadAllLines(path2);
            for (int i = 1; i < csvLines2.Length; i++)
            {
                string[] data = csvLines2[i].Split(",");
                Customer c = new Customer(data[0], Convert.ToInt32(data[1]), Convert.ToDateTime(data[2]));

            }
            for (int i = 1; i < csvLines.Length; i++)
            {
                List<Flavour> flavoursList = new List<Flavour>();
                List<Topping> toppingList = new List<Topping>();

                string[] data = csvLines[i].Split(',');
                Order order = new Order(Convert.ToInt16(data[0]), Convert.ToDateTime(data[2]));
                order.TimeFulfilled = Convert.ToDateTime(data[3]);
                ordercustID = Convert.ToInt32(data[1]);
                string[] flavourData = new string[] { data[8], data[9], data[10] };
                string[] toppingData = new string[] { data[11], data[12], data[13], data[14] };
                IceCream iceCream = null;
                bool dipped = false;
                bool premium = false;
                int flavourCount = 0;
                foreach (string flavour in flavourData)
                {
                    
                    
                    if (!string.IsNullOrEmpty(flavour))
                    {
                        if (flavour == "Ube" || flavour == "Durian" || flavour == "Sea Salt")
                        {
                            premium = true;
                            flavourCount = 1;

                        }
                        flavoursList.Add(new Flavour(flavour, premium, flavourCount));
                    }
                    
                }
                


                foreach (string topping in toppingData)
                {
                    if (!string.IsNullOrEmpty(topping))
                    {   
                        toppingList.Add(new Topping(topping));
                    }
                }
                
                if (data[4] == "Cup")
                {
                    iceCream = new Cup(data[4], Convert.ToInt32(data[5]), flavoursList, toppingList);
                }
                else if (data[4] == "Cone")
                {
                    if (data[6] == "TRUE")
                    {
                        dipped = true;
                    }
                    iceCream = new Cone(data[4], Convert.ToInt32(data[5]), flavoursList, toppingList, dipped);
                }
                else if (data[4] == "Waffle")
                {
                   
                    iceCream = new Waffle(data[4], Convert.ToInt32(data[5]), flavoursList, toppingList, data[7]);
                }

                iceCream.Flavours = new List<Flavour>(flavoursList);
                iceCream.Toppings = new List<Topping>(toppingList);
                order.AddIceCream(iceCream);
                if (customerDict.ContainsKey(ordercustID))
                {
                    customerDict[ordercustID].OrderHistory.Add(order);
                    
                }

            }
            while (true)
            {
                try
                {
                    Console.Write("Select a customer by MemberID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Customer customer = customerDict[id];
                    Console.WriteLine();
                    //Console.WriteLine("Order History: ");
                    foreach(Order o in customer.OrderHistory)
                    {
                        Console.WriteLine(o.ToString());
                        foreach (IceCream ic in o.IceCreamList)
                        {
                            Console.WriteLine(ic.ToString());
                            Console.WriteLine();
                        }
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input MemberID is in incorrect format!");
                    break;
                }
                catch (Exception)
                { 
                    Console.WriteLine("Input MemberID not found!");
                    break;
                }

            }
        }

    }
}