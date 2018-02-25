using Command.Models;
using System.Collections.Generic;

namespace Command
{
    public static class DefaultItems
    {
        public static List<Item> GetDefaultItems()
        {
            return new List<Item>
            {
                new Item
                {
                    Name = "Duck",
                    Price = 20.5M
                },
                new Item
                {
                    Name = "Speaker Set",
                    Price = 30
                },
                new Item
                {
                    Name = "Red Cap",
                    Price = 13.44M
                }
            };
        }
    }
}
