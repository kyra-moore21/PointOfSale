using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale
{
    public class Products
    {
        //properties
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        //constructor 
        public Products(string _name, string _category, decimal _price, int _stock)
        {
            Name = _name;
            Category = _category;
            Description = "Central Perk";
            Price = _price;
            Stock = _stock;
        }

        //methods
        public override string ToString()
        {
            
            return $"{Description,5} {Name,-20} \t{Category,-15} \t {Price,-3:C} {Stock,15}\n";
        }

        public static int ReduceStock(int CurrentStock, int Buy)
        {
            int NewStock = CurrentStock - Buy;
            return NewStock;
        }

        public static decimal LineTotal(int qty, decimal price)
        {
           decimal Total = qty* price;

            return Total;
        }

    }
}
