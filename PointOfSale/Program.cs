using PointOfSale;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;



// 1.ORDERING WITH MENU


bool MainProgram = true;

while (MainProgram)
{

    bool runProgram = true;

    // textfile path 
    string filepath = "../../../menu.txt";

    // creates textfile if doesn't exist
    FileIO.NoFile(filepath);

    // creates the menu list
    MenuClass menu = new MenuClass();
    
    // populates the menu list
    FileIO.Reader(filepath, menu.PerkItems);

    // stores customer order
    Dictionary<string, int> OrderedItems = new Dictionary<string, int>();

    decimal lineTotal = 0;
    decimal salesTax = 1.06m;

    int choice = -1;
    int quantity = 0;

    // starting header
    Console.WriteLine("WELCOME TO CENTRAL PERK CAFE !!!\n");

    // menu 
    while (runProgram)
    {

        menu.DisplayMenu();
        Console.WriteLine("Which item would you like? Please enter a number.");
        choice = ValidatorPOS.UserChoice(menu.PerkItems);

        /// add item to the text file combined with regular order process
        if (choice == 99)
        {
            FileIO.AddItem(menu.PerkItems);
            FileIO.UpdateFile(filepath, menu.PerkItems);
            continue;
        }

        /// customer order quantity
        Console.WriteLine($"How many {menu.PerkItems[choice - 1].Name}s would you like?");
        quantity = ValidatorPOS.Qty(menu.PerkItems[choice - 1].Stock);

        ValidatorPOS.AddToReceipt(OrderedItems, menu.PerkItems[choice - 1].Name, quantity);

        /// reduces stock value 
        menu.PerkItems[choice - 1].Stock = Products.ReduceStock(menu.PerkItems[choice - 1].Stock, quantity);

        lineTotal += Products.LineTotal(quantity, menu.PerkItems[choice - 1].Price);

        /// order confirmation
        Console.WriteLine($"You ordered {menu.PerkItems[choice - 1].Name} \nQuantity: {quantity} @ {menu.PerkItems[choice - 1].Price,0:C}=  " +
            $"{Products.LineTotal(quantity, menu.PerkItems[choice - 1].Price),0:C}\n");
        
        /// running subtotal
        Console.WriteLine($"Subtotal: {ValidatorPOS.RunningSubtotal(lineTotal),0:C}");
        
        ///  asking for another item
        runProgram = ValidatorPOS.GetContinue("Would like to order something else?");
    }

    // end of loop - Items will get totaled and user selects payment type


// 2.GRAND TOTAL
    Console.Clear();

    // shows list of items that customer ordered
    ValidatorPOS.ShowReceipt(OrderedItems, lineTotal, salesTax);

    // grand total
    decimal grandTotal = ValidatorPOS.GrandTotal(lineTotal, salesTax);



 // 3.PAYMEMT


    Console.WriteLine("\n How would like to purchase your items? \n" +
                         "1. Cash \n" +
                         "2. Credit Card \n" +
                         "3. Check");

    int payType = 0;

    // validating payment type
    while (int.TryParse(Console.ReadLine(), out payType) == false || payType < 1 || payType > 3)
    {
        Console.WriteLine("Invalid Input, try again");
    }

  

    switch (payType)
    {
        case 1:
            Console.WriteLine(ValidatorPOS.CashPayment(grandTotal));
            break;
        case 2:
            Console.WriteLine(ValidatorPOS.CreditPayment(grandTotal));
            break;
        case 3:
            Console.WriteLine(ValidatorPOS.CheckPayment(grandTotal));
            break;
    }


    // creates a final copy of stock depletion(s) or new stock item(s)  
    FileIO.UpdateFile(filepath, menu.PerkItems);

   

// 4.OPEN OR CLOSE THE PROGRAM

    MainProgram = ValidatorPOS.GetContinue("Keep our coffee shop open?");

    Console.Clear();

}



Console.WriteLine("\nThank you for shopping with us at Central Perk!");


