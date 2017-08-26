using Singleton.Topics;
using System;
using System.Collections.Generic;

namespace Singleton
{
    public sealed class Arguer
    {
        public IArguable Topic { get; set; }
        
        private Random RandomGenerator { get; set; }

        public Arguer(Random random)
        {
            RandomGenerator = random;
        }

        private List<string> GenericResponses = new List<string>
        {
            "Interesting point. Could you explain further.",
            "I think I see what you mean. Perhaps you could say more?",
            "Oh. I hadn't thought of that. Please tell me more so as to further educate me."
        };  

        public string GetArgument(Argument argument)
        {
            if (Topic == null)
            {
                return "You must choose a topic on which to argue.";
            }

            switch (argument.Position)
            {
                case ForOrAgainst.For:
                    return GetResponse(Topic.AgainstArguments);
                case ForOrAgainst.Against:
                    return GetResponse(Topic.ForArguments);
                default:
                    return "There seems to be a problem with the arguer.";
            }
        }

        private string GetResponse(List<Argument> possibleArguments)
        {
            var statement = RandomGenerator.Next(0, possibleArguments.Count);
            if (statement == possibleArguments.Count)
            {
                var genRsp = RandomGenerator.Next(0, GenericResponses.Count - 1);
                return GenericResponses[genRsp];
            }

            return possibleArguments[statement].Proposition;
        }
    }
}
