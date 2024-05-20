using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PointOfSale
{
    public class MenuClass
    {
        //properties
        public List<Products> PerkItems { get; set; }

        //constructor 
        public MenuClass()

        {

            PerkItems = new List<Products>();



        }
        //methods
        public void DisplayMenu()
        {
            Console.WriteLine($"{"Item",3}{"Description",38}{"Category",34}{"Price",18}{"In Stock",14}\n");
            int count = 1;
            foreach (Products p in PerkItems)
            {

                Console.WriteLine($"{count,2}. {p.ToString()}");
                count++;
            }
        }
         public static List<Products> GetInitalList()
        {
            List<Products> PerkItems = new List<Products>()
                 {

            new Products("Black Drip Coffee","Pure bold invigorating simplicity.", "Drinks", 3.50m, 50),
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
            return PerkItems;
        }



    }
}


