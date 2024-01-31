//==========================================================
// Student Number : S10257564A
// Student Name : Liew Yong Hong
// Partner Name : Loh Sze Kye
//==========================================================


using Microsoft.VisualBasic.FileIO;
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
            Console.WriteLine(IceCreamList[Id].ToString());
            Console.WriteLine();
            List<string> flavourList = new List<string>() { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt" }; //Store all the flavour in the list 
            List<string> premiumList = new List<string>() { "durian", "ube", "sea salt" }; //Store the premium flavour in the list
            List<string> waffleFlavour = new List<string>() { "red velvet", "charcoal", "pandan", "original" }; //Store the waffle flavour in the list
            while (true)
            {
                Console.WriteLine("What modification you wish to make?");
                Console.WriteLine("[1] Modify Ice Cream Option");
                Console.WriteLine("[2] Modify Scoops");
                Console.WriteLine("[3] Modify Flavours");
                Console.WriteLine("[4] Modify Toppings");

                if (IceCreamList[Id].Option.ToLower() == "cone") //Check if the icecream option is Cone, if yes, show the [5] Modify option
                {
                    Console.WriteLine("[5] Modify Dipped Cone");
                }
                if (IceCreamList[Id].Option.ToLower() == "waffle") //Check if the icecream option is waffle, if yes, show the [6] Modify option
                {
                    Console.WriteLine("[6] Modify Waffle Flavour");
                }
                Console.WriteLine("[0] Exit");
                Console.Write("Choose an option: "); //Ask for modify option
                int modifyOption = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                if (modifyOption == 1) //Change icecream option
                {
                    try
                    {
                        Console.Write("Which option do you want to pick?(Waffle,Cone,Cup) (0 to exit): "); //Ask for the icecream option they want
                        string? modify = Console.ReadLine();

                        switch (modify.ToLower())
                        {
                            case "waffle": //If user input is waffle, execute the code below
                                while (true)
                                {
                                    Console.Write("Which waffle flavour do you want? (Red velvet, Charcoal, Pandan, Original): "); //Ask for waffle flavour 
                                    string? wf = Console.ReadLine();
                                    if (waffleFlavour.Contains(wf.ToLower())) //check if waffle flavour input is in the option
                                    {
                                        /*Replace the customer currentorder selected icecream to the new Waffle*/
                                        IceCreamList[Id] = new Waffle("Waffle", IceCreamList[Id].Scoops, IceCreamList[Id].Flavours, IceCreamList[Id].Toppings, wf);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter a valid waffle flavour!");
                                        continue;
                                    }
                                }
                                break;

                            case "cone": //If user input is cone, execute the code below
                                while (true)
                                {
                                    Console.Write("Do you want it to be chocolate dipped? (Y/N): "); //Ask if they want cone to be dipped
                                    string? dipped = Console.ReadLine();
                                    bool dippedBool = false;
                                    if (dipped.ToLower() == "y" || dipped.ToLower() == "n") //check if dipped input is in the option
                                    {
                                        if (dipped.ToLower() == "y") //check if dipped is true
                                        {
                                            dippedBool = true;
                                        }

                                        /*Replace the customer currentorder selected icecream to the new Cone*/
                                        IceCreamList[Id] = new Cone("Cone", IceCreamList[Id].Scoops, IceCreamList[Id].Flavours, IceCreamList[Id].Toppings, dippedBool);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter a valid dipped option!");
                                        continue;
                                    }
                                }
                                break;

                            case "cup": //If user input is cup, execute the code below

                                /*Replace the customer currentorder selected icecream to the new Cup*/
                                IceCreamList[Id] = new Cup("Cup", IceCreamList[Id].Scoops, IceCreamList[Id].Flavours, IceCreamList[Id].Toppings);
                                break;

                            case "0":
                                break;

                            default: //If none match the case, show the error message
                                Console.WriteLine("Invalid option, please try again");
                                break;
                        }
                    }
                    catch (Exception) //Catch if icecream option is not found
                    {
                        Console.WriteLine("Wrong input, please check your spelling!");
                    }
                }

                else if (modifyOption == 2) //Change scoops
                {
                    while (true)
                    {
                        Console.Write("How many scoops you want? (single,double,triple): "); //Ask for scoops
                        string? scoops = Console.ReadLine();
                        int a = 0;
                        bool premium = false;
                        IceCreamList[Id].Flavours.Clear(); //Clear the flavour as scoops is changed

                        /*If scoops is single, execute the code below*/
                        if (scoops == "single")
                        {
                            a = 1; //set a to be 1 as it is single
                            IceCreamList[Id].Scoops = 1; //Set icecream scoops to be 1

                            /*This for loop is to ask user to input the flavour same amount of time as the a*/
                            for (int i = 0; i < a; i++)
                            {
                                Console.Write("Enter flavour of scoops (Durian, Ube, Sea salt, Vanilla, Strawberry, Chocolate): ");
                                string? flavour = Console.ReadLine();
                                if (flavourList.Contains(flavour.ToLower()))
                                {
                                    if(premiumList.Contains(flavour.ToLower())) //Check if the flavour is premium
                                    {
                                        premium = true;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please input a valid flavour!");
                                    i--;
                                    continue;
                                }

                                IceCreamList[Id].Flavours.Add(new Flavour(flavour, premium)); //Add the flavour to the icecream
                            }

                            break;
                        }

                        /*If scoops is double, execute the code below*/
                        else if (scoops == "double")
                        {
                            a = 2; //set a to be 2 as it is double

                            /*This for loop is to ask user to input the flavour same amount of time as the a*/
                            for (int i = 0; i < a; i++)
                            {
                                Console.Write("Enter flavour of scoops (Durian, Ube, Sea salt, Vanilla, Strawberry, Chocolate): ");
                                string? flavour = Console.ReadLine();
                                if (flavourList.Contains(flavour.ToLower()))
                                {
                                    if (premiumList.Contains(flavour.ToLower())) //Check if the flavour is premium
                                    {
                                        premium = true;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please input a valid flavour!");
                                    i--;
                                    continue;
                                }

                                IceCreamList[Id].Flavours.Add(new Flavour(flavour, premium)); //Add the flavour to the icecream
                            }
                            break;
                        }

                        /*If scoops is triple, execute the code below*/
                        else if (scoops == "triple")
                        {
                            a = 3; //set a to be 3 as it is triple

                            /*This for loop is to ask user to input the flavour same amount of time as the a*/
                            for (int i = 0; i < a; i++)
                            {
                                Console.Write("Enter flavour of scoops (Durian, Ube, Sea salt, Vanilla, Strawberry, Chocolate): ");
                                string? flavour = Console.ReadLine();
                                if (flavourList.Contains(flavour.ToLower()))
                                {
                                    if (premiumList.Contains(flavour.ToLower())) //Check if the flavour is premium
                                    {
                                        premium = true;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please input a valid flavour!");
                                    i--;
                                    continue;
                                }

                                IceCreamList[Id].Flavours.Add(new Flavour(flavour, premium)); //Add the flavour to the icecream
                            }
                            break;
                        }
                        else //Show the error message if input scoops is invalid
                        {
                            Console.WriteLine("Please enter a valid scoops!");
                        }
                    }

                }
                else if (modifyOption == 3) //Modify flavour
                {
                    foreach (Flavour flavour in IceCreamList[Id].Flavours) //Show all the flavour of the icecream
                    {

                        Console.WriteLine(flavour.ToString());
                    }
                    Console.WriteLine();
                    while (true)
                    {
                        try
                        {
                            bool premium = false;
                    
                            Console.Write("Which flavour do you want to modify? (1,2,3,.., 0 to exit) "); //Ask for flavour number to modify
                            int flavourOption = Convert.ToInt32(Console.ReadLine());
                            if (flavourOption == 0) //Exit if 0
                            {
                                break;
                            }
                            if (IceCreamList[Id].Flavours.Count() < flavourOption)
                            {
                                Console.WriteLine("Please enter a flavour in range!");
                                continue;
                            }
                            Console.Write("Which flavour do you want? (Vanilla, Chocolate, Strawberry, Durian, Ube, Sea salt) "); //Ask for flavour
                            string? flavours = Console.ReadLine();
                            if (flavourList.Contains(flavours.ToLower()))
                            {
                                if (premiumList.Contains(flavours.ToLower())) //Check if the flavour is premium
                                {
                                    premium = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please input a valid flavour!");
                                continue;
                            }
                            Console.WriteLine();

                            /*Set the flavour they selected to the new flavour*/
                            IceCreamList[Id].Flavours[flavourOption - 1] = new Flavour(flavours, premium);

                        }
                        catch (FormatException) //Catch a input format error
                        {
                            Console.WriteLine("Please input a valid option!");
                        }
                        catch (Exception) //Catch if input is invalid
                        {
                            Console.WriteLine("Invalid input, please check your spelling!");
                        }
                    }

                }
                else if (modifyOption == 4) //Modify topping
                {
                    Console.WriteLine("Current Topping: ");
                    foreach (Topping topping in IceCreamList[Id].Toppings) //Show all the toppings of the icecream
                    {
                        Console.WriteLine(topping.ToString());
                    }
                    Console.WriteLine();
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
                            if (toppingModify == 1) //Modify topping
                            {
                                while (true)
                                {
                                    if (IceCreamList[Id].Toppings.Count() != 0) //Check whether toppings list is empty
                                    {
                                        Console.Write("Which topping do you want to modify? (1,2,3,.., 0 to exit) ");
                                        int toppingOption = Convert.ToInt32(Console.ReadLine());
                                        if(toppingOption == 0)
                                        {
                                            break;
                                        }
                                        if (toppingOption > 0 && toppingOption <= IceCreamList[Id].Toppings.Count()) //Check if the toppingOption is in range in icecream topping list
                                        {
                                            Console.Write("Which topping do you want? (Sprinkles, Mochi, Sago, Oreos) ");
                                            string? topping = Console.ReadLine();
                                            Console.WriteLine();
                                            if (topping.ToLower() == "sprinkles" || topping.ToLower() == "mochi" || topping.ToLower() == "sago" || topping.ToLower() == "oreos") //
                                            {
                                                IceCreamList[Id].Toppings[toppingOption - 1].Type = topping; //Set the icecream topping type
                                            }
                                            else if (toppingOption == 0) //0 to exit
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Please input a valid topping!"); //Check if the topping input is valid
                                            }

                                        }
                                        else //Check if topping option is in range
                                        {
                                            Console.WriteLine("Please enter a valid topping option in range!");
                                            Console.WriteLine();
                                        }


                                    }
                                    else //If empty is empty, execute the below code
                                    {
                                        Console.WriteLine("No topping for this ice cream!");
                                        Console.WriteLine();
                                        break;
                                    }

                                }


                            }
                            else if (toppingModify == 2) //Add topping
                            {
                                if (IceCreamList[Id].Toppings.Count() == 4)
                                {
                                    Console.WriteLine("Maximum topping is 4!");
                                    Console.WriteLine();
                                }
                                for (int i = IceCreamList[Id].Toppings.Count(); i < 4; i++)
                                {
                                    Console.Write("Enter the topping you wish to add on (Sprinkles, Mochi, Sago, Oreos) (enter nil to stop): ");
                                    string? t = Console.ReadLine();
                                    if (t.ToLower() == "sprinkles" || t.ToLower() == "mochi" || t.ToLower() == "sago" || t.ToLower() == "oreos")
                                    {
                                        IceCreamList[Id].Toppings.Add(new Topping(t));
                                        Console.WriteLine();
                                    }
                                    else if (t.ToLower() == "nil")
                                    {
                                        Console.WriteLine();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Enter a valid topping!");
                                        i--;
                                    }
                                }

                            }
                            else if (toppingModify == 3) //Delete topping
                            {
                                while (true)
                                {
                                    if (IceCreamList[Id].Toppings.Count() != 0)
                                    {
                                        Console.WriteLine("Current Topping: ");
                                        Console.WriteLine();
                                        foreach(Topping t in IceCreamList[Id].Toppings)
                                        {
                                            Console.WriteLine(t.ToString());
                                            Console.WriteLine();
                                        }
                                        
                                        Console.Write("Which topping do you want to delete? (1,2,3,.., 0 to exit) ");
                                        int toppingOption = Convert.ToInt32(Console.ReadLine());
                                        if (toppingOption > 0 && toppingOption <= IceCreamList[Id].Toppings.Count()) //Check if the toppingOption is in range in icecream topping list
                                        {
                                            IceCreamList[Id].Toppings.RemoveAt(toppingOption - 1); //Remove the topping based on the number customer input
                                            Console.WriteLine("Topping deleted!");
                                            Console.WriteLine();
                                            break;
                                        }
                                        else //Show error message if topping option is not in the range
                                        {
                                            Console.WriteLine("Please enter a valid topping option in range!");
                                            Console.WriteLine();
                                        }


                                    }
                                    else //Show error message if topping is empty
                                    {
                                        Console.WriteLine("No topping for this ice cream!");
                                        Console.WriteLine();
                                        break;
                                    }
                                }
                            }
                            else if (toppingModify == 0) //0 to exit
                            {
                                break;
                            }


                        }
                        catch (FormatException) //Catch a option format error
                        {
                            Console.WriteLine("Please enter a valid option in correct format!");
                        }
                        catch (IndexOutOfRangeException) //Catch if option input is out of range
                        {
                            Console.WriteLine("Please input a option in range!");
                        }
                        catch (Exception) //Catch if option input is not found
                        {
                            Console.WriteLine("Invalid input, please check your spelling!");
                        }
                    }
                }
                else if (modifyOption == 5) //Modify dipped for cone
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write("Do you want it to be dipped? (Y/N) ");
                            string? dipped = Console.ReadLine();
                            bool dippedBool = false;

                            if (dipped.ToLower() == "y" || dipped.ToLower() == "n") //Check if input is correct
                            {
                                if (dipped.ToLower() == "y") //Check whether dipped or not
                                {
                                    dippedBool = true;
                                }

                                /*Replace the icecream to the new Cone*/
                                IceCreamList[Id] = new Cone(IceCreamList[Id].Option, IceCreamList[Id].Scoops, IceCreamList[Id].Flavours, IceCreamList[Id].Toppings, dippedBool);
                                break;
                            }
                            else //Show error message if input is invalid
                            {
                                throw new Exception();
                            }



                        }
                        catch (Exception) //Catch if input is not found
                        {
                            Console.WriteLine("Invalid input, please check your spelling!");
                        }
                    }

                }
                else if (modifyOption == 6) //Modify waffle flavour for waffle
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write("What waffle flavour do you want? (Red velvet, charcoal, pandan, original) ");
                            string? wf = Console.ReadLine();
                            if (waffleFlavour.Contains(wf.ToLower())) //Check if waffleflavour is in the option
                            {
                                /*Replace the icecream to the new Waffle*/
                                IceCreamList[Id] = new Waffle(IceCreamList[Id].Option, IceCreamList[Id].Scoops, IceCreamList[Id].Flavours, IceCreamList[Id].Toppings, "original");
                                break;
                            }

                            else //Show error message if waffle flavour input is invalid
                            {
                                Console.WriteLine("Please enter a valid waffle flavour!");
                            }


                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Invalid input, please check your spelling!");
                        }
                    }
                }
                else if (modifyOption == 0) //0 to exit
                {
                    Console.WriteLine(IceCreamList[Id].ToString());
                    break;
                }
                Console.WriteLine("------------------------------------------");
                foreach (IceCream ic in IceCreamList)
                {
                    Console.WriteLine(ic.ToString());
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine();
                }
            }

        }
        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }
        public void DeleteIceCream(int id)
        {
            IceCreamList.RemoveAt(id);
        }
        public double CalculateTotal()
        {
            double total_cost = 0;
            foreach (IceCream cream in IceCreamList)
            {
                double c_price = cream.CalculatePrice();
                total_cost += c_price;
            }
            return total_cost;
        }
        public override string ToString()
        {
            string tr = TimeReceived.ToString("dd/MM/yyyy h:mm:ss tt");
            string? tf = null;
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
