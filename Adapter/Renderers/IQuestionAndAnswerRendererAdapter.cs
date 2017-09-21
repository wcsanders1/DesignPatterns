using Adapter.PersonalInformation;
using System.Collections.Generic;

namespace Adapter.Renderers
{
    public interface IQuestionAndAnswerRendererAdapter
    {
        string ListQuestionsAndAnswers(IEnumerable<QuestionAndAnswer> questionsAndAnswers);
    }
}
