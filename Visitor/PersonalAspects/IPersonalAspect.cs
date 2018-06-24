namespace Visitor.PersonalAspects
{
    public interface IPersonalAspect
    {
        void Accept(IVisitor visitor);

        /// <summary>
        /// Sets the personal aspect based on information obtained from the user.
        /// </summary>
        void SetAspect();
    }
}
