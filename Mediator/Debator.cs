using System;
using System.Collections.Generic;

namespace Mediator
{
    public class Debator
    {
        public string Name { get; }

        private List<string> Arguments { get; }

        private readonly IDebateMediator DebateMediator;
        private Random rnd = new Random();

        public Debator(IDebateMediator debateMediator, string name, List<string> arguments)
        {
            DebateMediator = debateMediator;
            Name = name;
            Arguments = arguments;
        }

        public string MakeArgument()
        {
            var argument = Arguments[rnd.Next(Arguments.Count - 1)];
            while (!DebateMediator.ArgumentIsSuitable(argument))
            {
                argument = Arguments[rnd.Next(Arguments.Count - 1)];
            }

            return argument;
        }
    }
}
