using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Adapter.PersonalInformation
{
    public static class QuestionAndAnswerGetter
    {
        private const string Instruction = "Type the number corresponding to the correct answer below.";
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

        public static List<QuestionAndAnswer> GetQuestionsAndAnswers(List<InfoGetter> infoGetters)
        {
            var questionsAndAnswers = new List<QuestionAndAnswer>();
            foreach (var infoGetter in infoGetters)
            {
                var questionAndAnswer = new QuestionAndAnswer();
                var (answerGiven, answerGivenShortForm) = GetAnswerAndShortForm(infoGetter);
                if (answerGiven == null || answerGivenShortForm == null)
                {
                    return null;
                }

                questionAndAnswer.AnswerGiven = answerGiven;
                questionAndAnswer.AnswerGivenShortForm = answerGivenShortForm;
                questionAndAnswer.CorrectAnswer = infoGetter.PossibleAnswers[infoGetter.CorrectAnswer];
                questionAndAnswer.Topic = infoGetter.QuestionTopic;
                questionAndAnswer.Question = infoGetter.Question;

                questionsAndAnswers.Add(questionAndAnswer);
            };

            return questionsAndAnswers;
        }

        private static (string, string) GetAnswerAndShortForm(InfoGetter infoGetter)
        {
            while (true)
            {
                AskQuestion(infoGetter.Question);
                PrintInstruction();
                PrintPossibleAnswers(infoGetter.PossibleAnswers);

                if (!GetChoice(infoGetter.PossibleAnswers, out var answer, out var index))
                {
                    return (null, null);
                }

                if (answer != null)
                {
                    var answerGiven = infoGetter.LowerCase ? $"{infoGetter.AnswerPrefix} {answer.ToLower()}." :
                        $"{infoGetter.AnswerPrefix} {answer}.";
                    var answerGivenShortForm = infoGetter.PossibleAnswers[index];

                    return (answerGiven, answerGivenShortForm);
                }
            }
        }

        private static void AskQuestion(string question)
        {
            Console.WriteLine(question);
        }

        private static void PrintInstruction()
        {
            Console.WriteLine(Instruction);
        }

        private static void PrintPossibleAnswers(Dictionary<int, string> possibleAnswers)
        {
            Console.WriteLine();
            foreach (var kv in possibleAnswers)
            {
                Console.WriteLine($"{kv.Key}. {kv.Value}");
            }
        }

        private static bool GetChoice(Dictionary<int, string> possibleAnswers, out string choice, out int tempChoice)
        {
            Console.WriteLine();
            if (!Int32.TryParse(Console.ReadLine(), out tempChoice))
            {
                choice = null;
                return ContinuationDeterminer.GoAgainWithInvalidInputMessage();
            }

            if (!possibleAnswers.TryGetValue(tempChoice, out choice))
            {
                return ContinuationDeterminer.GoAgainWithInvalidInputMessage("That isn't one of the choices.");
            }
            Console.WriteLine();

            return true;
        }
    }
}
