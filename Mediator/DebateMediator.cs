using System;
using System.Collections.Generic;

namespace Mediator
{
    public class DebateMediator : IDebateMediator
    {
        private readonly List<Debator> Debators = new List<Debator>();

        public bool ArgumentIsSuitable(string argument)
        {
            throw new NotImplementedException();
        }     

        public void RegisterDebater(Debator debator)
        {
            Debators.Add(debator);
        }
    }
}
