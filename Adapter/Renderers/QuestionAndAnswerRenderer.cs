using System.Collections.Generic;
using Adapter.PersonalInformation;

namespace Adapter.Renderers
{
    public class QuestionAndAnswerRenderer
    {
        private readonly IQuestionAndAnswerRendererAdapter adapter;

        public QuestionAndAnswerRenderer(IQuestionAndAnswerRendererAdapter adapter)
        {
            this.adapter = adapter;
        }

        public QuestionAndAnswerRenderer() : this (new QuestionsAndAnswersRendererAdapter()) {}

        public string ListQuestionsAndAnswers(IEnumerable<QuestionAndAnswer> questionsAndAnswers)
        {
            return adapter.ListQuestionsAndAnswers(questionsAndAnswers);
        }
    }
}
