using System.Collections.Generic;

namespace Singleton.Topics
{
    public class Golf : IArguable
    {
        public string Topic { get; }
        public List<Argument> ForArguments { get; }
        public List<Argument> AgainstArguments { get; }

        public Golf()
        {
            Topic            = "Golf";
            ForArguments     = GetForArguments();
            AgainstArguments = GetAgainstArguments();
        }

        private List<Argument> GetForArguments()
        {
            return new List<Argument>
            {
                new Argument
                {
                    Position = ForOrAgainst.For,
                    Proposition = "Golf is a great sport because it's outside."
                },
                new Argument
                {
                    Position = ForOrAgainst.For,
                    Proposition = "You can play golf even when you're old."
                },
                new Argument
                {
                    Position = ForOrAgainst.For,
                    Proposition = "Every golf course is different, so there's lots of variety."
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
                    Proposition = "Golf takes a long time to play."
                },
                new Argument
                {
                    Position = ForOrAgainst.Against,
                    Proposition = "Golf can be very frustrating."
                },
                new Argument
                {
                    Position = ForOrAgainst.Against,
                    Proposition = "Golf is very dangerous because of wild nature and wild shots."
                }
            };
        }
    }
}
