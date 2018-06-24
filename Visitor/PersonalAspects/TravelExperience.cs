using CommonClientLib;
using System;

namespace Visitor.PersonalAspects
{
    public class TravelExperience : IPersonalAspect
    {
        public int NumberOfCountriesVisited { get; set; }
        public int NumberOfMonthsAbroad { get; set; }

        private static readonly QuestionAsker Asker = new QuestionAsker();
        private static TextPrinter TxtPrinter = new TextPrinter();

        public void SetAspect()
        {
            TxtPrinter.PrintInformation("Now we'll get information regarding your travel experience.", '-', ConsoleColor.DarkGreen);
            NumberOfCountriesVisited = Asker.GetValue<int>("How many countries have you visited?");
            NumberOfMonthsAbroad = Asker.GetValue<int>("What is the total amount of months you've spent abroad?");
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}