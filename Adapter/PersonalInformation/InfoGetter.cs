using System.Collections.Generic;

namespace Adapter.PersonalInformation
{
    public class InfoGetter
    {
        public Dictionary<int, string> PossibleAnswers;
        public string QuestionTopic { get; set; }
        public string Question { get; set; }
        public string AnswerPrefix { get; set; }
        public bool LowerCase { get; set; }
        public int CorrectAnswer { get; set; }
    }
}
