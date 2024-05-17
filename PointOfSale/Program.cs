using PointOfSale;
using StaticClass;
using System.IO;
using System.Text.RegularExpressions;

string filepath = "../../../menu.txt";
//if file doesn't exists
if (File.Exists(filepath) == false)
{
    StreamWriter tempWriter = new StreamWriter(filepath);
    foreach(Products s in MenuClass.GetInitalList())
    {
        tempWriter.WriteLine($"{s.Name}|{s.Description}|{s.Category}|{s.Price}|{s.Stock}");
    }
    tempWriter.Close();
}
MenuClass menu = new MenuClass();
StreamReader reader = new StreamReader(filepath);
while (true)
{
    string line = reader.ReadLine();
    if(line == null)
    {
        break;
    } else
    {
        string[] parts = line.Split("|");
        Products m = new Products(parts[0], parts[1], parts[2], decimal.Parse(parts[3]), int.Parse(parts[4]));
        menu.PerkItems.Add(m);
    }
}
reader.Close();

Dictionary<string, int> OrderedItems = new Dictionary<string, int>();



decimal lineTotal = 0;
decimal salesTax = 1.06m;

int choice = -1;
int quantity = 0;
//display 
do
{

    //header
    
    menu.DisplayMenu();

    Console.WriteLine("Which item would you like?");
    
    while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice >= menu.PerkItems.Count+1)
    {
        Console.WriteLine("Invalid Input, please try again");
    }

    //user selects quantity of item
    Console.WriteLine($"\n How many {menu.PerkItems[choice - 1].Name}s would you like?");

    //stock to match properties
    
    while (!int.TryParse(Console.ReadLine(), out quantity) || quantity >= menu.PerkItems[choice - 1].Stock || quantity < 0 || quantity == 0)
    {
        Console.WriteLine("Invalid Input, please try again");
    }

    AddToReceipt(OrderedItems, menu.PerkItems[choice - 1].Name, quantity);

    //reduces the stock 
    menu.PerkItems[choice - 1].Stock = Products.ReduceStock(menu.PerkItems[choice - 1].Stock, quantity);
    //checking to see if worked
    

    lineTotal += Products.LineTotal(quantity, menu.PerkItems[choice - 1].Price);



    Console.WriteLine($"\n You ordered {menu.PerkItems[choice - 1].Name} \n  Quantity: {quantity} @ {menu.PerkItems[choice - 1].Price, 0:C}  {Products.LineTotal(quantity, menu.PerkItems[choice - 1].Price),0:C}\n");
    
    Console.WriteLine($"Subtotal: {RunningSubtotal(lineTotal),0:C}");

   

} while (Validator.GetContinue("\n Would like to order something else?"));

//end of loop- Items will get totalled and user selects payment type

//shows list of items that customer ordered
ShowReceipt(OrderedItems);



Console.WriteLine($"\tSubtotal: \t{RunningSubtotal(lineTotal),-3:C}");

Console.WriteLine($"\tSales tax: \t{(salesTax - 1) * lineTotal,-3:C}");

Console.WriteLine($"\tGrand Total: \t{GrandTotal(lineTotal, salesTax),-3:C}");

decimal GRANDTOTAL = GrandTotal(lineTotal, salesTax);





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
    Console.WriteLine(CashPayment(GRANDTOTAL));
}
else if (payType == 2)
{
    Console.WriteLine (CreditPayment(GRANDTOTAL));
}

else
{
    Console.WriteLine(CheckPayment(GRANDTOTAL));
}


Console.WriteLine("\nThank you for shopping with us at Central Perk!");



StreamWriter writer = new StreamWriter(filepath);
foreach (Products s in menu.PerkItems)
{
    writer.WriteLine($"{s.Name}|{s.Description}|{s.Category}|{s.Price}|{s.Stock}");
}
writer.Close();






//Methods



static decimal RunningSubtotal (decimal Running )
{

 decimal subtotal = 0;

 subtotal = Running + subtotal;

  return subtotal;


}

static decimal GrandTotal(decimal subtotal, decimal salestax)
{

    return salestax * subtotal;

}



static string CashPayment(decimal grandTotal)

{
    Console.WriteLine("Please enter amount of cash.");

    decimal CASH = Validator.GetPositiveInputDecimal();
    decimal CHANGE = CASH - Math.Round(grandTotal, 2);
    return $"You have paid your total of {Math.Round(grandTotal, 2)} with cash. Your change is {CHANGE}";

}

static string CreditPayment(decimal grandTotal)
{
  
    // Expiration Date regex pattern
    string expirationDateRegex = @"^(0[1-9]|1[0-2])\/([0-9]{2})$";

    Console.Write("Please enter your Credit Card number: ");
    long creditcardnum = ValidatorPOS.GetCreditCard();
    string num = creditcardnum.ToString();

    Console.Write("Please enter the expiration date: MM/YY");
    decimal creditcardexp = Validator.GetPositiveInputDecimal();
    
    Console.Write("Please enter the cvv number: ");
    int creditcardcvv = ValidatorPOS.GetCVV();

    return $"You have paid your total of {Math.Round(grandTotal,2)} with credit card ending in XXXX XXXX XXXX {num.Substring(num.Length - 4)}";
}

static string CheckPayment(decimal grandTotal)
{
    Console.Write("Please enter your Check number: ");
    double checknum = Validator.GetPositiveInputDouble();
    return $"You have paid your total of {Math.Round(grandTotal, 2)} with check #{checknum}.";
}


static Dictionary<string, int> AddToReceipt(Dictionary<string,int>OrderedItems, string choice, int qty)
{

    if (OrderedItems.ContainsKey(choice))
    {
        OrderedItems[choice] += qty;
    }
    else
    {
        OrderedItems.Add(choice, qty);
    }

    return OrderedItems;
}

static void ShowReceipt(Dictionary<string, int> OrderedItems)
{

    Console.WriteLine("\n -----------Receipt-------------\n");
    foreach (KeyValuePair<string, int> pair in OrderedItems)
    {
            Console.WriteLine($"\t{pair.Key,3}\t{pair.Value,-4}\n");
    }
}


























