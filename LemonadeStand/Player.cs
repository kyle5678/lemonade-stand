using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    class Player
    {
        public static double Balance = 50;
        public static int Day = 0;

        public static bool Deduct(double deduction)
        {
            if (Balance > deduction)
            {
                Balance -= deduction;
                return true;
            }

            else
            {
                Console.WriteLine($"You only have ${Balance}! You can't spend ${deduction}.");
                return false;
            }
        }
    }    
}
