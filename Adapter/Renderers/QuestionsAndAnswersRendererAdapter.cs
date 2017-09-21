using System;
using System.Collections.Generic;
using Adapter.PersonalInformation;

namespace Adapter.Renderers
{
    class QuestionsAndAnswersRendererAdapter : IQuestionAndAnswerRendererAdapter
    {
        public string ListQuestionsAndAnswers(IEnumerable<QuestionAndAnswer> questionsAndAnswers)
        {
            throw new NotImplementedException();
        }
    }
}
