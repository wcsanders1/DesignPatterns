using System.Collections.Generic;

namespace Mediator
{
    public abstract class Debator
    {
        private readonly IDebateMediator DebateMediator;
        private readonly string Name;
        private readonly List<string> Arguments;

        public Debator(IDebateMediator debateMediator, string name, List<string> arguments)
        {
            DebateMediator = debateMediator;
            Name = name;
            Arguments = arguments;
        }



        public string MakeArgument(string argument)
        {
            return $"{Name}: {argument}";
        }
    }
}
