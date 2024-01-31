//==========================================================
// Student Number : S10257172B
// Student Name : Loh Sze Kye (option 1,3,4, and advanced feature part(a) which is option 7)
// Partner Name : Liew Yong Hong (option 2,5,6 and advanced feature part (b) which is option 8)
//==========================================================

using System;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using S10257172B_PRG2Assignment;
using static System.Formats.Asn1.AsnWriter;

class Program
{

    public static void Main(string[] args)
    {
        Dictionary<int, Customer> customerDict = new Dictionary<int, Customer>(); //easy access to each customer by calling memberid(key)
        Queue<Customer> regQueue = new Queue<Customer>();
        Queue<Customer> goldQueue = new Queue<Customer>();
        CreateCustomers(customerDict);
        Customer temp_customer;
        Order custOrder;
        bool yes = false;

        /*The below part is for option5, store the data in customer order history so I can show it on option5*/
        string path = "orders.csv";
        string[] csvLines = File.ReadAllLines(path);
        string path2 = "customers.csv";
        string[] csvLines2 = File.ReadAllLines(path2);
        int ordercustID;
            
        /*This for loop is to read the file,store the customer data*/
        for (int i = 1; i < csvLines2.Length; i++)
        {
            string[] data = csvLines2[i].Split(",");
            Customer c = new Customer(data[0], Convert.ToInt32(data[1]), DateTime.ParseExact(data[2], "dd/MM/yyyy", null));

        }

        /*This for loop is to read the order file, store the details in the customer order history*/
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

            /*This foreach loop is to check whether their flavour is empty or is it premium*/
            foreach (string flavour in flavourData)
            {


                if (!string.IsNullOrEmpty(flavour))
                {
                    if (flavour == "Ube" || flavour == "Durian" || flavour == "Sea Salt")
                    {
                        premium = true;

                    }
                    flavoursList.Add(new Flavour(flavour, premium));
                }

            }


            /*This foreach loop is to check whether their topping is empty*/
            foreach (string topping in toppingData)
            {
                if (!string.IsNullOrEmpty(topping))
                {
                    toppingList.Add(new Topping(topping));
                }
            }

            /*The below if statement it to check the icecream option, then add a instance based on each option*/
            if (data[4] == "Cup")
            {
                iceCream = new Cup(data[4], Convert.ToInt32(data[5]), flavoursList, toppingList);
            }
            else if (data[4] == "Cone")
            {
                /*Check if the cone is dippe*/
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

                
            iceCream.Flavours = new List<Flavour>(flavoursList); //Create a Flavour class list and append the flavoursList inside
            iceCream.Toppings = new List<Topping>(toppingList); //Create a Topping class list adn append the toppingList inside
            order.AddIceCream(iceCream); //Add the icecream to the order

            /*This if statement add the order to the customer based on their customer id*/
            if (customerDict.ContainsKey(ordercustID))
            {
                customerDict[ordercustID].OrderHistory.Add(order);

            }

        }//End of storing orderhistory data

        while (!yes)
        {

            int choice = Menu(); // try catch blocks NOT needed for the option menu, presence of while loop for wrong int inputs

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Thank You for shopping at I.C.Treats!");
                    yes = true;
                    break;

                case 1:
                    Console.WriteLine("{0,-10} {1,-10} {2,-15} {3,-15} {4,-15} {5,-15}", "Name", "MemberID", "Date Of Birth", "Tier", "Points", "PunchCard");
                    foreach (Customer customer in customerDict.Values)
                    {
                        Console.WriteLine("{0,-10} {1,-10} {2,-15} {3,-15} {4,-15} {5,-15}",
                            customer.Name, customer.Memberid, customer.Dob.ToString("dd/MM/yyyy"), customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);
                    }
                    break;

                case 2:
                    //This is for option2

                    /*This if else statement if to check whether the queue is empty or not, if not, show all the icecream in the queue*/
                    if (goldQueue.Count > 0)
                    {
                        Console.WriteLine("Gold member's queue: ");
                        foreach (Customer gq in goldQueue)
                        {
                            Console.WriteLine(gq); // Print information about the order outside the inner loop
                                
                            /*This foreach loop is to show all the detail of the icecream in the list*/
                            foreach (IceCream ic in gq.CurrentOrder.IceCreamList)
                            {
                                Console.WriteLine(ic);
                                Console.WriteLine("------------------------------------------");
                            }

                            Console.WriteLine("------------------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Gold queue is empty.");
                    }

                    /*Same as above, check whether it is empty*/
                    if (regQueue.Count > 0)
                    {
                        Console.WriteLine("Regular member's queue: ");
                        foreach (Customer rq in regQueue)
                        {
                            Console.WriteLine(rq); //Print information about order

                            /*Show all the detail of the icecream*/
                            foreach (IceCream ic in rq.CurrentOrder.IceCreamList)
                            {
                                Console.WriteLine(ic);
                                Console.WriteLine("-------------------------");
                            }
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
                    Console.WriteLine("{0,-10} {1,-10} {2,-15} {3,-15} {4,-15} {5,-15}", "Name", "MemberID", "Date Of Birth", "Tier", "Points", "PunchCard");
                    foreach (Customer customer in customerDict.Values)
                    {
                        Console.WriteLine("{0,-10} {1,-10} {2,-15} {3,-15} {4,-15} {5,-15}",
                            customer.Name, customer.Memberid, customer.Dob.ToString("dd/MM/yyyy"), customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);
                    }
                    CreateOrder();
                    break;

                case 5:
                    orderDetail();
                    break;

                case 6:
                    ModifyOrder();
                    break;
                case 7:
                    ProcessOrderAndCheckout();

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
                "Create an order","Display customer's order details","Modify order details", "Process order and checkout", "Display monthly charged amounts", "Exit"
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
            while (true)
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
            using (StreamReader sr = new StreamReader("customers.csv"))
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
            int id_num;
            DateTime dob;
            while (true)
            {
                try
                {
                    Console.Write("Enter id number: "); // try catch blocks needed here
                    id_num = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter a proper ID");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid ID entered" + e.Message);
                }
            }
            while (true)
            {
                try
                {
                    Console.Write("Enter date of birth (dd/MM/yyyy): ");
                    dob = Convert.ToDateTime(Console.ReadLine());
                    if (dob <= DateTime.Today)
                    {
                        break;
                    }
                    Console.WriteLine("Date of birth cannot be later than today. Enter your DOB again.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter a valid birthdate.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid DOB entered." + e.Message);
                }
            }


            Customer c1 = new Customer(c_name, id_num, dob);
            c1.Rewards = new PointCard(0, 0);
            c1.Rewards.Tier = "Ordinary";
            customerDict.Add(id_num, c1);
            string c2 = c_name + "," + id_num + "," + dob.ToString("dd/MM/yyyy") + "," + "Ordinary,0,0";
            using (StreamWriter sw = new StreamWriter("customers.csv", true))
            {
                sw.WriteLine(c2);
                Console.WriteLine("Application successful.");
            }
        }

        void CreateOrder()
        {
            Dictionary<string, int> flavDict = new Dictionary<string, int>();
            List<string> premiumFlavList = new List<string>();
            bool check1 = false;
            using (StreamReader sr = new StreamReader("flavours.csv")) //gets flavours information
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
                    if (Convert.ToInt32(str[1]) == 2) // in the csv file premium flavours have a cost of 2
                    {
                        premiumFlavList.Add(str[0].ToLower());
                    }
                }
            }

            while (!check1)
            {
                try //to catch inputs that are not int
                {
                    Console.Write("Enter Member ID of customer: "); //member id is unique, members can have the same name
                    int chosenId = Convert.ToInt32(Console.ReadLine());
                    if (customerDict.ContainsKey(chosenId))
                    {
                        check1 = true;
                        temp_customer = customerDict[chosenId];
                        custOrder = temp_customer.MakeOrder(); //creates a new order object
                        bool check = false;
                        while (!check) //keeps looping until user does not want to add anymore ice creams to his order
                        {
                            using (StreamReader sr = new StreamReader("options.csv"))
                            {
                                string? s;
                                bool header = true;

                                while ((s = sr.ReadLine()) != null)
                                {
                                    string[] str = s.Split(",");
                                    if (header)
                                    {
                                        Console.WriteLine("{0,-10} {1,-10} {2,-10}", str[0], str[1], str[4]);
                                        header = false;
                                    }
                                    if (str[0] == "Cup")
                                    {
                                        Console.WriteLine("{0,-10} {1,-10} {2,-10}", str[0], str[1], str[4]);
                                    }
                                    if (str[2] == "FALSE")
                                    {
                                        Console.WriteLine("{0,-10} {1,-10} {2,-10}", str[0], str[1], str[4]);
                                    }
                                    if (str[3] == "Original")
                                    {
                                        Console.WriteLine("{0,-10} {1,-10} {2,-10}", str[0], str[1], str[4]);
                                    }
                                }
                            }
                            List<Flavour> icf = new List<Flavour>();
                            List<Topping> ict = new List<Topping>();
                            int numScoop = 0;
                            int extraRound = 0;
                            string? orderOption;
                            string? scoopType;

                            while (true)
                            {
                                List<string> optionList = new List<string>() { "cup", "cone", "waffle" };
                                Console.Write("Enter your ice cream option (Cup/Cone/Waffle): "); // try catch blocks needed here
                                orderOption = Console.ReadLine();
                                orderOption = orderOption.ToLower().Trim(); // removes all heading and trailing whietespace + converts all letters to lowercase.
                                if (optionList.Contains(orderOption))
                                {
                                    break;
                                }
                                Console.WriteLine("Invalid option entered.");
                            }

                            while (true)
                            {
                                List<string> scoopCheck = new List<string>() { "single", "double", "triple" };
                                Console.Write("Enter type of scoop (single/double/triple): ");
                                scoopType = Console.ReadLine();
                                scoopType = scoopType.ToLower().Trim();
                                if (scoopCheck.Contains(scoopType))
                                {
                                    numScoop += scoopCheck.FindIndex(x => x.Contains(scoopType)) + 1; //index starts from 0 hence the +1
                                    break;
                                }
                                Console.WriteLine("Enter a valid choice of scoop.");
                            }

                            Console.WriteLine("********Flavours Available********"); //flavours menu
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
                                    if (flavDict[flavScoop] == 2)
                                    {
                                        icf.Add(new Flavour(flavScoop, true));
                                        continue;
                                    }
                                    icf.Add(new Flavour(flavScoop, false));
                                }

                                else // if invalid flavour is entered it adds one to numScoop, so that the wasted loop loops again
                                {
                                    Console.WriteLine("Enter a valid scoop flavour");
                                    numScoop += 1;
                                    extraRound -= 1;
                                }
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
                                bool dipped = false;
                                while (true)
                                {
                                    Console.Write("For $2, would you like to upgrade to a chocolate dipped cone? [Y/N]: ");
                                    string? cCone = Console.ReadLine();
                                    cCone = cCone.ToLower().Trim();
                                    if (cCone == "y")
                                    {
                                        dipped = true;
                                        break;
                                    }
                                    else if (cCone == "n")
                                    {
                                        dipped = false;
                                        break;
                                    }
                                    Console.WriteLine("Enter a valid upgrade selection.");
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

                        if (temp_customer.Rewards.Tier == "Gold")
                        {
                            goldQueue.Enqueue(temp_customer);
                        }
                        else
                        {
                            regQueue.Enqueue(temp_customer);
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
                    Console.WriteLine("Invalid customer Member ID entered." + ex.Message);
                }


            }


        }

        void orderDetail() //Option 5
        {

            /*Show all the customer*/
            foreach (Customer cust in customerDict.Values)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-10}", cust.Name, cust.Memberid, cust.Dob.ToString("dd/MM/yyyy"));
            }

            while (true)
            {
                try
                {
                    Console.Write("Select a customer by MemberID: "); //Ask for customer MemberID
                    int id = Convert.ToInt32(Console.ReadLine()); 
                    Customer customer = customerDict[id]; //Set customer to be the customer with the MemberID
                    Console.WriteLine();
                    Console.WriteLine("Order History: ");

                    /*This if else statement is to check whether the customer orderhistory is empty*/
                    if (customer.OrderHistory.Count != 0)
                    {
                        Console.WriteLine("------------------------------------------");
                        foreach (Order o in customer.OrderHistory)
                        {
                            Console.WriteLine(o.ToString()); //Show the order
                            foreach (IceCream ic in o.IceCreamList)
                            {
                                Console.WriteLine(ic.ToString()); //Show the icecream details in the icecream list
                                Console.WriteLine();
                            }
                            Console.WriteLine("------------------------------------------");

                        }

                    }
                    else
                    {
                        Console.WriteLine("No order history");
                        Console.WriteLine();
                    }
                Console.WriteLine();
                    
                Console.WriteLine("Current order: ");
                /*This if else statement is to check whether the customer currentorder is empty*/
                if (customer.CurrentOrder != null)
                    {
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine(customer.CurrentOrder.ToString()); //Show the order
                        foreach (IceCream ic in customer.CurrentOrder.IceCreamList)
                        {
                            Console.WriteLine(ic.ToString()); //Show the icecream details in the icecream list
                            Console.WriteLine("------------------------------------------");
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
                catch (FormatException) //Use to check whether the MemberID is in correct format
                {
                    Console.WriteLine("Input MemberID is in incorrect format!");
                    Console.WriteLine();
                    break;
                }
                catch (Exception) //Use to check whether the MemberID input is found in the customer dictionary
                {
                    Console.WriteLine("Input MemberID not found!");
                    Console.WriteLine();
                    break;
                }

            }
        }

        void ModifyOrder() //Option6
        {
            /*Show all the customer*/
            foreach (Customer cust in customerDict.Values)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-10}", cust.Name, cust.Memberid, cust.Dob.ToString("dd/MM/yyyy"));
                cust.OrderHistory.Clear();
            }

            while (true)
            {
                try
                {
                    Console.Write("Select a customer by MemberID: "); //Ask for customer MemberID
                    int id = Convert.ToInt32(Console.ReadLine());

                    Customer customer = customerDict[id]; //Set customer to be the customer with the MemberID
                    Console.WriteLine("------------------------------------");

                    if (customer.CurrentOrder != null) //Check whether customer currentorder is null, if not, execute the below code
                    {
                        /*This foreach loop is to output all the icecraem detail in the customer currentorder icecream list*/
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
                            Console.Write("Choose an option: "); //Ask for option
                            int option = Convert.ToInt32(Console.ReadLine());
                            if (option == 1) //Modify Icecream
                            {
                            while (true)
                            {
                                /*Check whether the current order is null*/
                                if (customer.CurrentOrder == null)
                                {
                                    Console.WriteLine("You do not have current order!");
                                    Console.WriteLine();
                                    break;
                                }
                                Console.Write("Select which ice cream you want to modify by the order(1,2,3,.. 0 to exit): "); //Ask for the icecream they want to modify
                                int icOption = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine();
                                if (icOption == 0) //0 to exit
                                {
                                    break;
                                }

                                /*This if statement is to check whether the input icOption is in range*/
                                if (customer.CurrentOrder.IceCreamList.Count() < icOption || icOption < 0)
                                {
                                    Console.WriteLine("Please enter a valid ice cream option in range!");
                                    Console.WriteLine();
                                    continue;
                                }

                                customer.CurrentOrder.ModifyIceCream(icOption-1); //Modify the icecream by calling the class method
                                //Code below is to replace the customer current order in the queue
                                foreach (Customer c in goldQueue)
                                {
                                    if (c.Memberid == customer.Memberid)
                                    {
                                        c.CurrentOrder = customer.CurrentOrder;
                                    }
                                }
                                foreach (Customer c in regQueue)
                                {
                                    if (c.Memberid == customer.Memberid)
                                    {
                                        c.CurrentOrder = customer.CurrentOrder;
                                    }
                                }

                                }
                                    
                            }
                            else if (option == 2) //Add new icecream
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
                                        Console.Write("Enter your ice cream option (Cup/Cone/Waffle) (0 to exit): ");
                                        string icOption = Console.ReadLine();
                                    if (icOption == "0")
                                    {
                                        break;
                                    }
                                    if (!(icOption.ToLower() == "cup" || icOption.ToLower() == "cone" || icOption.ToLower() == "waffle"))
                                        {
                                            Console.WriteLine("Please enter a valid option!");
                                            continue;
                                        }
                                            
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


                                                    if (icScoops == "single" || icScoops == "double" || icScoops == "triple") //Check if scoops input is correct
                                                    {
                                                        /*Set the scoops and a based on the icScoops*/
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
                                                    else //throw new exception if input is invalid
                                                    {
                                                        throw new Exception("Please enter a valid scoops!");
                                                            
                                                    }
                                                }
                                                catch (Exception ex) //Catch the error
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }

                                            while (true)
                                            {
                                                try
                                                {
                                                    for (int i = 0; i < a; i++) //Ask for the flavour based on their scoops
                                                    {
                                                        Console.Write("Enter flavour of scoops (Durian, Ube, Sea salt, Vanilla, Chocolate, Strawberry): ");
                                                        string flavour = Console.ReadLine();
                                                        if (flavourType.Contains(flavour.ToLower())) //Check if the input is in the flavour type list
                                                        {
                                                            if (premiumList.Contains(flavour.ToLower())) //Check if the flavour is empty
                                                            {
                                                                premium = true;
                                                            }
                                                                
                                                            flavourList.Add(new Flavour(flavour, premium)); //Add the flavour to the flavourList
                                                        }
                                                        else //throw new exception is flavour input is invalid
                                                        {
                                                            Console.WriteLine("Please enter a valid flavour!");
                                                            i--; // Use to iterate the current loop again if input is invalid
                                                            continue;
                                                        }
                                                    }
                                                    break;
                                                }
                                                catch (Exception) //catch the error
                                                {
                                                    Console.WriteLine("Please enter a valid flavour!");
                                                }


                                            }

                                            for (int i = 0; i < 4; i++)
                                            {
                                                Console.Write("Enter the topping you wish to add on (Sprinkles, Mochi, Sago, Oreos) (enter nil to stop): ");
                                                string? t = Console.ReadLine();
                                                if (t.ToLower() == "sprinkles" || t.ToLower() == "mochi" || t.ToLower() == "sago" || t.ToLower() == "oreos") //Check if topping input is in the option
                                                {
                                                    toppingList.Add(new Topping(t)); //Add the topping to the toppingList

                                                }
                                                else if (t.ToLower() == "nil") //nil to exit
                                                {
                                                    Console.WriteLine();
                                                    break;
                                                }
                                                else //show error message if topping input is invalid
                                                {
                                                    Console.WriteLine("Please enter a valid topping!");
                                                    i--;
                                                    continue; // Use to iterate the current loop again if input is invalid
                                                    
                                                }
                                            }

                                        }

                                        /*Check if currentorder is null, if it is, create a new currentorder for the customer to store the icecream*/
                                        if (customer.CurrentOrder == null)
                                        {
                                            customer.CurrentOrder = new Order(customer.Memberid, DateTime.Now); //Use their memberID and current time to create the order
                                        }
                                        if (icOption.ToLower() == "cup") //Check if option is cup
                                        {
                                            customer.CurrentOrder.AddIceCream(new Cup(icOption, scoops, flavourList, toppingList)); //Add the icecream to the current order

                                        }
                                        else if (icOption.ToLower() == "cone") //Check if option is cone
                                        {
                                            while (true)
                                            {
                                                bool dipped = false;
                                                Console.Write("Do you want it to be chocolate dipped? (Y/N) ");
                                                string? dip = Console.ReadLine();
                                                if (dip.ToLower() == "y" || dip.ToLower() == "n")
                                                {
                                                    if (dip.ToLower() == "y")
                                                    {
                                                        dipped = true;
                                                    }
                                                    customer.CurrentOrder.AddIceCream(new Cone(icOption, scoops, flavourList, toppingList, dipped)); //Add the icecream to the current order
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Please enter a valid dipped option!");
                                                    continue;
                                                }
                                            }
                                        }
                                        else if (icOption.ToLower() == "waffle") //Check if option is waffle
                                        {
                                            List<string> waffleFlavour = new List<string>() { "red velvet", "charcoal", "pandan", "original" }; //Store the waffle flavour in the list
                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.Write("What waffle flavour do you want? (Red velvet, Charcoal, Pandan, Original) ");
                                                    string? wf = Console.ReadLine();
                                                    if (waffleFlavour.Contains(wf.ToLower()))
                                                    {
                                                        customer.CurrentOrder.AddIceCream(new Waffle(icOption, scoops, flavourList, toppingList, wf)); //Add the icecream to the current order
                                                        break;
                                                    }
                                                    else //throw new exception is waffle flavour not found
                                                    {
                                                        throw new Exception("Please enter a valid waffle flavour!");
                                                    }
                                                }
                                                catch (Exception ex) //Catch error
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }


                                        }
                                        else //Show error message if option input is invalid
                                        {
                                            Console.WriteLine("Please input a valid option!");
                                            Console.WriteLine();
                                        }
                                        

                                        Console.WriteLine("Order successfully created");

                                        //Code below is to replace the customer current order in the queue
                                        bool custFound = false;
                                        foreach (Customer c in goldQueue)
                                        {
                                            if (c.Memberid == customer.Memberid)
                                            {
                                                c.CurrentOrder = customer.CurrentOrder;
                                                custFound = true;
                                            }
                                        }
                                        foreach (Customer c in goldQueue)
                                        {
                                            if (c.Memberid == customer.Memberid)
                                            {
                                                c.CurrentOrder = customer.CurrentOrder;
                                                custFound = true;
                                            }
                                        }
                                        if(custFound == false)
                                        {
                                            if (customer.Rewards.Tier.ToLower() == "gold")
                                            {
                                                goldQueue.Enqueue(customer);
                                            }
                                            else
                                            {
                                                regQueue.Enqueue(customer);
                                            }
                                        }


                                        break;

                                    }

                                    catch (Exception) //Catch error
                                    {
                                        Console.WriteLine("Please input a valid option!");
                                    }
                                }
                            }
                            else if (option == 3) //Delete icecream
                            {
                                while (true)
                                {
                                    try
                                    {
                                        if(customer.CurrentOrder == null) //Check if currentorder is null or not,if yes, show the below message and break;
                                        {
                                            Console.WriteLine("You do not have current order!");
                                            Console.WriteLine();
                                            break;
                                        }
                                        Console.Write("Which ice cream do you want to delete? (1,2,3,.., 0 to exit) ");
                                        int icDelete = Convert.ToInt16(Console.ReadLine());
                                        if (icDelete > 0 && icDelete <= customer.CurrentOrder.IceCreamList.Count()) //Check whether the icDelete is in range 
                                        {
                                            if (customer.CurrentOrder.IceCreamList.Count() == 1) //Show the message if they try to delete the only icecream left
                                            {
                                                Console.WriteLine("You cannot have zero ice cream in an order!");
                                                break;
                                            }
                                            else
                                            {
                                                customer.CurrentOrder.DeleteIceCream(icDelete -1); //Delete the icecream they selected
                                                //Code below is to replace the customer current order in the queue
                                                foreach(Customer c in goldQueue)
                                                {
                                                    if(c.Memberid == customer.Memberid)
                                                    {
                                                        c.CurrentOrder = customer.CurrentOrder;
                                                    }
                                                }
                                                foreach(Customer c in regQueue)
                                                {
                                                    if(c.Memberid == customer.Memberid)
                                                    {
                                                        c.CurrentOrder = customer.CurrentOrder;
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                        else if(icDelete == 0) //0 to exit
                                        {
                                            break;
                                        }
                                        else //throw new index out of range exception if option input is out of range
                                        {
                                            throw new IndexOutOfRangeException("Please input a valid option in range!");
                                        }
                                    }
                                    catch (FormatException) //Catch error if option input is in incorrect format
                                    {
                                        Console.WriteLine("Please enter a valid option in correct format!");
                                    }
                                    catch (IndexOutOfRangeException ex) //Catch the error
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (Exception) //Catch error if option is invalid
                                    {
                                        Console.WriteLine("Please enter a valid option!");
                                    }
                                }

                            }
                            else if (option == 0) //0 to exit
                            {
                                break;
                            }
                            else
                            {
                                throw new Exception("Please input a valid option!");
                            }
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter a valid option in correct format!");
                        continue;
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    /*Show the customer current order icecream list if current order is not null*/
                    if (customer.CurrentOrder != null)
                    {
                        foreach (IceCream ic in customer.CurrentOrder.IceCreamList) //Show all the icecream detail in the currentorder
                        {
                            Console.WriteLine();
                            Console.WriteLine(ic.ToString());
                            Console.WriteLine();
                            Console.WriteLine("------------------------------------");
                        }
                    }
                    break;

                }
                catch (FormatException) //Catch if member id is in incorrect format
                {
                    Console.WriteLine("Input MemberID is in incorrect format!");

                }
                catch (Exception) //Catch if member id not found
                {
                    Console.WriteLine("Input MemberID not found!");

                }

            }

        }

        void totalCharged() //Option 8
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter the year: ");
                    int year = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    double total = 0;

                    /*Create a dictionary for the totalCharged of the year*/
                    Dictionary<string, double> monthDictionary = new Dictionary<string, double> { { "Jan", 0 }, { "Feb", 0 }, { "Mar", 0 }, { "Apr", 0 }, { "May", 0 }, { "Jun", 0 }, { "Jul", 0 }, { "Aug", 0 }, { "Sep", 0 }, { "Oct", 0 }, { "Nov", 0 }, { "Dec", 0 } };

                    foreach (Customer cust in customerDict.Values) //Loop through every customer to get the order details
                    {
                        foreach (Order co in cust.OrderHistory) //This foreach is to get all the order in the order history
                        {
                            if (co.TimeFulfilled.HasValue && co.TimeFulfilled.Value.Year == year) //This if statement is to check if the timefulfilled year is same as inputted year and timefulfilled is not empty
                            {
                                string month = co.TimeFulfilled.Value.ToString("MMM"); //Change month to the Jan,Feb format
                                foreach (IceCream ic in co.IceCreamList) //Iterate every icecream in the list and calculate the price, then add to the dictionary
                                {
                                    monthDictionary[month] += ic.CalculatePrice();
                                }
                            }
                        }
                        if (cust.CurrentOrder != null) //Check if current order is null or not, it not null, execute the below code
                        {
                            foreach (IceCream ic in cust.CurrentOrder.IceCreamList) //Iterate every icecream in the current order icecream list
                            {
                                if (cust.CurrentOrder.TimeFulfilled.HasValue && cust.CurrentOrder.TimeFulfilled.Value.Year == year) //This is statement is to check if the timefulfilled year is same as inputted year and timefulfilled is not empty
                                {
                                    string month = cust.CurrentOrder.TimeFulfilled.Value.ToString("MMM"); //Change month to the Jan,Feb format
                                    monthDictionary[month] += ic.CalculatePrice(); //Calculate icecream price then add to the dictionary
                                }
                            }
                        }

                    }

                    foreach (var kvp in monthDictionary) //This for each statement is to show the output of every month charged
                    {
                        Console.WriteLine($"{kvp.Key} {year}:   ${kvp.Value}"); //This is the output, month then the totalCharge of the month
                        total += kvp.Value;
                    }
                    Console.WriteLine($"\nTotal:      ${total}"); //Show the total charged
                    break;
                }
                catch (FormatException) //Catch if year input is in the correct format
                {
                    Console.WriteLine("Invalid format, please try again!");
                }
                catch (Exception) //Catch if year input is valid
                {
                    Console.WriteLine("Please enter a valid year");
                }

            }

        }


        void ProcessOrderAndCheckout()
        {
            if (goldQueue.Count > 0 || regQueue.Count > 0)  // there must be an order in the queue for it to follow through
            {
                if (goldQueue.Count > 0) // gets first order in queue, gold queue takes priority
                {
                    temp_customer = goldQueue.Dequeue();
                }
                else
                {
                    temp_customer = regQueue.Dequeue();
                }
                custOrder = temp_customer.CurrentOrder;
                temp_customer.CurrentOrder = null; //set the order object in current order to null
                double finalBill = custOrder.CalculateTotal();
                Console.WriteLine($"\n****Bill****\nName: {temp_customer.Name}\nMember ID: {temp_customer.Memberid}");

                List<double> prices = new List<double>();
                bool PunchOnce = true;
                foreach (IceCream ic in custOrder.IceCreamList)
                {
                    Console.WriteLine(ic);
                    if (PunchOnce && temp_customer.Rewards.PunchCard == 10)
                    {
                        finalBill -= ic.CalculatePrice();
                        prices.Add(0);
                        temp_customer.Rewards.PunchCard = 0;
                        PunchOnce = false;
                        Console.WriteLine("Punch Card redeemed.");
                        continue;
                    }
                    PunchOnce = false;
                    prices.Add(ic.CalculatePrice());
                    temp_customer.Rewards.Punch(); //increment the punch card for every ice cream in the order
                }



                bool today = temp_customer.IsBirthday();
                if (today) //birthday check 
                {
                    Console.WriteLine("Happy birthday! The most expensive ice cream in your order is on the house!");
                    double mostEx = prices.Max();
                    finalBill -= mostEx;
                }
                double pointsEarned = Math.Floor(finalBill * 0.72);
                int p = (int)pointsEarned;
                temp_customer.Rewards.AddPoints(p); //points added based on price after factoring in punch card and birthday

                Console.WriteLine("Net Total: ${0:F2}\nMembership Status: {1}\nCurrent Points: {2}", finalBill, temp_customer.Rewards.Tier, temp_customer.Rewards.Points);


                bool check = false;
                if (finalBill > 0)
                {
                    if (temp_customer.Rewards.Tier == "Ordinary") //original tier members cannot redeem points 
                    {
                        Console.WriteLine($"Net Total: ${finalBill}\nPress any key to make payment.");
                        Console.ReadKey();
                        check = true;
                    }

                    while (!check)
                    {
                        Console.Write("Your current net total is ${0:F2}\nPoints available: {1}\nWould you like to redeem your points? [Y/N]: ",
                            finalBill, temp_customer.Rewards.Points);
                        string? redeemCheck = Console.ReadLine();
                        redeemCheck = redeemCheck.ToLower().Trim(); //addresses the problem of case sensitivity 
                        switch (redeemCheck)
                        {
                            case "y": //executes if members want to redeem their points.
                                int numPoints;
                                while (true)
                                {
                                    Console.Write("Enter the number of points you would like to redeem: ");
                                    try
                                    {
                                        numPoints = Convert.ToInt32(Console.ReadLine());
                                        if (numPoints <= temp_customer.Rewards.Points && numPoints > 0) //ensures that no. of redeemed is lower than available points and not a negative
                                        {
                                            double offset = numPoints * 0.02;
                                            if (finalBill-offset < 0)
                                            {
                                                Console.WriteLine("Redeeming points to make your total negative is not allowed. Try again.");
                                                continue;
                                            }
                                            else
                                            {
                                                temp_customer.Rewards.RedeemPoints(numPoints);
                                                finalBill -= offset;
                                                break;
                                            }
                                        }
                                        Console.WriteLine("You only have {0} points, redemption of {1} points is not allowed.",
                                                temp_customer.Rewards.Points, numPoints);
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Enter a whole number for the number of points that you want to redeem.");
                                    }

                                }

                                Console.WriteLine($"Net Total: ${finalBill:F2}\nPress any key to make payment.", finalBill);
                                Console.ReadKey();
                                check = true;
                                break;

                            case "n": //executes if members do not want to redeem their points
                                Console.WriteLine($"Net Total: ${finalBill:F2}\nPress any key to make payment.", finalBill);
                                Console.ReadKey();
                                check = true;
                                break;

                            default: //executes if customer enters anything other than y or n
                                Console.WriteLine("Invalid choice entered, try again.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Net Total: ${finalBill:F2}");
                }
                custOrder.TimeFulfilled = DateTime.Now;
                temp_customer.OrderHistory.Add(custOrder);

                using (StreamWriter sw = new StreamWriter("customers.csv", false)) //overwrites the csv to reflect the updated values
                {
                    sw.WriteLine("Name,MemberID,DOB,MembershipStatus,MembershipPoints,PunchCard");
                }
                foreach (Customer c in customerDict.Values)
                {
                    using (StreamWriter sw = new StreamWriter("customers.csv", true))
                    {
                        sw.WriteLine(c.ToString());
                    }
                }

                foreach (IceCream iic in custOrder.IceCreamList)
                {
                    string orderRecord = custOrder.Id + "," + temp_customer.Memberid + "," + custOrder.TimeReceived + "," + custOrder.TimeFulfilled + "," + char.ToUpper(iic.Option[0]) + iic.Option.Substring(1) + "," + iic.Scoops + ",";
                    if (iic.Option == "cup")
                    {
                        orderRecord += ",,";
                    }
                    else if (iic.Option == "cone")
                    {
                        Cone cone = (Cone)iic;
                        orderRecord += cone.Dipped + ",,";
                    }
                    else if (iic.Option == "waffle")
                    {
                        Waffle waffle = (Waffle)iic;
                        orderRecord += "," + waffle.WaffleFlavour + ",";
                    }
                    for (int i = 0; i < 3; i++)
                    { //Flavour1,Flavour2,Flavour3,Topping1,Topping2,Topping3,Topping4 <- how its written in the csv.
                        try
                        {
                            string flavourWrite = char.ToUpper(iic.Flavours[i].Type[0]) + iic.Flavours[i].Type.Substring(1);
                            orderRecord += flavourWrite + ",";
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            orderRecord += ",";
                        }
                    }
                    for (int r = 0; r < 4; r++)
                    {
                        try
                        {
                            string toppingWrite = char.ToUpper(iic.Toppings[r].Type[0]) + iic.Toppings[r].Type.Substring(1);
                            orderRecord += toppingWrite + ",";
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            orderRecord += ",";
                        }
                    }
                    using (StreamWriter sw = new StreamWriter("orders.csv", true))
                    {
                        sw.WriteLine(orderRecord);
                    }
                }
                Console.WriteLine("\nPayment made with order fulfilled and added to history.");
            }
            else
            {
                Console.WriteLine("No orders to process in either queue.");
            }

        }







    }
}
