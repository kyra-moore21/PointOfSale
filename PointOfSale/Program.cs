using PointOfSale;
using StaticClass;
using System.IO;
using System.Text.RegularExpressions;

//textfile
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
//calling our list
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
//storing ordered items
Dictionary<string, int> OrderedItems = new Dictionary<string, int>();



decimal lineTotal = 0;
decimal salesTax = 1.06m;

int choice = -1;
int quantity = 0;
//display 
do
{ 

    menu.DisplayMenu();
    Console.WriteLine("Which item would you like?");
    choice = ValidatorPOS.UserChoice(menu.PerkItems);
    //user selects quantity of item
    Console.WriteLine($"\n How many {menu.PerkItems[choice - 1].Name}s would you like?");
    quantity = ValidatorPOS.Qty(menu.PerkItems[choice - 1].Stock);

    ValidatorPOS.AddToReceipt(OrderedItems, menu.PerkItems[choice - 1].Name, quantity);

    //reduces the stock 
    menu.PerkItems[choice - 1].Stock = Products.ReduceStock(menu.PerkItems[choice - 1].Stock, quantity);
    //checking to see if worked
    

    lineTotal += Products.LineTotal(quantity, menu.PerkItems[choice - 1].Price);



    Console.WriteLine($"You ordered {menu.PerkItems[choice - 1].Name} \nQuantity: {quantity} @ {menu.PerkItems[choice - 1].Price, 0:C}=  {Products.LineTotal(quantity, menu.PerkItems[choice - 1].Price),0:C}\n");
    
    Console.WriteLine($"Subtotal: {ValidatorPOS.RunningSubtotal(lineTotal),0:C}");

   

} while (Validator.GetContinue("Would like to order something else?"));

//end of loop- Items will get totalled and user selects payment type

//shows list of items that customer ordered
ValidatorPOS.ShowReceipt(OrderedItems);



Console.WriteLine($"\tSubtotal: \t{ValidatorPOS.RunningSubtotal(lineTotal),-3:C}");

Console.WriteLine($"\tSales tax: \t{(salesTax - 1) * lineTotal,-3:C}");

Console.WriteLine($"\tGrand Total: \t{ValidatorPOS.GrandTotal(lineTotal, salesTax),-3:C}");

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
    Console.WriteLine (ValidatorPOS.CreditPayment(GRANDTOTAL));
}

else
{
    Console.WriteLine(ValidatorPOS.CheckPayment(GRANDTOTAL));
}


Console.WriteLine("\nThank you for shopping with us at Central Perk!");



StreamWriter writer = new StreamWriter(filepath);
foreach (Products s in menu.PerkItems)
{
    writer.WriteLine($"{s.Name}|{s.Description}|{s.Category}|{s.Price}|{s.Stock}");
}
writer.Close();






//Methods in classes

