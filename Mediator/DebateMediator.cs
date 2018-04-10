using System;
using System.Collections.Generic;

namespace Mediator
{
    public class DebateMediator : IDebateMediator
    {
        private readonly List<AbstractDebator> Debators = new List<AbstractDebator>();

        public void ReceiveProposition()
        {
            throw new NotImplementedException();
        }

        public void RegisterDebater(AbstractDebator debator)
        {
            Debators.Add(debator);
        }
    }
}
