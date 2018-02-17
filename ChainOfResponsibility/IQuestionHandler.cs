namespace ChainOfResponsibility
{
    public interface IQuestionHandler
    {
        string AnswerQuestion(Question question);
        void RegisterNext(IQuestionHandler nextQuestionHandler)
    }
}
