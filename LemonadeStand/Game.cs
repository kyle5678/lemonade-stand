using System;
using System.Collections.Generic;
using System.Text;

namespace LemonadeStand
{
    public enum Weather
    {
        Sunny,
        PartlyCloudy,
        Cloudy,
        Windy,
        Rainy
    }

    public enum Temperature
    {
        Hot,
        Warm,
        Cold
    }

    class Game
    {
        public static Dictionary<Temperature, int> TemperatureWeight = new Dictionary<Temperature, int>
        {
            { Temperature.Hot, 3 },
            { Temperature.Warm, 2 },
            { Temperature.Cold, 1 }
        };

        public static Dictionary<Temperature, Dictionary<Weather, int>> WeatherWeight = new Dictionary<Temperature, Dictionary<Weather, int>>
        {
            {
                Temperature.Hot,
                new Dictionary<Weather, int>
                {
                    { Weather.Sunny, 4 },
                    { Weather.PartlyCloudy, 2 },
                    { Weather.Cloudy, 2 }
                }
            },
            {
                Temperature.Warm,
                new Dictionary<Weather, int>
                {
                    { Weather.Sunny, 2 },
                    { Weather.PartlyCloudy, 3 },
                    { Weather.Cloudy, 3 }
                }
            },
            {
                Temperature.Cold,
                new Dictionary<Weather, int>
                {
                    { Weather.Cloudy, 1 },
                    { Weather.Windy, 3 },
                    { Weather.Rainy, 4 }
                }
            }
        };

        public static Dictionary<Weather, double> WeatherSellChance = new Dictionary<Weather, double>
        {
            { Weather.Sunny, 0.75 },
            { Weather.PartlyCloudy, 0.65 },
            { Weather.Cloudy, 0.5 },
            { Weather.Windy, 0.25 },
            { Weather.Rainy, 0.15 }
        };

        public static Dictionary<Temperature, double> TemperatureMultiplier = new Dictionary<Temperature, double>
        {
            { Temperature.Hot, 1.25 },
            { Temperature.Warm, 1 },
            { Temperature.Cold, 0.75 }
        };

        public const double CupCost = 0.15;
        public const double SignCost = 0.50;

        public static void Day()
        {
            Console.Clear();
            Player.Day++;
            Console.WriteLine($"It's Day #{Player.Day} of selling lemonade!");

            Random rnd = new Random();
            Temperature currentTemperature = TemperatureWeight.Random();
            Weather currentWeather = WeatherWeight[currentTemperature].Random();
            Console.WriteLine($"Today the weather is {currentTemperature} and {currentWeather}.");
            Wait();

            double chanceToChange = (rnd.NextDouble() + rnd.NextDouble()) / 2;
            Temperature forecastTemperature = TemperatureWeight.Random();
            Weather forecastWeather = WeatherWeight[forecastTemperature].Random();
            Console.WriteLine($"However, there is a {Math.Floor(chanceToChange * 100)}% chance that it will be {forecastTemperature} and {forecastWeather} later on.");
            Wait();

            double expenses = 0;
            int cups;
            Console.WriteLine($"To make a cup of lemonade costs ${CupCost}. How many cups of lemonade would you like to make? (${Player.Balance})");
            while (true)
            {
                cups = InputInt();
                if (cups <= 0)
                {
                    Console.WriteLine("You gave up on the useless day.");
                    Wait();
                    return;
                }

                if (Player.Deduct(cups * CupCost))
                {
                    expenses += cups * CupCost;
                    break;
                }
            }

            int signs;
            Console.WriteLine($"To buy a sign to bring more customers to your shop costs ${SignCost}. How many signs would you want? (${Player.Balance})");
            while (true)
            {
                signs = InputInt();
                if (Player.Deduct(signs * SignCost))
                {
                    expenses += signs * SignCost;
                    break;
                }
            }

            Console.WriteLine($"How much would you like to charge people for lemonade?");
            double lemonadeCost = InputDouble();

            Temperature temperature;
            Weather weather;

            if (rnd.NextDouble() < chanceToChange)
            {
                temperature = forecastTemperature;
                weather = forecastWeather;
                Console.WriteLine($"The weather became {temperature} and {weather}.");
                Wait();
            }

            else
            {
                temperature = currentTemperature;
                weather = currentWeather;
                Console.WriteLine($"The weather stayed {temperature} and {weather}.");
                Wait();
            }

            double chanceToBuy = TemperatureMultiplier[temperature] * WeatherSellChance[weather] / (Math.Pow(2, lemonadeCost) / 2);
            int sold = 0;
            for (int i = 0; i < (cups > 15 ? 15 : cups); i++)
            {
                if (rnd.NextDouble() < chanceToBuy)
                {
                    sold++;
                }
            }

            int signSee = (int)Math.Floor((double)((rnd.Next(signs - 2, signs + 3) + rnd.Next(signs - 2, signs + 3)) / 2));
            for (int i = 0; i < signSee; i++)
            {
                if (sold >= cups)
                    break;
                if (rnd.NextDouble() < chanceToBuy)
                {
                    sold++;
                }
            }

            Console.WriteLine($"You successfully sold {sold} cups of lemonade.");
            Wait();

            double earnings = sold * lemonadeCost;
            Console.WriteLine($"You earned ${earnings} from this.");
            Player.Balance += earnings;

            double profit = earnings - expenses;
            if (profit > 0)
                Console.WriteLine($"You made a profit of ${profit}. (${Player.Balance})");
            else
                Console.WriteLine($"You lost ${0 - profit}. (${Player.Balance})");

            Wait();
        }

        private static int InputInt()
        {
            int num;
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out num))
                    break;
            }

            return num;
        }

        private static double InputDouble()
        {
            double num;
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                if (double.TryParse(input, out num))
                    break;
            }

            return num;
        }

        private static void Wait()
        {
            Console.ReadKey(true);
        }
    }
}
