using System;
using System.Collections.Generic;
using System.Linq;
using Command.Models;

namespace Command.Commands
{
    public class DisplaySummary : AbstractCommand, ICommand
    {
        public string Description { get; } = "Show a summary of your order";
        
        public Order Execute(Order order, List<Item> items)
        {
            if (order?.Items?.Count == 0)
            {
                Console.WriteLine("\nYour order is currently empty.\n");
                return order;
            }

            var priceByItem = items.ToDictionary(k => k.Name, v => v.Price);
            
            Console.WriteLine("Here is a summary of your order:\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{ItemField}{new string(' ', LeftBuffer - (ItemField.Length + PriceField.Length))}" +
                              $"{PriceField}{new string(' ', LeftBuffer - QuantityField.Length)}" +
                              $"{QuantityField}{new string(' ', LeftBuffer - TotalField.Length)}" +
                              $"{TotalField}\n");

            var totalCost = 0M;
            foreach (var item in order.Items)
            {
                var price = priceByItem[item.Key];
                var quantity = item.Value;
                var cost = (price * quantity);
                totalCost += cost;

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{item.Key}{new string(' ', LeftBuffer - (item.Key.Length + price.ToString("C").Length))}" +
                                  $"{price.ToString("C")}{new string(' ', LeftBuffer - quantity.ToString().Length)}" +
                                  $"{quantity.ToString()}{new string(' ', LeftBuffer - cost.ToString("C").Length)}" +
                                  $"{cost.ToString("C")}");
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{new string(' ', (LeftBuffer * 3) - totalCost.ToString("C").Length)}{totalCost.ToString("C")}");
            Console.ResetColor();
            Console.WriteLine();

            return order;
        }
    }
}
