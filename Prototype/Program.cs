using System;

namespace Prototype
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter the url of the website you'd like information about");
            var url = Console.ReadLine();

            var explorer = new WebPageExplorer(url);
            var (info, error) = explorer.GetInformationAsync().Result;

            if (error != null)
            {
                Console.WriteLine($"{error.Message}");
                Console.WriteLine($"{error.Exception.Message}");
            }
            else
            {
                Console.WriteLine($"{info.Content.ToString()}");
            }

            Console.ReadKey();
        }
    }
}