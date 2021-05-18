using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    static class Extensions
    {
        public static Random random = new Random();

        public static T Random<T>(this Dictionary<T, int> weights)
        {
            int rollWeight = 0;
            foreach (KeyValuePair<T, int> entry in weights)
            {
                rollWeight += entry.Value;
            }

            int roll = random.Next(1, rollWeight + 1);
            int lowerBoundCheck = 0;
            foreach (KeyValuePair<T, int> entry in weights)
            {
                if (roll <= lowerBoundCheck + entry.Value)
                {
                    return entry.Key;
                }

                lowerBoundCheck += entry.Value;
            }

            return default;
        }

        public static T Print<T>(this T item, string prefix = "", string suffix = "")
        {
            Console.WriteLine($"{prefix}{item}{suffix}");
            return item;
        }
    }
}
