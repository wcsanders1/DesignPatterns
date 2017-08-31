using System.Collections.Generic;

namespace Singleton.Topics
{
    public sealed class Weather : AbstractArguable<Weather>, IArguable
    {
        public override string Topic { get; }
        public override List<Argument> ForArguments { get; }
        public override List<Argument> AgainstArguments { get; }

        public Weather()
        {
            Topic = "Weather";
            ForArguments = GetForArguments();
            AgainstArguments = GetAgainstArguments();
        }

        private List<Argument> GetForArguments()
        {
            return new List<Argument>
            {
                new Argument
                {
                    Position = ForOrAgainst.For,
                    Proposition = "I think weather is a good thing, and that we should continue to have it."
                },
                new Argument
                {
                    Position = ForOrAgainst.For,
                    Proposition = "I like being outside in the weather."
                },
                new Argument
                {
                    Position = ForOrAgainst.For,
                    Proposition = "The weather is what gives us nice days."
                }
            };
        }

        private List<Argument> GetAgainstArguments()
        {
            return new List<Argument>
            {
                new Argument
                {
                    Position = ForOrAgainst.Against,
                    Proposition = "Weather is bad because it makes rain and strong wind."
                },
                new Argument
                {
                    Position = ForOrAgainst.Against,
                    Proposition = "If we didn't have weather, then it wouldn't get too hot!"
                },
                new Argument
                {
                    Position = ForOrAgainst.Against,
                    Proposition = "Weather is bad because it's always making different skies."
                }
            };
        }
    }
}
