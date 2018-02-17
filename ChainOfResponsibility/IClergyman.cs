namespace ChainOfResponsibility
{
    public interface IClergyman
    {
        string Rank { get; }

        bool CanAnswer(DegreeOfPhilosophicalDepth questionDepth);
        void AnswerQuestion(Question question);
    }
}
