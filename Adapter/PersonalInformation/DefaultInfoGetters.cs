using System.Collections.Generic;

namespace Adapter.PersonalInformation
{
    public static class DefaultInfoGetters
    {
        public static List<InfoGetter> GetDefaultInfoGetters()
        {
            return new List<InfoGetter>
            {
                new InfoGetter
                {
                    PossibleAnswers = new Dictionary<int, string>
                    {
                        { 1, "Twice" },
                        { 2, "Five times" },
                        { 3, "Nearly six times" }
                    },
                    QuestionTopic = "Times Hit By Train",
                    Question = "How many times have you been hit by a train?",
                    AnswerPrefix = "You've been hit by a train",
                    LowerCase = true,
                    CorrectAnswer = 2
                },
                new InfoGetter
                {
                    PossibleAnswers = new Dictionary<int, string>
                    {
                        {1, "Salmon P. Chase" },
                        {2, "Bud Bedsy" },
                        {3, "Bill Cosby" }
                    },
                    QuestionTopic = "Favorite Treasury Secretary",
                    Question = "Who is your favorite secretary of the treasury?",
                    AnswerPrefix = "Your favorie Treasury Secretary is",
                    LowerCase = false,
                    CorrectAnswer = 1
                },
                new InfoGetter
                {
                    PossibleAnswers = new Dictionary<int, string>
                    {
                        {1, "They're stupid" },
                        {2, "They age without grace" },
                        {3, "It's raining" }
                    },
                    QuestionTopic = "Reason For Not Liking Horses",
                    Question = "Why don't you like horses?",
                    AnswerPrefix = "You don't like horses because",
                    LowerCase = true,
                    CorrectAnswer = 2
                }
            };
        }
    }
}
