using System.Collections.Generic;

namespace Command.Models
{
    public class Order
    {
        public Dictionary<string, int> Items { get; set; } = new Dictionary<string, int>();
    }
}
