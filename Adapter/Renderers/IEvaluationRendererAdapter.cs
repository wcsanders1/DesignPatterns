using Adapter.PersonalInformation;
using System.Collections.Generic;

namespace Adapter.Renderers
{
    public interface IEvaluationRendererAdapter
    {
        (string, int) ListTopicsAndScores(IEnumerable<QuestionAndAnswer> questionsAndAnswers);
    }
}
