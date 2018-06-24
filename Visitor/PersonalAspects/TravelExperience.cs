namespace Visitor.PersonalAspects
{
    public class TravelExperience : IPersonalAspect
    {
        public int NumberOfCountriesVisited { get; set; }
        public int NumberOfMonthsAbroad { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
