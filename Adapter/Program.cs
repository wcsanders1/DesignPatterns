using System;
using CommonClientLib;
using System.Collections.Generic;
using Adapter.PersonalInformation;
using Adapter.Renderers;

namespace Adapter
{
    class Program
    {
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static TextParser TextParser                         = new TextParser();
        private static TypeParser TypeParser                         = new TypeParser(TextParser);

        static void Main(string[] args)
        {
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                  WELCOME TO THE ADAPTER PROGRAM -- WHICH IS SORT OF A FUNNY PROGRAM");
            Console.WriteLine("**********************************************************************************************************\n");

            var questionsAndAnswers = GetQuestionsAndAnswers();
            if (questionsAndAnswers == null)
            {
                return;
            }

            var renderer = new QuestionAndAnswerRenderer();

            var questionsAndAnswersString = renderer.ListQuestionsAndAnswers(questionsAndAnswers);

            Console.WriteLine(questionsAndAnswersString);
            Console.ReadKey();
        }

        private static IEnumerable<QuestionAndAnswer> GetQuestionsAndAnswers()
        {
            var (personalInformationGetters, _) = TypeParser.GetTypeDictionaryAndNameList<IPersonalInformationGettable>();
            var questionsAndAnswers             = new List<QuestionAndAnswer>();

            foreach (var kv in personalInformationGetters)
            {
                var answer = kv.Value.GetAnswer();
                if (string.IsNullOrWhiteSpace(answer))
                {
                    return null;
                }

                questionsAndAnswers.Add(new QuestionAndAnswer
                {
                    Question = kv.Value.GetQuestion(),
                    Answer   = answer
                });
            }

            return questionsAndAnswers;
        }
    }
}
