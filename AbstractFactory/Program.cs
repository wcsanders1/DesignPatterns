using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SimpleFactory
{
    public class Program
    {
        static void Main(string[] args)
        {
            const int MAX_ELEMENTS_TO_PRINT = 1000;
            var keepLooping = true;
            var stopWatch = new Stopwatch();

            while (keepLooping)
            {
                var array = GetArrayToSort();
                if (array == null || array.Length < 1)
                {
                    continue;
                }

                if (array.Length <= MAX_ELEMENTS_TO_PRINT)
                {
                    Console.WriteLine("\nHERE IS THE UNSORTED ARRAY:*********************************************************\n");
                    PrintArray(array);
                }
                else
                {
                    Console.WriteLine("\n(That array is too large to print to the console, because it would just take too long.)\n");
                }

                var sorter = GetSorter();
                if (sorter == null)
                {
                    continue;
                }
                
                var elapsedTime = SortArray(array, sorter, stopWatch);

                if (array.Length <= MAX_ELEMENTS_TO_PRINT)
                {
                    Console.WriteLine("\nHERE IS THE SORTED ARRAY:***********************************************************\n");
                    PrintArray(array);
                }
                else
                {
                    Console.WriteLine("\n(Again, the array is too large to print to the console, so I won't print it.)\n");
                }

                PrintResults(elapsedTime);

                keepLooping = GoAgain();
                stopWatch.Reset();
                Console.WriteLine();
            }
        }

        static private int[] GetArrayToSort()
        {
            Console.WriteLine("Enter the amount of numbers you want to sort.\n");

            if (!Int32.TryParse(Console.ReadLine(), out var arraySize))
            {
                Console.WriteLine("That's not a valid number. I guess we'll try this again.\n");
                return null;
            }

            var randomNumGenerator = new Random();
            var arrayToSort = new int[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                arrayToSort[i] = randomNumGenerator.Next(1, arraySize);
            }

            return arrayToSort;
        }

        static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i == array.Length - 1)
                {
                    Console.Write(array[i] + "\n\n");
                    continue;
                }

                Console.Write(array[i] + ", ");
            }
        }

        static void PrintSorters()
        {
            Console.WriteLine("Choose which sort method you want to use (these methods are listed in order of slowest to fastest):\n");

            var sorters = Enum.GetValues(typeof(Sorters));
            foreach (var sorter in sorters)
            {
                var sorterName = Regex.Replace(sorter.ToString(), "(\\B[A-Z])", " $1");
                Console.WriteLine($"{(int)sorter}. {sorterName}");
            }
            Console.WriteLine();
        }

        static AbstractSorter<int> GetSorter()
        {
            PrintSorters();

            if (!Int32.TryParse(Console.ReadLine(), out int sortChosen))
            {
                Console.WriteLine("That input isn't valid at all, so let's try again I guess.\n");
                return null;
            }

            var sorterFactory = new SorterFactory<int>();
            var sortMethod = (Sorters)sortChosen;

            return sorterFactory.CreateInstance(sortMethod);
        }

        static TimeSpan SortArray(int[] array, AbstractSorter<int> sorter, Stopwatch stopWatch)
        {
            Console.Write($"\nSorting the array now using {sorter.Name}, please wait...  ");

            stopWatch.Start();
            sorter.Sort(array);
            stopWatch.Stop();

            Console.Write("Finished.\n");

            return stopWatch.Elapsed;
        }

        static void PrintResults(TimeSpan elapsedTime)
        {
            Console.Write("Here is how long it took to sort that array: ");
            var elapsedTimeToString = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds);
            Console.WriteLine(elapsedTimeToString + "\n");
        }

        static bool GoAgain()
        {
            Console.WriteLine("Enter 0 to quit, or something else to have some more fun.\n");
            var goAgain = Console.ReadLine();

            if (goAgain == "0")
            {
                return false;
            }

            return true;
        }
    }
}
