using System;

namespace CommonClientLib
{
    public class TextPrinter
    {
        /// <summary>
        /// Prints the title of the application in green and centered.
        /// </summary>
        /// <param name="title">Application title.</param>
        public void PrintAppTitle(string title)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{new String('*', (Console.WindowWidth - 1))}");
            Console.Write(new string(' ', (Console.WindowWidth - title.Length) / 2));
            Console.WriteLine(title);
            Console.WriteLine($"{new String('*', (Console.WindowWidth - 1))}\n");
            Console.ForegroundColor = prevColor;
        }
    }
}
