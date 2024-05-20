using PointOfSale;
using StaticClass;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


bool MainProgram = true;

while (MainProgram)
{

    bool runProgram = true;

    //textfile
    string filepath = "../../../menu.txt";

    //creates menu class if it doesn't exist
    FileIO.NoFile(filepath);

    //calling our list
    MenuClass menu = new MenuClass();

    //reading file & filling c# list
    FileIO.Reader(filepath, menu.PerkItems);

    //storing ordered items
    Dictionary<string, int> OrderedItems = new Dictionary<string, int>();



    decimal lineTotal = 0;
    decimal salesTax = 1.06m;

    int choice = -1;
    int quantity = 0;


    Console.WriteLine("WELCOME TO CENTRAL PERK CAFE !!!\n");

    //display 
    while (runProgram)
    {

        menu.DisplayMenu();
        Console.WriteLine("Which item would you like? Please enter a number.");
        choice = ValidatorPOS.UserChoice(menu.PerkItems);
        //add item
        if (choice == 99)
        {
            FileIO.AddItem(menu.PerkItems);
            FileIO.UpdateFile(filepath, menu.PerkItems);
            continue;
        }
        //user selects quantity of item
        Console.WriteLine($"How many {menu.PerkItems[choice - 1].Name}s would you like?");
        quantity = ValidatorPOS.Qty(menu.PerkItems[choice - 1].Stock);

        ValidatorPOS.AddToReceipt(OrderedItems, menu.PerkItems[choice - 1].Name, quantity);

        //reduces the stock 
        menu.PerkItems[choice - 1].Stock = Products.ReduceStock(menu.PerkItems[choice - 1].Stock, quantity);

        lineTotal += Products.LineTotal(quantity, menu.PerkItems[choice - 1].Price);

        Console.WriteLine($"You ordered {menu.PerkItems[choice - 1].Name} \nQuantity: {quantity} @ {menu.PerkItems[choice - 1].Price,0:C}=  " +
            $"{Products.LineTotal(quantity, menu.PerkItems[choice - 1].Price),0:C}\n");

        Console.WriteLine($"Subtotal: {ValidatorPOS.RunningSubtotal(lineTotal),0:C}");
        //Console.Clear();
        runProgram = Validator.GetContinue("Would like to order something else?");
    }

    //end of loop- Items will get totaled and user selects payment type
    Console.Clear();

    //shows list of items that customer ordered
    ValidatorPOS.ShowReceipt(OrderedItems, lineTotal, salesTax);

    decimal GRANDTOTAL = ValidatorPOS.GrandTotal(lineTotal, salesTax);

    Console.WriteLine("\n How would like to purchase your items? \n" +
                         "1. Cash \n" +
                         "2. Credit Card \n" +
                         "3. Check");

    int payType = 0;

    while (int.TryParse(Console.ReadLine(), out payType) == false || payType < 1 || payType > 3)
    {
        Console.WriteLine("Invalid Input, try again");
    }

    //If statement for which payment type was selected

    if (payType == 1)
    {
        Console.WriteLine(ValidatorPOS.CashPayment(GRANDTOTAL));
    }
    else if (payType == 2)
    {
        Console.WriteLine(ValidatorPOS.CreditPayment(GRANDTOTAL));
    }
    else
    {
        Console.WriteLine(ValidatorPOS.CheckPayment(GRANDTOTAL));
    }

    FileIO.UpdateFile(filepath, menu.PerkItems);

  
    MainProgram = Validator.GetContinue("Keep our coffee shop open?");
    

}



Console.WriteLine("\nThank you for shopping with us at Central Perk!");


