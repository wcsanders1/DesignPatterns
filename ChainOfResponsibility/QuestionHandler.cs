using System;

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

        public void AnswerQuestion(Question question)
        {
            if (!Clergyman.CanAnswer(question.PhilosophicalDepth))
            {
                if (NextQuestionHandler == null)
                {
                    Console.WriteLine($"\nThe degree of philosophical depth of this question is {question.PhilosophicalDepth.ToString()}.\n" +
                                      $"None of the clergymen are able to answer such a question.\n" +
                                      $"You'll have to seek the answer elsewhere.\n");

                    return;
                }
                
                NextQuestionHandler.AnswerQuestion(question);

                return;
            }

            Clergyman.AnswerQuestion(question);
        }

        public void RegisterNext(IQuestionHandler nextQuestionHandler)
        {
            NextQuestionHandler = nextQuestionHandler;
        }
    }
}
