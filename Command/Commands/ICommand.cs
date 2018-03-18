using Command.Models;
using System.Collections.Generic;

namespace Command.Commands
{
    public interface ICommand
    {
        string Description { get; }
        Order Execute(Order order, List<Item> items);
    }
}
