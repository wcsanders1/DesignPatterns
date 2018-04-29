using System;
using System.Collections.Generic;

namespace Mediator
{
    public class DebateMediator : IDebateMediator
    {
        private readonly List<Debator> Debators = new List<Debator>();
        private List<string> UsedArguments = new List<string>();

        public bool ArgumentIsSuitable(string argument)
        {
            return !UsedArguments.Contains(argument);
        }     

        public void RegisterDebator(Debator debator)
        {
            Debators.Add(debator);
        }

        public void Mediate()
        {
            foreach (var debator in Debators)
            {
                var argument = debator.MakeArgument();
                UsedArguments.Add(argument);
                Console.WriteLine($"{debator.Name}: {argument}");
            }

            UsedArguments.Clear();
        }
    }
}
