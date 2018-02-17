namespace ChainOfResponsibility
{
    public class QuestionHandler : IQuestionHandler
    {
        private IClergyman Clergyman { get; }
        private IQuestionHandler NextQuestionHandler { get; set; }

        public QuestionHandler(IClergyman clergyman)
        {
            Clergyman = clergyman;
        }

        public string AnswerQuestion(Question question)
        {
            if (!Clergyman.CanAnswer(question.PhilosophicalDepth))
            {
                return NextQuestionHandler.AnswerQuestion(question);
            }

            return Clergyman.GetAnswer(question);
        }

        public void RegisterNext(IQuestionHandler nextQuestionHandler)
        {
            NextQuestionHandler = nextQuestionHandler;
        }
    }
}
