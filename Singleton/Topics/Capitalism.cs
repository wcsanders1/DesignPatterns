using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton.Topics
{
    public sealed class Capitalism : IArguable
    {
        public string Topic { get; }
        public List<Argument> ForArguments { get; }
        public List<Argument> AgainstArguments { get; }

        public static Capitalism Instance
        {
            get
            {
                return instance;
            }
        }

        private static readonly Capitalism instance = new Capitalism();

        private Capitalism()
        {
            Topic = "Capitalism";
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
                    Proposition = "I think capitalism is pretty neat!"
                },
                new Argument
                {
                    Position = ForOrAgainst.For,
                    Proposition = "Capitalism helps us buy things."
                },
                new Argument
                {
                    Position = ForOrAgainst.For,
                    Proposition = "Capitalism is a good way for an economy to be."
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
                    Proposition = "Capitalism isn't cool."
                },
                new Argument
                {
                    Position = ForOrAgainst.Against,
                    Proposition = "Capitalism just allows people to make money, which isn't everything."
                },
                new Argument
                {
                    Position = ForOrAgainst.Against,
                    Proposition = "Capitalism is bad because it makes the weather too hot."
                }
            };
        }
    }
}
