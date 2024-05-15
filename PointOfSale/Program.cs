using PointOfSale;
using StaticClass;
using System.Runtime.CompilerServices;

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

//display 

Console.WriteLine("Item Description                       Category                  Price         Current Stock\n");
DisplayMenu(PerkItems);

Console.WriteLine("What item would you like?");
int choice = -1;
while(!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice >= PerkItems.Count)
{
    Console.WriteLine("Invalid Input, please try again");
}
Console.WriteLine($"How many {PerkItems[choice - 1].Name}s would you like?");

//stock to match properties
int stock = 0;
while(!int.TryParse(Console.ReadLine(),out stock) || stock >= PerkItems[choice -1].Stock || stock < 0 || stock == 0)
{
    Console.WriteLine("Invalid Input, please try again");
}

//reduces the stock 
PerkItems[choice - 1].Stock = Products.ReduceStock(PerkItems[choice - 1].Stock, stock);
//checking to see if worked
DisplayMenu(PerkItems);


//Console.WriteLine(PerkItems[choice - 1].Stock);







static void DisplayMenu(List<Products> items)
{
    int count = 1;
    foreach (Products p in items)
    {

        Console.WriteLine($"{count}. { p.ToString()}");
        count++;
    }
}