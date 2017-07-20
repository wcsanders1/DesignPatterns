using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SimpleFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var keepLooping = true;
            var stopWatch = new Stopwatch();

            while (keepLooping)
            {
                Console.WriteLine("Enter the amount of numbers you want to sort.\n");
                int arraySize = Int32.Parse(Console.ReadLine());

                var arrayToSort = new int[arraySize];
                var randomNumGenerator = new Random();

                for (int i = 0; i < arraySize; i++)
                {
                    arrayToSort[i] = randomNumGenerator.Next(1, arraySize);
                }

                if (arraySize <= 1000)
                {
                    Console.WriteLine("\nHERE IS THE UNSORTED ARRAY:*********************************************************\n");

                    for (int i = 0; i < arraySize; i++)
                    {
                        if (i == arraySize - 1)
                        {
                            Console.Write(arrayToSort[i] + "\n\n");
                            continue;
                        }

                        Console.Write(arrayToSort[i] + ", ");
                    }
                }
                else
                {
                    Console.WriteLine("\n(That array is too large to print to the console, because it would just take too long.)\n");
                }

                Console.WriteLine("Choose which sort method you want to use (these methods are listed in order of slowest to fastest:");
                Console.WriteLine("1: Selection Sort");
                Console.WriteLine("2: Intertion Sort");
                Console.WriteLine("3: Shell Sort");
                Console.WriteLine("4: Top Down Merge Sort\n");

                if (!Int32.TryParse(Console.ReadLine(), out int sortChosen))
                {
                    Console.WriteLine("That input isn't valid at all, so let's try again I guess.\n");
                    continue;
                }

                var sortMethod = (Sorters)sortChosen;
                var sorterFactory = new SorterFactory<int>();
                var sorter = sorterFactory.CreateInstance(sortMethod);

                if (sorter == null)
                {
                    Console.WriteLine("That input isn't valid at all, so let's try again I guess.\n");
                    continue;
                }

                var methodToString = Regex.Replace(sortMethod.ToString(), "(\\B[A-Z])", " $1");

                Console.Write($"\nSorting the array now using {methodToString}, please wait...  ");

                stopWatch.Start();

                sorter.Sort(arrayToSort);

                stopWatch.Stop();

                Console.Write("Finished.\n");

                var elapsedTime = stopWatch.Elapsed;

                if (arraySize <= 1000)
                {
                    Console.WriteLine("\nHERE IS THE SORTED ARRAY:***********************************************************\n");

                    for (int i = 0; i < arraySize; i++)
                    {
                        if (i == arraySize - 1)
                        {
                            Console.WriteLine(arrayToSort[i] + "\n\n");
                            continue;
                        }
                        Console.Write(arrayToSort[i] + ", ");
                    }
                }
                else
                {
                    Console.WriteLine("\n(Again, the array is too large to print to the console, so I won't print it.)\n");
                }

                Console.Write("Here is how long it took to sort that array: ");
                var elapsedTimeToString = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds);
                Console.WriteLine(elapsedTimeToString + "\n");

                Console.WriteLine("Enter 0 to quit, or something else to have some more fun.\n");
                var goAgain = Int32.Parse(Console.ReadLine());

                if (goAgain == 0)
                    keepLooping = false;

                stopWatch.Reset();
                Console.WriteLine();
            }
        }
    }
}
