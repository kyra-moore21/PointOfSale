using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale
{
    public class ValidatorPOS
    {
        public static long GetCreditCard()
        {
            long result = -1;
            while (long.TryParse(Console.ReadLine(), out result) == false || result >= 100000000000 && result <= 999999999999)
            {
                Console.WriteLine("Invalid input. Try again with a 12 digit credit card number.");
            }
            return result;
        }

        public static int GetCVV()
        {
            int result = -1;
            while (int.TryParse(Console.ReadLine(), out result) == false || result >= 100 && result <= 999)
            {
                Console.WriteLine("Invalid input. Try again with a three digit positive number");
            }
            return result;
        }
    }
}
