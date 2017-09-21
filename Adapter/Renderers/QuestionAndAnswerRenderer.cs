using System.Collections.Generic;
using Adapter.PersonalInformation;

namespace Adapter.Renderers
{
    public class QuestionAndAnswerRenderer
    {
        private readonly IQuestionAndAnswerRendererAdapter renderer;

        public QuestionAndAnswerRenderer(IQuestionAndAnswerRendererAdapter renderer)
        {
            this.renderer = renderer;
        }

        public string ListQuestionsAndAnswers(IEnumerable<QuestionAndAnswer> questionsAndAnswers)
        {
            return renderer.ListQuestionsAndAnswers(questionsAndAnswers);
        }
    }
}
