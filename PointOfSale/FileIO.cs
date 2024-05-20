using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticClass;


namespace PointOfSale
{
    public class FileIO
    {

        //methods
        public static List<Products> AddItem(List<Products> perkItems)
        {
            Console.WriteLine("Enter Product Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Description: ");
            string description = Console.ReadLine();
            Console.WriteLine("Enter Category: ");
            string category = Console.ReadLine();
            Console.WriteLine("Enter Price: ");
            decimal price = Validator.GetPositiveInputDecimal();
            Console.WriteLine("Enter Stock: ");
            int stock = Validator.GetPositiveInputInt();
                
            Products newProduct = new Products(name, description, category, price, stock);
            perkItems.Add(newProduct);
            return perkItems;

        }

        public static void NoFile(string filepath)
        {
            if (File.Exists(filepath) == false)
            {
                StreamWriter tempWriter = new StreamWriter(filepath);
                foreach (Products s in MenuClass.GetInitalList())
                {
                    tempWriter.WriteLine($"{s.Name}|{s.Description}|{s.Category}|{s.Price}|{s.Stock}");
                }
                tempWriter.Close();
            }
        }

        public static void Reader(string filepath, List<Products> menu)
        {

            StreamReader reader = new StreamReader(filepath);
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                else
                {
                    string[] parts = line.Split("|");
                    Products m = new Products(parts[0], parts[1], parts[2], decimal.Parse(parts[3]), int.Parse(parts[4]));
                    menu.Add(m);
                }
            }
            reader.Close();
        }

        public static void UpdateFile(string filepath, List<Products> menu)
        {
            StreamWriter writer = new StreamWriter(filepath);
            foreach (Products s in menu)
            {
                writer.WriteLine($"{s.Name}|{s.Description}|{s.Category}|{s.Price}|{s.Stock}");
            }
            writer.Close();
        }
    }
}
