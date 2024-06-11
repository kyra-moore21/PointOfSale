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
            while (!Regex.IsMatch(input, expirationDateRegex))
            {
                Console.WriteLine("Please enter expiration using MM/YY");
                input = Console.ReadLine();
            }
            return input;
        }

        public static int UserChoice(List<Products> menu)

        {
            int choice = -1;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > menu.Count || menu[choice - 1].Stock == 0)

            {
                if (choice == 99)
                {
                    break;
                }

                Console.WriteLine("Invalid input or item out of stock. Please try again.");

            }

            return choice;
        }

        public static int Qty(int stock)
        {
            int quantity = 0;
            while (!int.TryParse(Console.ReadLine(), out quantity) || quantity > stock || quantity <= 0)
            {
                Console.WriteLine("Invalid Input, please try again");
            }
            return quantity;
        }
        public static bool GetContinue(string message)
        {
            bool result = false;
            while (true)
            {
                Console.WriteLine($"{message} y/n");
                string choice = Console.ReadLine().Trim().ToLower();
                if (choice == "y")
                {
                    result = true;
                    break;
                }
                else if (choice == "n")
                {
                    result = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again");
                }
            }
            return result;
        }
        public static decimal Cash_Validator(decimal GrandTotal)
        {
            decimal result = -1;

            while (!decimal.TryParse(Console.ReadLine(), out result) || result <= 0 || result < GrandTotal)
            {
                Console.WriteLine("Insufficient funds. Please try again.");
            }

            return result;
        }

        public static decimal RunningSubtotal(decimal Running)
        {

            decimal subtotal = 0;

            subtotal = Running + subtotal;

            return subtotal;

        }

        public static decimal GrandTotal(decimal subtotal, decimal salestax)
        {

            return salestax * subtotal;

        }

        public static string CashPayment(decimal grandTotal)
        {
            Console.WriteLine("\nPlease enter amount of cash.");

            decimal CASH = Cash_Validator(grandTotal);
            decimal CHANGE = CASH - Math.Round(grandTotal, 2);
            return $"You have paid your total of ${Math.Round(grandTotal, 2)} with cash. Your change is ${CHANGE}\n";

        }

        public static string CreditPayment(decimal grandTotal)
        {

            // Expiration Date regex pattern


            Console.WriteLine("\nPlease enter your Credit Card number using this format XXXX-XXXX-XXXX-XXXX: ");
            string creditcardnum = GetCreditCard(Console.ReadLine());


            Console.WriteLine("Please enter the expiration date MM/YY: ");
            GetMMYY(Console.ReadLine());

            Console.WriteLine("Please enter the cvv number: ");
            GetCVV(Console.ReadLine());

            return $"You have paid your total of {Math.Round(grandTotal, 2)} with credit card ending in XXXX XXXX XXXX {creditcardnum.Substring(creditcardnum.Length - 4)}\n";
        }


        public static string CheckPayment(decimal grandTotal)
        {
            Console.Write("\nPlease enter your Check number: ");
            double checknum = GetPositiveInputDouble();
            return $"You have paid your total of {Math.Round(grandTotal, 2)} with check #{checknum}.\n";
        }

        public static Dictionary<string, int> AddToReceipt(Dictionary<string, int> OrderedItems, string choice, int qty)
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

        public static void ShowReceipt(Dictionary<string, int> OrderedItems, decimal lineTotal, decimal salesTax)
        {

            Console.WriteLine("\n ----------Receipt-----------\n");
            foreach (KeyValuePair<string, int> pair in OrderedItems)
            {
                Console.WriteLine($"{pair.Key,-27}{pair.Value,3}\n");
            }
            Console.WriteLine($"{"Subtotal:",-25}{ValidatorPOS.RunningSubtotal(lineTotal),5:C}");

            Console.WriteLine($"{"Sales tax:",-25}{(salesTax - 1) * lineTotal,5:C}");

            Console.WriteLine($"{"Grand Total:",-25}{ValidatorPOS.GrandTotal(lineTotal, salesTax),5:C}");

        }
        public static int GetPositiveInputInt()
        {
            int result = -1;
            while (int.TryParse(Console.ReadLine(), out result) == false || result <= 0)
            {
                Console.WriteLine("Invalid input. Try again with a positive number.");
            }
            return result;
        }
        public static double GetPositiveInputDouble()
        {
            double result = -1;
            while (double.TryParse(Console.ReadLine(), out result) == false || result <= 0)
            {
                Console.WriteLine("Invalid input. Try again with a positive number.");
            }
            return result;
        }
        public static decimal GetPositiveInputDecimal()
        {
            decimal result = -1;
            while (decimal.TryParse(Console.ReadLine(), out result) == false || result <= 0)
            {
                Console.WriteLine("Invalid input. Try again with a positive number.");
            }
            return result;
        }


    }
}
