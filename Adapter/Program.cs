using System;
using CommonClientLib;
using System.Collections.Generic;
using Adapter.PersonalInformation;

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
            Console.WriteLine("                  WELCOME TO THE PROTOTYPE PROGRAM -- WHICH IS SORT OF A FUNNY PROGRAM");
            Console.WriteLine("**********************************************************************************************************\n");

            var questionsAndAnswers = GetQuestionsAndAnswers();
        }

        private static IEnumerable<QuestionAndAnswer> GetQuestionsAndAnswers()
        {
            var (personalInformationGetters, _) = TypeParser.GetTypeDictionaryAndNameList<IPersonalInformationGettable>();
            var questionsAndAnswers             = new List<QuestionAndAnswer>();

            foreach (var kv in personalInformationGetters)
            {
                questionsAndAnswers.Add(new QuestionAndAnswer
                {
                    Question = kv.Value.GetQuestion(),
                    Answer   = kv.Value.GetAnswer()
                });
            }

            return questionsAndAnswers;
        }
    }
}
