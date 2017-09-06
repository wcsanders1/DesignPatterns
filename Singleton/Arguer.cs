using Singleton.Topics;
using System;
using System.Collections.Generic;

namespace Singleton
{
    public sealed class Arguer
    {
        private IArguable _topic;
        private bool _initialStatement = true;
        private Random _randomGenerator;

        public IArguable Topic
        {
            get
            {
                return _topic;
            }

            set
            {
                if (value != _topic)
                {
                    _initialStatement = true;
                }
                else
                {
                    _initialStatement = false;
                }

                _topic = value;
            }
        }

        public Arguer(Random random)
        {
            _randomGenerator = random;
        }

        public string GetArgument(Argument argument)
        {
            if (_topic == null)
            {
                return "You must choose a topic on which to argue.";
            }

            switch (argument.Position)
            {
                case ForOrAgainst.For:
                    return GetResponse(_topic.AgainstArguments);
                case ForOrAgainst.Against:
                    return GetResponse(_topic.ForArguments);
                default:
                    return "There seems to be a problem with the arguer.";
            }
        }

        private string GetResponse(List<Argument> possibleArguments)
        {
            var statement = _randomGenerator.Next(0, possibleArguments.Count + 1);
            if (statement == possibleArguments.Count && !_initialStatement)
            {
                _initialStatement = false;
                var genRsp = _randomGenerator.Next(0, GenericResponses.Count - 1);
                return GenericResponses[genRsp];
            }

            _initialStatement = false;           
            if (statement >= possibleArguments.Count)
            {
                statement--;
            }
            return possibleArguments[statement].Proposition;
        }

        private List<string> GenericResponses = new List<string>
        {
            "Interesting point. Could you explain further?",
            "I think I see what you mean. Perhaps you could say more?",
            "Oh. I hadn't thought of that. Please tell me more so as to further educate me."
        };
    }
}
