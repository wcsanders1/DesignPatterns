using CommonClientLib;
using System;
using System.Diagnostics;

namespace SimpleFactory
{
    public class Program
    {
        private static TextPrinter TxtPrinter                        = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static TextParser TxtParser                          = new TextParser();

        static void Main()
        {
            const int MAX_ELEMENTS_TO_PRINT = 1000;
            var keepLooping                 = true;
            var stopWatch                   = new Stopwatch();

            TxtPrinter.PrintInformation("WELCOME TO THE SORT PROGRAM -- WHICH IS A PRETTY NEAT PROGRAM!");
            
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

                keepLooping = ContinuationDeterminer.GoAgain();
                stopWatch.Reset();
                Console.WriteLine();
            }
        }

        static private int[] GetArrayToSort()
        {
            Console.WriteLine($"Enter the amount of numbers you want to sort. The number must be less than {Int32.MaxValue.ToString("N0")}.\n");

            if (!Int32.TryParse(Console.ReadLine(), out var arraySize))
            {
                Console.WriteLine("That's not valid input. I guess we'll try this again.\n");
                return null;
            }

            if (arraySize < 2)
            {
                Console.WriteLine("Come on, how can a collection with less than two elements be sorted? Let's try this again.\n");
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

        static AbstractSorter<int> GetSorter()
        {
            Console.WriteLine("Choose which sort method you want to use (these methods are listed in order of slowest to fastest):\n");
            TxtParser.PrintEnum<Sorters>();

            if (!Int32.TryParse(Console.ReadLine(), out int sortChosen))
            {
                Console.WriteLine("That input isn't valid at all, so let's try again I guess.\n");
                return null;
            }

            var sorterFactory = new SorterFactory<int>();
            var sortMethod = (Sorters)sortChosen;

            AbstractSorter<int> sorter;
            try
            {
                sorter = sorterFactory.CreateInstance(sortMethod);
            }
            catch
            {
                Console.WriteLine("Unable to get the kind of sort method you wanted, so I guess we'll try this again.\n");
                return null;
            }

            return sorter;
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
    }
}
