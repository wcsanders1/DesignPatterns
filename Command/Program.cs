using Command.Models;
using CommonClientLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Command
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private const string ConfigurableItemsPath = "Items.json";

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE COMMAND PROGRAM - WHICH IS YET FORMLESS");

            while (true)
            {
                List<Item> items;
                try
                {
                    using (var reader = new StreamReader(ConfigurableItemsPath))
                    {
                        var json = reader.ReadToEnd();
                        items = JsonConvert.DeserializeObject<List<Item>>(json);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to retrieve item from '{ConfigurableItemsPath}'.\n" +
                                      $"Exception message: {ex.Message}\n" +
                                      $"Using default questions and answers instead.\n");

                    items = DefaultItems.GetDefaultItems();
                }

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
