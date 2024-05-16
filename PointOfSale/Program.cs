using PointOfSale;
using StaticClass;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;


List<Products> PerkItems = new List<Products>()
{
    new Products("Black Drip Coffee", "Drinks", 3.50m, 50),
    new Products("Hot Chocolate", "Drinks", 4.25m, 45),
    new Products("Tea", "Drinks", 5.00m, 30),
    new Products("Bottled Water", "Drinks", 2.00m, 100),
    new Products("Pour Over Coffee", "Drinks", 6.50m, 64),
    new Products("Iced Lavender Latte", "Drinks", 5.00m, 23),
    new Products("Souvenir Tumbler", "Accessories", 12.99m, 60),
    new Products("Cup Cozy", "Accessories", 8.00m, 65),
    new Products("Mug Warmer", "Accessories", 18.99m, 25),
    new Products("TShirt", "Accessories", 26.99m, 35),
    new Products("Ground Coffee Bean", "Accessories", 12.99m, 20),
    new Products("Whole Coffee Bean", "Accessories", 10.99m, 20)

};

Dictionary<string, int> OrderedItems = new Dictionary<string, int>();


decimal lineTotal = 0;
decimal salesTax = 1.06m;


//display 
do
{


    Console.WriteLine("Item Description                       Category                  Price         Current Stock\n");
    DisplayMenu(PerkItems);

    Console.WriteLine("What item would you like?");
    int choice = -1;
    while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice >= PerkItems.Count+1)
    {
        Console.WriteLine("Invalid Input, please try again");
    }

    //user selects quantity of item
    Console.WriteLine($"How many {PerkItems[choice - 1].Name}s would you like?");

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



    Console.WriteLine($" You ordered {PerkItems[choice - 1].Name} \n  Quantity: {quantity} @ {PerkItems[choice - 1].Price, 0:C}  {Products.LineTotal(quantity, PerkItems[choice - 1].Price),0:C}\n");
    
    Console.WriteLine($"Subtotal: {RunningSubtotal(lineTotal),0:C}");



} while (Validator.GetContinue("Would like to order something else?"));

//end of loop- Items will get totalled and user selects payment type

//shows list of items that customer ordered
ShowReceipt(OrderedItems);



Console.WriteLine($"Subtotal: {RunningSubtotal(lineTotal),0:C}");

Console.WriteLine($"Sales tax: {(salesTax - 1) * lineTotal,0:C}");

Console.WriteLine($"Grand Total: {GrandTotal(lineTotal, salesTax),0:C}");

decimal GRANDTOTAL = GrandTotal(lineTotal, salesTax);





Console.WriteLine("How would like to purchase your items? \n" +
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

        Console.WriteLine($"{count}. { p.ToString()}");
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
    //need if statement here to check if item is already in the dictionary
        //if yes- add quantity to value
        //if no- proceed with adding item
    OrderedItems.Add(choice, qty);
    return OrderedItems;
}

static void ShowReceipt(Dictionary<string, int> OrderedItems)
{

    Console.WriteLine("----Receipt----");
    foreach (KeyValuePair<string, int> pair in OrderedItems)
    {
            Console.WriteLine($"{pair.Key}\t{pair.Value,-10}");
    }
}


























