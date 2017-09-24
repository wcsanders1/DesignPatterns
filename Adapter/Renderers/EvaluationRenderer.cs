using Adapter.PersonalInformation;
using System.Collections.Generic;

namespace Adapter.Renderers
{
    public class EvaluationRenderer
    {
        private readonly IEvaluationRendererAdapter adapter;

        public EvaluationRenderer(IEvaluationRendererAdapter adapter)
        {
            this.adapter = adapter;
        }

        public EvaluationRenderer() : this (new EvaluationRendererAdapter()) { }

        public (string, int) ListTopicsAndScores(IEnumerable<QuestionAndAnswer> questionsAndAnswers)
        {
            return (null, 0);
        }
    }
}
