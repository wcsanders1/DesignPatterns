using System;
using System.Collections.Generic;
using System.Linq;
using Command.Models;
using CommonClientLib;

namespace Command.Commands
{
    public class RemoveItem : AbstractCommand, ICommand
    {
        public string Description { get; } = "Remove items from your order";

        private static QuestionAsker Asker = new QuestionAsker();
        private static TextParser TxtParser = new TextParser();

        public Order Execute(Order order, List<Item> items)
        {
            if (order.Items == null || order.Items.Count == 0)
            {
                Console.WriteLine("\nYou have no items in your order to remove.\n");

                return order;
            }

            var sortedItems = order.Items
                .OrderBy(i => i.Key);

            Console.WriteLine("\nYou have the following items in your order:\n");
            Console.WriteLine($"{ItemField}{new string(' ', LeftBuffer - (ItemField.Length + QuantityField.Length))}" +
                              $"{QuantityField}\n");

            foreach (var item in sortedItems)
            {
                Console.WriteLine($"{item.Key}{new string(' ', LeftBuffer - (item.Key.Length + item.Value.ToString().Length))}" +
                                  $"{item.Value}");
            }

            var itemNames = sortedItems
                .Select(i => i.Key)
                .ToList();
            
            var chosenItemName = itemNames[Asker.GetChoiceFromList("Which category of item do you want to remove from your order?",
                itemNames)];

            var numToRemove = Asker.GetInt($"How many of the item {chosenItemName} do you want to remove from your order?", 
                order.Items[chosenItemName]);

            if (numToRemove >= order.Items[chosenItemName])
            {
                order.Items.Remove(chosenItemName);
                Console.WriteLine($"You have removed all of the {chosenItemName}s from your order.\n");
                
                return order;
            }

            order.Items[chosenItemName] -= numToRemove;
            Console.WriteLine($"You have removed {numToRemove} of the item {chosenItemName} from your order.\n" +
                              $"You now have {order.Items[chosenItemName]} {chosenItemName}{TxtParser.Pluralize(order.Items[chosenItemName])} " +
                              $"remaining.");

            return order;
        }
    }
}
