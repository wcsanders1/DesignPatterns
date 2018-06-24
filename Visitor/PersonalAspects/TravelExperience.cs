using CommonClientLib;

namespace Visitor.PersonalAspects
{
    public class TravelExperience : IPersonalAspect
    {
        public int NumberOfCountriesVisited { get; set; }
        public int NumberOfMonthsAbroad { get; set; }

        private static readonly QuestionAsker Asker = new QuestionAsker();

        public void SetAspect()
        {
            NumberOfCountriesVisited = Asker.GetValue<int>("How many countries have you visited?");
            NumberOfMonthsAbroad = Asker.GetValue<int>("What is the total amount of months you've spent abroad?");
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}