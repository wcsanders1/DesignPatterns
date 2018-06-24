namespace Visitor.PersonalAspects
{
    public class Education : IPersonalAspect
    {
        public EducationLevel EducationLevel { get; set; }
        public decimal GPA { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
