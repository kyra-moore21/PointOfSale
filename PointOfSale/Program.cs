using PointOfSale;
using StaticClass;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;


List<Products> PerkItems = new List<Products>()
{
    new Products("Black Drip Coffee","Pure, bold, invigorating simplicity.", "Drinks", 3.50m, 50),
    new Products("Hot Chocolate","Rich Cocoa Bliss", "Drinks", 4.25m, 45),
    new Products("Tea","Mystery Medicine!", "Drinks", 5.00m, 30),
    new Products("Bottled Water", "Straight from the Chicago River!", "Drinks", 2.00m, 100),
    new Products("Pour Over Coffee","The Pretentious choice!", "Drinks", 6.50m, 64),
    new Products("Iced Lavender Latte","The Hipster Choice", "Drinks", 5.00m, 23),
    new Products("Souvenir Tumbler","Collector's Edition", "Accessories", 12.99m, 60),
    new Products("Cup Cozy","Only if you need to ramble...", "Accessories", 8.00m, 65),
    new Products("Mug Warmer","We know you're forgetful!", "Accessories", 18.99m, 25),
    new Products("TShirt","Represent your local coffee shop!","Accessories", 26.99m, 35),
    new Products("Ground Coffee Bean","Rise and Shine!", "Accessories", 12.99m, 20),
    new Products("Whole Coffee Bean","Rise and Grind!", "Accessories", 10.99m, 20)

};

Dictionary<string, int> OrderedItems = new Dictionary<string, int>();



decimal lineTotal = 0;
decimal salesTax = 1.06m;


//display 
do
{

    //header
    Console.WriteLine($"{"Item",0}{"Description",38}{"Category",34}{"Price",18}{"In Stock",14}\n");
    DisplayMenu(PerkItems);

    Console.WriteLine("Which item would you like?");
    int choice = -1;
    while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice >= PerkItems.Count+1)
    {
        Console.WriteLine("Invalid Input, please try again");
    }

    //user selects quantity of item
    Console.WriteLine($"\n How many {PerkItems[choice - 1].Name}s would you like?");

    //stock to match properties
    int quantity = 0;
    while (!int.TryParse(Console.ReadLine(), out quantity) || quantity >= PerkItems[choice - 1].Stock || quantity < 0 || quantity == 0)
    {
        Console.WriteLine("Invalid Input, please try again");
    }

    AddToReceipt(OrderedItems, PerkItems[choice - 1].Name, quantity);

    //reduces the stock 
    PerkItems[choice - 1].Stock = Products.ReduceStock(PerkItems[choice - 1].Stock, quantity);
    //checking to see if worked

    
    lineTotal += Products.LineTotal(quantity, PerkItems[choice - 1].Price);



    Console.WriteLine($"\n You ordered {PerkItems[choice - 1].Name} \n  Quantity: {quantity} @ {PerkItems[choice - 1].Price, 0:C}  {Products.LineTotal(quantity, PerkItems[choice - 1].Price),0:C}\n");
    
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










//Methods

static void DisplayMenu(List<Products> items)
{
    int count = 1;
    foreach (Products p in items)
    {

        Console.WriteLine($"{count,2}. { p.ToString()}");
        count++;
    }
}

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
    Console.Write("Please enter your Credit Card number: ");
    decimal creditcardnum = Validator.GetPositiveInputDecimal();
    Console.Write("Please enter the expiration date: ");
    decimal creditcardexp = Validator.GetPositiveInputDecimal();
    Console.Write("Please enter the cvv number: ");
    decimal creditcardcvv = Validator.GetPositiveInputDecimal();
    return $"You have paid your total of {Math.Round(grandTotal,2)} with credit.";
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
            Console.WriteLine($"\t{pair.Key,-3}\t{pair.Value,-4}\n");
    }
}


























