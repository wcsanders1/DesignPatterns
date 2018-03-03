using System;
using System.Collections.Generic;
using System.Linq;
using Command.Models;
using CommonClientLib;

namespace Command.Commands
{
    public class AddItem : ICommand
    {
        public string Description { get; } = "Add an item to the order";

        private static QuestionAsker Asker = new QuestionAsker();
        private static TextParser TxtParser = new TextParser();
        private const int LeftBuffer = 25;

        public Order Execute(Order order, List<Item> items)
        {
            var sortedItems = items.OrderBy(i => i.Name).ToList();
            var chosenItem = sortedItems[Asker.GetChoiceFromList("Which item do you want to add to your order?",
                sortedItems.Select(i => $"{i.Name}: {i.Price.ToString("C").PadLeft(LeftBuffer - i.Name.Length)}").ToList())];

            if (order.Items.TryGetValue(chosenItem.Name, out var existingItems))
            {
                Console.WriteLine($"\nYour order contains {existingItems} {chosenItem.Name}{TxtParser.Pluralize(existingItems)}.\n");
            }
            else
            {
                order.Items.Add(chosenItem.Name, 0);
            }

            order.Items[chosenItem.Name] += Asker.GetInt($"How many {chosenItem.Name}s do you want to add to your order?", 
                int.MaxValue - existingItems);

            var newAmountExistingItems = order.Items[chosenItem.Name];
            Console.WriteLine($"\nYour order now contains {newAmountExistingItems} {chosenItem.Name}{TxtParser.Pluralize(newAmountExistingItems)}.\n");

            return order;
        }
    }
}
