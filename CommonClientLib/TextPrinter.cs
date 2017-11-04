using System;

namespace CommonClientLib
{
    public class TextPrinter
    {
        /// <summary>
        /// Prints info to the console in a pretty way
        /// </summary>
        /// <param name="info"><code>string</code> to be printed</param>
        /// <param name="border"><code>char</code> constituting the border</param>
        /// <param name="consoleColor">Color of string and border</param>
        public void PrintInformation(string info, char border = '*', ConsoleColor consoleColor = ConsoleColor.DarkGreen)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine($"{new String(border, (Console.WindowWidth - 1))}");

            if (Console.WindowWidth > info.Length)
            {
                Console.Write(new string(' ', (Console.WindowWidth - info.Length) / 2));
            }
            
            Console.WriteLine(info);
            Console.WriteLine($"{new String(border, (Console.WindowWidth - 1))}\n");
            Console.ForegroundColor = prevColor;
        }
    }
}
