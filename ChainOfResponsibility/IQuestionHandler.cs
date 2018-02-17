namespace ChainOfResponsibility
{
    public interface IQuestionHandler
    {
        void AnswerQuestion(Question question);
        void RegisterNext(IQuestionHandler nextQuestionHandler);
    }
}
