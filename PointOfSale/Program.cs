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
    Console.WriteLine($"How many {PerkItems[choice - 1].Name}s would you like?");

    //stock to match properties
    int stock = 0;
    while (!int.TryParse(Console.ReadLine(), out stock) || stock >= PerkItems[choice - 1].Stock || stock < 0 || stock == 0)
    {
        Console.WriteLine("Invalid Input, please try again");
    }

    //reduces the stock 
    PerkItems[choice - 1].Stock = Products.ReduceStock(PerkItems[choice - 1].Stock, stock);
    //checking to see if worked

    
    lineTotal += Products.LineTotal(stock, PerkItems[choice - 1].Price);



    Console.WriteLine($" You ordered {PerkItems[choice - 1].Name} \n  Quantity: {stock} @ {PerkItems[choice - 1].Price, 0:C}  {Products.LineTotal(stock, PerkItems[choice - 1].Price),0:C}\n");
    
    Console.WriteLine($"Subtotal: {RunningSubtotal(lineTotal),0:C}");




   




} while (Validator.GetContinue("Would like to order something else?"));

Console.WriteLine($"Subtotal: {RunningSubtotal(lineTotal),0:C}");

Console.WriteLine($"Sales tax: {(salesTax - 1) * lineTotal,0:C}");

Console.WriteLine($"Grand Total: {GrandTotal(lineTotal, salesTax),0:C}");

decimal GRANDTOTAL = GrandTotal(lineTotal, salesTax);

Console.WriteLine("How would like to purchase your items? \n" +
                 "1. Cash \n" +
                 "2. Credit Card \n" +
                 "3. Check");


int payType = int.Parse(Console.ReadLine());





















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



static decimal PaymentType(decimal GRANDTOTAL, int PAYMENT)

{
    if (PAYMENT == 1)
    {
        
        Console.WriteLine("Please enter amount of cash.");
        decimal CASH = Validator.GetPositiveInputDecimal();
        

        decimal CHANGE = GRANDTOTAL - CASH;
        return CHANGE;


    }

    return 0;



}

































