namespace ChainOfResponsibility
{
    public interface IClergyman
    {
        string Rank { get; }

        bool CanAnswer(DegreeOfPhilosophicalDepth questionDepth);
        string GetAnswer(Question question);
    }
}
