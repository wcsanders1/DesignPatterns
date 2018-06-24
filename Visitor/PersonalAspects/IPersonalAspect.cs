namespace Visitor.PersonalAspects
{
    public interface IPersonalAspect
    {
        void Accept(IVisitor visitor);
    }
}
