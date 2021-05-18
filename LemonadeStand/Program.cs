using System;
using System.IO;

namespace LemonadeStand
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GameCode();
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            finally
            {
                Console.ReadKey();
            }
        }

        public static void GameCode()
        {
            Load();
            while (Player.Balance > 0)
            {
                Game.Day();
                Save();
                if (Player.Balance < 0)
                {
                    Console.WriteLine($"Your balance is now at ${Player.Balance}. You are now bankrupt.");
                    return;
                }
            }
        }

        public static string save = "savedata.txt";

        public static void Save()
        {
            File.WriteAllLines(save, new string[] { Player.Balance.ToString(), Player.Day.ToString() });
        }

        public static void Load()
        {
            if (File.Exists(save))
            {
                Console.Write("You have saved data for this game. Would you like to load this data? (ENTER/ESC)");
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    else if (key.Key == ConsoleKey.Escape)
                        return;
                }

                string[] data = File.ReadAllLines(save);
                Player.Balance = double.Parse(data[0]);
                Player.Day = int.Parse(data[1]);
            }
        }
    }
}
