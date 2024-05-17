using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PointOfSale
{
    public class ValidatorPOS
    {
        public static string GetCreditCard(string input)
        {
            string creditCardRegex = @"^(?:\d{4}-){3}\d{4}$";
            while (!Regex.IsMatch(input, creditCardRegex))
            {
                Console.WriteLine("Invalid input. Try again with a 12 digit credit card number. XXXX-XXXX-XXXX-XXXX");
                input = Console.ReadLine();
            }

            return input;
        }


        public static string GetCVV(string input)
        {
            string cvvRegex = @"^\d{3,4}$";
            while (!Regex.IsMatch(input, cvvRegex))
            {
                Console.WriteLine("Invalid input. Try again with a three or four number");
                input = Console.ReadLine();
            }
            return input;
        }

        public static string GetMMYY(string input)
        {
            string expirationDateRegex = @"^(0[1-9]|1[0-2])\/([0-9]{2})$";
            while(!Regex.IsMatch(input, expirationDateRegex)) 
            {
                Console.WriteLine("Please enter expiration using MM/YY");
                input = Console.ReadLine();
            }
            return input;
        }

        public static int UserChoice(List<Products> menu)
        {
            int choice = -1;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice > menu.Count)
            {
                Console.WriteLine("Invalid Input, please try again");
            }
            return choice;
        }

       public static int Qty(int stock)
        {
            int quantity = 0;
            while (!int.TryParse(Console.ReadLine(), out quantity) || quantity >= stock || quantity <= 0)
            {
                Console.WriteLine("Invalid Input, please try again");
            }
            return stock;
        }
    }
}
