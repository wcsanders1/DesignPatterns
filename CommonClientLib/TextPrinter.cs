using System;

namespace CommonClientLib
{
    public class TextPrinter
    {
        /// <summary>
        /// Prints the title of the application in green and centered. Changes the console foreground color to white on exit.
        /// </summary>
        /// <param name="title">Application title.</param>
        public void PrintAppTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{new String('*', (Console.WindowWidth - 1))}");
            Console.Write(new string(' ', (Console.WindowWidth - title.Length) / 2));
            Console.WriteLine(title);
            Console.WriteLine($"{new String('*', (Console.WindowWidth - 1))}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
