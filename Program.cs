using System;
using System.Collections.Generic;

namespace Bogosort
{
    class Program
    {
        static bool IsListSorted(List<int> listToCheck)
        {
            for (int i = 0; i < listToCheck.Count - 1; i++)
            {
                if (listToCheck[i + 1] < listToCheck[i])
                {
                    return false;
                }
            }

            return true;
        }

        static void GenerateRandomElements(List<int> listToGenerateIn, int listSize, ref long attemptsMade)
        {
            var randomizer = new Random();
            for (int i = 0; i < listSize; i++)
            {
                var randomValue = randomizer.Next(int.MaxValue);
                listToGenerateIn.Add(randomValue);
            }
            attemptsMade++;
        }

        static string PrettifyDuration(TimeSpan duration)
        {
            return $"{duration.Days} days {duration.Hours} hours {duration.Minutes} minutes {duration.Seconds} seconds {duration.Milliseconds} milliseconds";
        }

        static void Main(string[] args)
        {
            int listSize = 0;
            long attemptsMade = 0;
            if (args.Length > 0 && int.TryParse(args[0], out _))
            {
                listSize = Convert.ToInt32(args[0]);
            }
            if (listSize == 0)
            {
                Console.Write("Enter how many elements you want to bogosort: ");
                while (!int.TryParse(Console.ReadLine(), out listSize))
                {
                    Console.Write("Enter how many elements you want to bogosort: ");
                }
            }

            var startDate = DateTime.Now;
            var listToOperateOn = new List<int>();
            GenerateRandomElements(listToOperateOn, listSize, ref attemptsMade);

            while (!IsListSorted(listToOperateOn))
            {
                if (attemptsMade % 100_000 == 0)
                {
                    Console.WriteLine($"Attempts thus far: {attemptsMade:N0}");
                }
                listToOperateOn.Clear();
                GenerateRandomElements(listToOperateOn, listSize, ref attemptsMade);
            }
            var prettyDuration = PrettifyDuration(TimeSpan.FromMilliseconds(DateTime.Now.Subtract(startDate).TotalMilliseconds));
            Console.WriteLine($"Finished bogosort of {listSize} elements in {prettyDuration} after {attemptsMade:N0} attempts!");
        }
    }
}
