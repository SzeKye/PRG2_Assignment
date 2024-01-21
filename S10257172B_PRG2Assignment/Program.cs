//==========================================================
// Student Number : S10257172B
// Student Name : Loh Sze Kye (option 1,3,4)
// Partner Name : Liew Yong Hong (option 2,5,6)
//==========================================================

using System;
using S10257172B_PRG2Assignment;
using static System.Formats.Asn1.AsnWriter;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<int,Customer> customerDict = new Dictionary<int,Customer>(); //easy access to each customer by calling memberid(key)
        Queue<Order>regQueue = new Queue<Order>();
        Queue<Order>goldQueue = new Queue<Order>();
        CreateCustomers(customerDict);
        Customer temp_customer;
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
                    if (goldQueue.Count > 0)
                    {
                        Console.WriteLine("Gold member's queue: ");
                        foreach (Order gq in goldQueue)
                        {
                            Console.WriteLine(gq); // Print information about the order outside the inner loop

                            foreach (IceCream ic in gq.IceCreamList)
                            {
                                Console.WriteLine(ic);
                                Console.WriteLine("-------------------------");
                            }
                            Console.WriteLine("-------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Gold queue is empty.");
                    }
                    if (regQueue.Count > 0)
                    {
                        Console.WriteLine("Regular member's queue: ");
                        foreach (Order rq in regQueue)
                        {
                            Console.WriteLine(rq);
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Regular queue is empty.");
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

                case 6:
                    ModifyOrder();
                    break;
                
                case 8:
                    totalCharged();
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
                "Create an order","Display customer's order details","Modify order details","Process order", "Display monthly charged amounts", "Exit"
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
        void CreateCustomers(Dictionary<int,Customer> customerDict)
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
                    Customer c1 = new Customer(str[0], Convert.ToInt32(str[1]), DateTime.ParseExact(str[2], "dd/MM/yyyy", null));
                    c1.Rewards = new PointCard(Convert.ToInt32(str[4]), Convert.ToInt32(str[5])); //assigns current points to each customer object
                    c1.Rewards.Tier = str[3]; //assigns membership tier to each customer object
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
            List<string> premiumFlavList = new List<string>();
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
                    flavDict.Add(str[0].ToLower(), Convert.ToInt32(str[1]));
                    if (Convert.ToInt32(str[1]) == 2)
                    {
                        premiumFlavList.Add(str[0].ToLower());
                    }
                }
            }
            
            while (!check1)
            {
                try //to catch inputs that are not int
                {
                    Console.Write("Enter Member ID of customer: ");
                    int chosenId = Convert.ToInt32(Console.ReadLine());
                    if (customerDict.ContainsKey(chosenId)) // to catch wrongly inputted ids which are int
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
                            string? orderOption = Console.ReadLine();
                            orderOption = orderOption.ToLower().Trim(); // removes all heading and trailing whietespace + converts all letters to lowercase.

                            int numScoop = 0;
                            int extraRound = 0;

                            Console.Write("Enter type of scoop (single/double/triple): ");
                            string? scoopType = Console.ReadLine();
                            scoopType = scoopType.ToLower().Trim();

                            Dictionary<string,int> sameScoop = new Dictionary<string,int>();
                            List<string> optionList = new List<string>() { "cup", "cone", "waffle" };
                            List<string> scoopCheck = new List<string>() { "single", "double", "triple" };
                            if (optionList.Contains(orderOption) && scoopCheck.Contains(scoopType))
                            {
                                numScoop += scoopCheck.FindIndex(x => x.Contains(scoopType)) + 1; //index starts from 0 hence the +1
                                Console.WriteLine("{0,-10} {1,-10}", "Flavours", "Cost");
                                foreach (KeyValuePair<string, int> f in flavDict)
                                {
                                    Console.WriteLine("{0,-10} ${1,-10}", f.Key, f.Value);
                                }

                                for (int a = 0; a < numScoop; a++) // if single loops once, double loops twice, triple loops thrice.
                                {
                                    Console.Write("Enter flavour of scoop: ");
                                    string? flavScoop = Console.ReadLine();
                                    flavScoop = flavScoop.ToLower().Trim();

                                    if (flavDict.ContainsKey(flavScoop)) //checks if its a valid flavour
                                    {
                                        if (sameScoop.ContainsKey(flavScoop)) //checks if the flavour has been ordered twice or thrice
                                        {
                                            sameScoop[flavScoop] += 1;
                                        }
                                        else
                                        {
                                            sameScoop.Add(flavScoop, 1);
                                        }  
                                    }

                                    else // if invalid flavour is entered it adds one to numScoop, so that the wasted loop loops again
                                    {
                                        Console.WriteLine("Enter a valid scoop flavour");
                                        numScoop += 1;
                                        extraRound -= 1; 
                                    }
                                }
                                foreach(KeyValuePair<string,int> ss in sameScoop)
                                {
                                    if (premiumFlavList.Contains(ss.Key)) 
                                    {
                                        icf.Add(new Flavour(ss.Key, true, ss.Value)); 
                                        continue;
                                    }
                                    icf.Add(new Flavour(ss.Key, false, ss.Value));
                                }  
                                

                                numScoop = numScoop + extraRound; //resets it back to the original scoop count by counting no. of extra rounds taken
                                Console.WriteLine("********Toppings Available********"); //toppings menu
                                List<string> toppings = new List<string>();
                                using (StreamReader sr = new StreamReader("toppings.csv"))
                                {
                                    string? s;
                                    bool x = false;
                                    while ((s = sr.ReadLine()) != null)
                                    {
                                        string[] str = s.Split(",");
                                        if (!x)
                                        {
                                            x = true;
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
                                    List<string> wFlavours = new List<string>() { "red velvet", "charcoal", "pandan", "original" };
                                    Console.WriteLine("{0,-10} {1,-10}", "Flavours", "Added Cost");
                                    foreach (string s in wFlavours)
                                    {

                                        if (s == "original")
                                        {
                                            Console.WriteLine("{0,-10} {1,-10}", s, "$0.00");
                                            break;
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
                                Console.WriteLine("Invalid ice cream option/type of scoop entered. Please check your spelling");
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
            string path = "orders.csv";
            string[] csvLines = File.ReadAllLines(path);
            string path2 = "customers.csv";
            string[] csvLines2 = File.ReadAllLines(path2);
            for (int i = 1; i < csvLines2.Length; i++)
            {
                string[] data = csvLines2[i].Split(",");
                Customer c = new Customer(data[0], Convert.ToInt32(data[1]), DateTime.ParseExact(data[2], "dd/MM/yyyy", null));

            }
            for (int i = 1; i < csvLines.Length; i++)
            {
                List<Flavour> flavoursList = new List<Flavour>();
                List<Topping> toppingList = new List<Topping>();

                string[] data = csvLines[i].Split(',');
                Order order = new Order(Convert.ToInt16(data[0]), DateTime.ParseExact(data[2], "dd/MM/yyyy HH:mm", null));
                order.TimeFulfilled = DateTime.ParseExact(data[3], "dd/MM/yyyy HH:mm", null);
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

                        }
                        flavourCount = 1;
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
                    Console.WriteLine("Order History: ");
                    
                    
                    if(customer.OrderHistory.Count != 0) 
                    {
                        Console.WriteLine("------------------------------------");
                        foreach (Order o in customer.OrderHistory)
                        {
                            Console.WriteLine(o.ToString());
                            foreach (IceCream ic in o.IceCreamList)
                            {
                                Console.WriteLine(ic.ToString());
                                Console.WriteLine();
                            }
                            Console.WriteLine("------------------------------------");
                            
                        }

                    }
                    else
                    {
                        Console.WriteLine("No order history");
                        Console.WriteLine();
                        break;
                    }

                    if(customer.CurrentOrder != null)
                    {
                        Console.WriteLine(customer.CurrentOrder.ToString());
                        foreach (IceCream ic in customer.CurrentOrder.IceCreamList)
                        {
                            Console.WriteLine(ic.ToString());
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No current order");
                        Console.WriteLine();
                        break;
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

        void ModifyOrder()
        {
            foreach (Customer cust in customerDict.Values)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-10}", cust.Name, cust.Memberid, cust.Dob.ToString("dd/MM/yyyy"));
                cust.OrderHistory.Clear();
            }
            while (true)
            {
                try
                {
                    Console.Write("Select a customer by MemberID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Customer customer = customerDict[id];
                    Console.WriteLine("------------------------------------");
                    
                    if (customer.CurrentOrder != null)
                    {

                        foreach (IceCream ic in customer.CurrentOrder.IceCreamList)
                        {
                            Console.WriteLine(ic.ToString());
                            Console.WriteLine();
                            Console.WriteLine("------------------------------------");
                        }
                    }

                    else
                    {
                        Console.WriteLine("No current order");
                        Console.WriteLine();
                        
                    }
                    while (true)
                    {
                        try
                        {

                            Console.WriteLine("[1] Modify existing ice cream");
                            Console.WriteLine("[2] Add a new ice cream");
                            Console.WriteLine("[3] Delete an ice cream");
                            Console.WriteLine("[0] Exit");
                            Console.Write("Choose an option: ");
                            int option = Convert.ToInt32(Console.ReadLine());
                            if (option == 1)
                            {
                                Console.Write("Select which ice cream you want to modify by the order(1,2,3,..): ");
                                int icOption = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(customer.CurrentOrder.IceCreamList[option - 1].ToString());
                                IceCream currentIceCream = customer.CurrentOrder.IceCreamList[option - 1];
                                while (true)
                                {
                                    Console.WriteLine("What modification you wish to make?");
                                    Console.WriteLine("[1] Modify Ice Cream Option");
                                    Console.WriteLine("[2] Modify Scoops");
                                    Console.WriteLine("[3] Modify Flavours");
                                    Console.WriteLine("[4] Modify Toppings");

                                    if (customer.CurrentOrder.IceCreamList[option - 1].Option == "Cone")
                                    {
                                        Console.WriteLine("[5] Modify Dipped Cone");
                                    }
                                    if (customer.CurrentOrder.IceCreamList[option - 1].Option == "Waffle")
                                    {
                                        Console.WriteLine("[6] Modify Waffle Flavour");
                                    }
                                    Console.WriteLine("[0] Exit");
                                    Console.Write("Choose an option: ");
                                    int modifyOption = Convert.ToInt32(Console.ReadLine());
                                    if (modifyOption == 1)
                                    {
                                        try
                                        {
                                            Console.Write("Which option do you want to pick?(Waffle,Cone,Cup): ");
                                            string modify = Console.ReadLine();

                                            switch (modify.ToLower())
                                            {
                                                case "waffle":
                                                    Console.WriteLine("Which waffle flavour do you want? (Red velvet, charcoal, pandan, original): ");
                                                    string wf = Console.ReadLine();
                                                    if (wf.ToLower() == "original")
                                                    {
                                                        wf = null;
                                                    }
                                                    else
                                                    {
                                                        wf.ToUpper();
                                                    }
                                                    customer.CurrentOrder.IceCreamList[option - 1] = new Waffle("Waffle", customer.CurrentOrder.IceCreamList[option - 1].Scoops, customer.CurrentOrder.IceCreamList[option - 1].Flavours, customer.CurrentOrder.IceCreamList[option - 1].Toppings, wf);
                                                    break;
                                                case "cone":
                                                    Console.Write("Do you want it to be chocolate dipped? (Y/N): ");
                                                    char dipped = Convert.ToChar(Console.ReadLine());
                                                    bool dippedBool = false;
                                                    if (dipped == 'Y')
                                                    {
                                                        dippedBool = true;
                                                    }
                                                    customer.CurrentOrder.IceCreamList[option - 1] = new Cone("Cone", customer.CurrentOrder.IceCreamList[option - 1].Scoops, customer.CurrentOrder.IceCreamList[option - 1].Flavours, customer.CurrentOrder.IceCreamList[option - 1].Toppings, dippedBool);
                                                    break;
                                                case "cup":
                                                    customer.CurrentOrder.IceCreamList[option - 1] = new Cup("Cup", customer.CurrentOrder.IceCreamList[option - 1].Scoops, customer.CurrentOrder.IceCreamList[option - 1].Flavours, customer.CurrentOrder.IceCreamList[option - 1].Toppings);
                                                    break;
                                                default:
                                                    Console.WriteLine("Invalid option, please try again");
                                                    break;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            Console.WriteLine("Wrong input, please check your spelling!");
                                        }
                                    }
                                    else if (modifyOption == 2)
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine("How many scoops you want? (single,double,triple): ");
                                            string scoops = Console.ReadLine();
                                            List<string> premiumList = new List<string>() { "durian", "ube", "sea salt" };
                                            int a = 0;
                                            bool premium = false;
                                            customer.CurrentOrder.IceCreamList[option - 1].Flavours.Clear();
                                            if (scoops == "single")
                                            {
                                                a = 1;
                                                customer.CurrentOrder.IceCreamList[option - 1].Scoops = 1;
                                                for (int i = 0; i < a; i++)
                                                {
                                                    Console.WriteLine("Enter flavour of scoops (Durian,Ube,Sea salt): ");
                                                    string flavour = Console.ReadLine();
                                                    if (premiumList.Contains(flavour.ToLower()))
                                                    {
                                                        premium = true;
                                                    }
                                                    customer.CurrentOrder.IceCreamList[option - 1].Flavours.Add(new Flavour(flavour, premium, 1));
                                                }

                                                break;
                                            }
                                            else if (scoops == "double")
                                            {
                                                a = 2;
                                                for (int i = 0; i < a; i++)
                                                {
                                                    Console.WriteLine("Enter flavour of scoops (Durian,Ube,Sea salt): ");
                                                    string flavour = Console.ReadLine();
                                                    if (premiumList.Contains(flavour.ToLower()))
                                                    {
                                                        premium = true;
                                                    }
                                                    customer.CurrentOrder.IceCreamList[option - 1].Flavours.Add(new Flavour(flavour, premium, 1));
                                                }
                                                break;
                                            }
                                            else if (scoops == "triple")
                                            {
                                                a = 3;
                                                for (int i = 0; i < a; i++)
                                                {
                                                    Console.WriteLine("Enter flavour of scoops (Durian,Ube,Sea salt): ");
                                                    string flavour = Console.ReadLine();
                                                    if (premiumList.Contains(flavour.ToLower()))
                                                    {
                                                        premium = true;
                                                    }
                                                    customer.CurrentOrder.IceCreamList[option - 1].Flavours.Add(new Flavour(flavour, premium, 1));
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Please enter a valid scoops!");
                                            }
                                        }

                                    }
                                    else if (modifyOption == 3)
                                    {
                                        foreach (Flavour flavour in customer.CurrentOrder.IceCreamList[option - 1].Flavours)
                                        {
                                            Console.WriteLine(flavour.ToString());
                                        }
                                        while (true)
                                        {
                                            try
                                            {
                                                bool premium = false;
                                                Console.Write("Which flavour do you want to modify? (1,2,3,.., 0 to exit)");
                                                int flavourOption = Convert.ToInt32(Console.ReadLine());
                                                if (flavourOption == 0)
                                                {
                                                    break;
                                                }
                                                Console.Write("Which flavour do you want? (Vanilla, Chocolate, Strawberry, Durian, Ube, Sea salt)");
                                                string flavours = Console.ReadLine();
                                                if (flavours == "Ube" || flavours == "Durian" || flavours == "Sea Salt")
                                                {
                                                    premium = true;
                                                }

                                                customer.CurrentOrder.IceCreamList[option - 1].Flavours[flavourOption - 1] = new Flavour(flavours, premium, 1);

                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Please input a valid option!");
                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("Invalid input, please check your spelling!");
                                            }
                                        }

                                    }
                                    else if (modifyOption == 4)
                                    {
                                        foreach (Topping topping in customer.CurrentOrder.IceCreamList[option - 1].Toppings)
                                        {
                                            Console.WriteLine(topping.ToString());
                                        }
                                        while (true)
                                        {
                                            try
                                            {
                                                Console.WriteLine("[1] Modify topping");
                                                Console.WriteLine("[2] Add topping");
                                                Console.WriteLine("[3] Delete topping");
                                                Console.WriteLine("[0] Exit");
                                                Console.Write("Enter the option you wish to make: ");
                                                int toppingModify = Convert.ToInt32(Console.ReadLine());
                                                Console.WriteLine();
                                                if (toppingModify == 1)
                                                {
                                                    while (true)
                                                    {
                                                        if (customer.CurrentOrder.IceCreamList[option - 1].Toppings.Count() != 0)
                                                        {
                                                            Console.Write("Which topping do you want to modify? (1,2,3,.., 0 to exit)");
                                                            int toppingOption = Convert.ToInt32(Console.ReadLine());
                                                            if (toppingOption > 0 && toppingOption <= customer.CurrentOrder.IceCreamList[option - 1].Toppings.Count())
                                                            {
                                                                Console.Write("Which topping do you want? (Sprinkles, Mochi, Sago, Oreos) ");
                                                                string topping = Console.ReadLine();
                                                                Console.WriteLine();
                                                                if (topping.ToLower() == "sprinkles" || topping.ToLower() == "mochi" || topping.ToLower() == "sago" || topping.ToLower() == "oreos")
                                                                {
                                                                    customer.CurrentOrder.IceCreamList[option - 1].Toppings[toppingOption - 1].Type = topping;
                                                                }
                                                                else if (toppingOption == 0)
                                                                {
                                                                    break;
                                                                }

                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Please enter a valid topping option in range!");
                                                                Console.WriteLine();
                                                            }


                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("No topping for this ice cream!");
                                                            Console.WriteLine();
                                                            break;
                                                        }

                                                    }


                                                }
                                                else if (toppingModify == 2)
                                                {
                                                    while (true)
                                                    {
                                                        try
                                                        {
                                                            Console.Write("Enter the topping you wish to add on (Sprinkles, Mochi, Sago, Oreos) (enter nil to stop): ");
                                                            string t = Console.ReadLine();

                                                            if (t.ToLower() == "sprinkles" || t.ToLower() == "mochi" || t.ToLower() == "sago" || t.ToLower() == "oreos")
                                                            {
                                                                customer.CurrentOrder.IceCreamList[option - 1].Toppings.Add(new Topping(t));
                                                                Console.WriteLine();
                                                            }
                                                            else if (t.ToLower() == "nil")
                                                            {
                                                                Console.WriteLine();
                                                                break;
                                                            }

                                                        }
                                                        catch (Exception)
                                                        {
                                                            Console.WriteLine("Enter a valid topping.");
                                                        }
                                                    }

                                                }
                                                else if (toppingModify == 3)
                                                {
                                                    while (true)
                                                    {
                                                        if (customer.CurrentOrder.IceCreamList[option - 1].Toppings.Count() != 0)
                                                        {
                                                            Console.Write("Which topping do you want to delete? (1,2,3,.., 0 to exit)");
                                                            int toppingOption = Convert.ToInt32(Console.ReadLine());
                                                            if (toppingOption > 0 && toppingOption <= customer.CurrentOrder.IceCreamList[option - 1].Toppings.Count())
                                                            {
                                                                customer.CurrentOrder.IceCreamList[option - 1].Toppings.RemoveAt(toppingOption - 1);
                                                                Console.WriteLine("Topping deleted!");
                                                                Console.WriteLine();
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Please enter a valid topping option in range!");
                                                                Console.WriteLine();
                                                            }


                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("No topping for this ice cream!");
                                                            Console.WriteLine();
                                                            break;
                                                        }
                                                    }
                                                }


                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Please input a valid option!");
                                            }
                                            catch (IndexOutOfRangeException)
                                            {
                                                Console.WriteLine("Please input a option in range!");
                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("Invalid input, please check your spelling!");
                                            }
                                        }
                                    }
                                    else if (modifyOption == 5)
                                    {
                                        while (true)
                                        {
                                            try
                                            {
                                                Console.WriteLine("Do you want it to be dipped? (Y/N)");
                                                string dipped = Console.ReadLine();
                                                bool dippedBool = false;
                                                if (dipped == "Y")
                                                {
                                                    dippedBool = true;

                                                }

                                                customer.CurrentOrder.IceCreamList[option - 1] = new Cone(customer.CurrentOrder.IceCreamList[option - 1].Option, customer.CurrentOrder.IceCreamList[option - 1].Scoops, customer.CurrentOrder.IceCreamList[option - 1].Flavours, customer.CurrentOrder.IceCreamList[option - 1].Toppings, dippedBool);
                                                break;
                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("Invalid input, please check your spelling!");
                                            }
                                        }

                                    }
                                    else if (modifyOption == 6)
                                    {
                                        while (true)
                                        {
                                            try
                                            {
                                                Console.WriteLine("What waffle flavour do you want? (Red velvet, charcoal, pandan, original)");
                                                string wf = Console.ReadLine();
                                                if (wf == "D")
                                                {
                                                    customer.CurrentOrder.IceCreamList[option - 1] = new Waffle(customer.CurrentOrder.IceCreamList[option - 1].Option, customer.CurrentOrder.IceCreamList[option - 1].Scoops, customer.CurrentOrder.IceCreamList[option - 1].Flavours, customer.CurrentOrder.IceCreamList[option - 1].Toppings, "original");
                                                }
                                                customer.CurrentOrder.IceCreamList[option - 1] = new Waffle(customer.CurrentOrder.IceCreamList[option - 1].Option, customer.CurrentOrder.IceCreamList[option - 1].Scoops, customer.CurrentOrder.IceCreamList[option - 1].Flavours, customer.CurrentOrder.IceCreamList[option - 1].Toppings, wf);

                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("Invalid input, please check your spelling!");
                                            }
                                        }
                                    }
                                    else if (modifyOption == 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            else if (option == 2)
                            {
                                while (true)
                                {
                                    try
                                    {
                                        List<string> optionList = new List<string>() { "cup", "cone", "waffle" };
                                        List<string> premiumList = new List<string>() { "durian", "ube", "sea salt" };
                                        List<string> flavourType = new List<string>() { "durian", "ube", "sea salt", "vanilla", "chocolate", "strawberry" };

                                        List<Flavour> flavourList = new List<Flavour>();
                                        List<Topping> toppingList = new List<Topping>();
                                        int scoops = 0;
                                        Console.Write("Enter your ice cream option (Cup/Cone/Waffle): ");
                                        string icOption = Console.ReadLine();
                                        int a = 0;
                                        bool premium = false;
                                        if (optionList.Contains(icOption.ToLower()) == true)
                                        {
                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.Write("Enter type of scoops (single/double/triple): ");
                                                    string icScoops = Console.ReadLine();


                                                    if (icScoops == "single" || icScoops == "double" || icScoops == "triple")
                                                    {
                                                        if (icScoops == "single")
                                                        {
                                                            scoops = 1;
                                                            a = 1;
                                                        }
                                                        else if (icScoops == "double")
                                                        {
                                                            scoops = 2;
                                                            a = 2;
                                                        }
                                                        else if (icScoops == "triple")
                                                        {
                                                            scoops = 3;
                                                            a = 3;
                                                        }
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Please enter a valid scoops!");
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }



                                            while (true)
                                            {
                                                try
                                                {
                                                    for (int i = 0; i < a; i++)
                                                    {
                                                        Console.Write("Enter flavour of scoops (Durian, Ube, Sea salt, Vanilla, Chocolate, Strawberry): ");
                                                        string flavour = Console.ReadLine();
                                                        if (flavourType.Contains(flavour.ToLower()))
                                                        {
                                                            if (premiumList.Contains(flavour.ToLower()))
                                                            {
                                                                premium = true;
                                                            }
                                                            flavourList.Add(new Flavour(flavour, premium, 1));
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Please enter a valid flavour!");
                                                        }
                                                    }
                                                    break;
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }


                                            }

                                            while (true)
                                            {
                                                try
                                                {

                                                    Console.Write("Enter the topping you wish to add on (Sprinkles, Mochi, Sago, Oreos) (enter nil to stop): ");
                                                    string t = Console.ReadLine();

                                                    if (t.ToLower() == "sprinkles" || t.ToLower() == "mochi" || t.ToLower() == "sago" || t.ToLower() == "oreos")
                                                    {
                                                        toppingList.Add(new Topping(t));

                                                    }
                                                    else if (t.ToLower() == "nil")
                                                    {
                                                        Console.WriteLine();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Please enter a valid topping!");
                                                    }

                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }

                                        }
                                        if (customer.CurrentOrder == null)
                                        {
                                            customer.CurrentOrder = new Order(customer.Memberid, DateTime.Now);
                                        }
                                        if (icOption.ToLower() == "cup")
                                        {
                                            customer.CurrentOrder.AddIceCream(new Cup(icOption, scoops, flavourList, toppingList));
                                            
                                        }
                                        if (icOption.ToLower() == "cone")
                                        {
                                            bool dipped = false;
                                            Console.WriteLine("Do you want it to be chocolate dipped? (Y/N) ");
                                            string dip = Console.ReadLine();
                                            if (dip == "Y")
                                            {
                                                dipped = true;
                                            }
                                            customer.CurrentOrder.AddIceCream(new Cone(icOption, scoops, flavourList, toppingList, dipped));
                                            
                                        }
                                        if (icOption.ToLower() == "waffle")
                                        {
                                            List<string> waffleFlavour = new List<string>() { "red velvet", "charcoal", "pandan", "original" };
                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.Write("What waffle flavour do you want? (Red velvet, Charcoal, Pandan, Original) ");
                                                    string wf = Console.ReadLine();
                                                    if (waffleFlavour.Contains(wf.ToLower()))
                                                    {
                                                        customer.CurrentOrder.AddIceCream(new Waffle(icOption, scoops, flavourList, toppingList, wf));
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Please enter a valid waffle flavour!");
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }
                                            

                                        }
                                        else
                                        {
                                            Console.WriteLine("Please input a valid option!");
                                            Console.WriteLine();
                                        }

                                        custOrder = customer.CurrentOrder;
                                        if (customer.Rewards.Tier == "Gold")
                                        {
                                            goldQueue.Enqueue(custOrder);
                                        }
                                        else
                                        {
                                            regQueue.Enqueue(custOrder);
                                        }
                                        Console.WriteLine("Order successfully made and queued");
                                        break;

                                    }

                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Please input a valid option!");
                                    }
                                }
                            }
                            else if (option == 3)
                            {
                                while (true)
                                {
                                    try
                                    {
                                        Console.Write("Which ice cream do you want to delete? (1,2,3,.., 0 to exit)");
                                        int icDelete = Convert.ToInt16(Console.ReadLine());
                                        if (icDelete > 0 && icDelete <= customer.CurrentOrder.IceCreamList.Count())
                                        {
                                            if (customer.CurrentOrder.IceCreamList.Count() == 1)
                                            {
                                                Console.WriteLine("You cannot have zero ice cream in an order!");
                                                break;
                                            }
                                            else
                                            {
                                                customer.CurrentOrder.IceCreamList.RemoveAt(icDelete - 1);
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            throw new IndexOutOfRangeException("Please input a valid option in range!");
                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Please enter a valid option in correct format!");
                                    }
                                    catch(IndexOutOfRangeException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Please enter a valid option!");
                                    }
                                }
                                                        
                            }
                            else if(option == 0)
                            {
                                break;
                            }
                            else
                            {
                                throw new Exception("Please input a valid option!");
                            }
                            break;
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    

                    if(customer.CurrentOrder != null)
                    {
                        foreach (IceCream ic in customer.CurrentOrder.IceCreamList)
                        {
                            Console.WriteLine();
                            Console.WriteLine(ic.ToString());
                            Console.WriteLine();
                            Console.WriteLine("------------------------------------");
                        }
                    }
                    break;

                }
                catch (FormatException)
                {
                    Console.WriteLine("Input MemberID is in incorrect format!");
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("Input MemberID not found!");
                    
                }

            }
           
        }

        void totalCharged()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter the year: ");
                    int year = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    int monthCount = 1;
                    double total = 0;
                    Dictionary<string, double> monthDictionary = new Dictionary<string, double> { { "Jan", 0 }, { "Feb", 0 }, { "Mar", 0 }, { "Apr", 0 }, { "May", 0 }, { "Jun", 0 }, { "Jul", 0 }, { "Aug", 0 }, { "Sep", 0 }, { "Oct", 0 }, { "Nov", 0 }, { "Dec", 0 } };

                    foreach (Customer cust in customerDict.Values)
                    {
                        foreach (Order co in cust.OrderHistory)
                        {
                            if (co.TimeFulfilled.HasValue && co.TimeFulfilled.Value.Year == year)
                            {
                                string month = co.TimeFulfilled.Value.ToString("MMM");
                                foreach (IceCream ic in co.IceCreamList)
                                {
                                    monthDictionary[month] += ic.CalculatePrice();
                                }
                            }
                        }
                        if(cust.CurrentOrder != null)
                        {
                            foreach (IceCream ic in cust.CurrentOrder.IceCreamList)
                            {
                                if (cust.CurrentOrder.TimeFulfilled.HasValue && cust.CurrentOrder.TimeFulfilled.Value.Year == year)
                                {
                                    string month = cust.CurrentOrder.TimeFulfilled.Value.ToString("MMM");
                                    monthDictionary[month] += ic.CalculatePrice();
                                }
                            }
                        }
                        
                    }

                    foreach (var kvp in monthDictionary)
                    {
                        Console.WriteLine($"{kvp.Key} {year}:   ${kvp.Value}");
                        total += kvp.Value;
                    }
                    Console.WriteLine($"\nTotal:      ${total}");
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid format, please try again!");
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a valid year");
                }
                
            }
            
        }

    }
}