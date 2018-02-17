namespace ChainOfResponsibility
{
    public class Clergyman : IClergyman
    {
        public string Rank { get; }
        private DegreeOfPhilosophicalDepth DepthLimit { get; }

        public Clergyman(string rank, DegreeOfPhilosophicalDepth depthLimit)
        {
            Rank = rank;
            DepthLimit = depthLimit;
        }

        public bool CanAnswer(DegreeOfPhilosophicalDepth questionDepth)
        {
            return DepthLimit <= questionDepth;
        }

        public string GetAnswer(Question question)
        {
            return question.Answer;
        }
    }
}
