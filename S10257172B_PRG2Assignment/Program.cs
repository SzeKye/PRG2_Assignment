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
        CreateCustomers(customerDict);
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
        void CreateCustomers(Dictionary<int, Customer> customerDict)
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
                    Customer c1 = new(str[0], Convert.ToInt32(str[1]), Convert.ToDateTime(str[2]));
                    c1.Rewards = new(Convert.ToInt32(str[4]),Convert.ToInt32(str[5]));
                    c1.Rewards.Tier = str[3];
                    customerDict.Add(Convert.ToInt32(str[1]), c1);

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
            Dictionary<string, int> flavDict = new Dictionary<string, int>();
            bool check1 = false;
            using (StreamReader sr = new StreamReader("flavours.csv"))
            {
                bool x = false;
                string? s;

                while ((s = sr.ReadLine()) != null)
                {
                    string[] str = s.Split(',');
                    if (!x) // ignores the header
                    {
                        x = true;
                        
                        continue;
                    }
                    flavDict.Add(str[0].ToLower(),Convert.ToInt32(str[1]));
                }
            }
            Customer temp_customer;
            Order custOrder;
            while (!check1)
            {
                try //to catch inputs that are not int
                {
                    Console.Write("Enter Member ID of customer: ");
                    int chosenId = Convert.ToInt32(Console.ReadLine());
                    if(customerDict.ContainsKey(chosenId)) // to catch wrongly inputted ids which are int
                    {
                        check1 = true;
                        temp_customer = customerDict[chosenId];
                        custOrder = temp_customer.MakeOrder();
                        bool check = false;
                        while (!check)
                        {
                            List<Flavour> icf = new List<Flavour>();
                            List<Topping> ict = new List<Topping>();
                            

                            Console.Write("Enter your ice cream option (Cup/Cone/Waffle): "); // try catch blocks needed here
                            var orderOption = Console.ReadLine();
                            orderOption = orderOption.ToLower().Trim(); // removes all heading and trailing whietespace + converts all letters to lowercase.

                            int numScoop = 0;
                            int extraRound = 0;

                            Console.Write("Enter type of scoop (single/double/triple): "); 
                            string? scoopType = Console.ReadLine();
                            scoopType = scoopType.ToLower().Trim();

                            List<string>optionList = new List<string>() { "cup","cone","waffle"};
                            List<string> scoopCheck = new List<string>() { "single", "double", "triple" };
                            if (optionList.Contains(orderOption) && scoopCheck.Contains(scoopType))
                            {
                                numScoop += scoopCheck.FindIndex(x => x.Contains(scoopType)) + 1;
                                Console.WriteLine("{0,-10} {1,-10}","Flavours", "Cost");
                                foreach (KeyValuePair<string,int> f in flavDict)
                                {
                                    Console.WriteLine("{0,-10} ${1,-10}",f.Key,f.Value);
                                }

                                for (int a = 0; a < numScoop; a++) // if single loops once, double loops twice, triple loops thrice.
                                {
                                    Console.Write($"Enter flavour of scoop: "); 
                                    string? flavScoop = Console.ReadLine();
                                    flavScoop = flavScoop.ToLower().Trim();
                                    
                                    if (flavDict.ContainsKey(flavScoop))
                                    {
                                        int flavCost = flavDict[flavScoop];
                                        icf.Add(new Flavour(scoopType, true, flavCost)); 
                                    }

                                    else // if invalid flavour is entered it adds one to numScoop, so that the wasted loop loops again
                                    {
                                        Console.WriteLine("Enter a valid scoop flavour");
                                        numScoop += 1;
                                        extraRound -= 1; //resets it back to the original scoop count by counting no. of extra rounds taken
                                    }
                                }
                                numScoop = numScoop + extraRound;
                                Console.WriteLine("********Toppings Available********"); //toppings menu
                                List<string> toppings = new List<string>();
                                using (StreamReader sr = new StreamReader("toppings.csv")) 
                                {
                                    string? s;
                                    bool x= false;
                                    while ((s = sr.ReadLine()) != null)
                                    {
                                        string[] str = s.Split(",");
                                        if (!x)
                                        {
                                            x= true;
                                            Console.WriteLine("{0,-10} {1,-10}", str[0], str[1]);
                                            continue;
                                        }
                                        toppings.Add(str[0].ToLower());
                                        Console.WriteLine("{0,-10} {1,-10}", str[0], str[1]);

                                    }

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
                                    Console.WriteLine("*****Waffle Flavours*****"); //premium waffle menu
                                    List<string> wFlavours = new List<string>() { "red velvet", "charcoal", "pandan","original" };
                                    Console.WriteLine("{0,-10} {1,-10}", "Flavours", "Added Cost");
                                    foreach (string s in wFlavours)
                                    {
                                        
                                        if (s == "regular")
                                        {
                                            Console.WriteLine("{0,-10} {1,-10}", s, "$0.00");
                                        }
                                        Console.WriteLine("{0,-10} {1,-10}", s, "$3.00");
                                    }
                                    bool waffleCheck = false;
                                    while (!waffleCheck)
                                    {
                                        Console.Write("Enter waffle flavour: ");
                                        string? chosenFlavour = Console.ReadLine();
                                        chosenFlavour = chosenFlavour.ToLower().Trim();
                                        if (wFlavours.Contains(chosenFlavour)) //checks whether waffle flavour is one of the upgraded ones
                                        {
                                            custOrder.AddIceCream(new Waffle(orderOption, numScoop, icf, ict, chosenFlavour)); //any waffle flavours passed in would be either the upgraded one or nil
                                            waffleCheck = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Enter a valid waffle flavour.");
                                        }
                                    }
                                    
                                }
                                temp_customer.CurrentOrder = custOrder;
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
                        if (temp_customer.Rewards.Tier == "Gold")
                        {
                            goldQueue.Enqueue(custOrder);
                        }
                        else
                        {
                            regQueue.Enqueue(custOrder);
                        }
                        Console.WriteLine("Order successfully made and queued");
                    }
                    else
                    {
                        Console.WriteLine("Customer's member ID does not exist.");
                    }
                    
                }
                catch (FormatException)
                {

                    Console.WriteLine("Invalid customer Member ID entered."); 
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                
            }
            
            
        }

    }
}