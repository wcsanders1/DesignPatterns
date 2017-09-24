using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Adapter.PersonalInformation
{
    public class HitByTrain : AbstractPersonalInformation, IPersonalInformationGettable
    {
        private static Dictionary<int, string> PossibleAnswers = new Dictionary<int, string>
        {
            { 1, "Twice" },
            { 2, "Five times" },
            { 3, "Nearly six times" }
        };

        private const int CorrectAnswer = 2;
        private int AnswerChoice { get; set; }

        public string QuestionTopic { get; } = "Times Hit By Train";

        private readonly ContinuationDeterminer continuationDeterminer;
        private readonly string question = "How many times have you been hit by a train?";

        public HitByTrain() : this(new ContinuationDeterminer()) { }
        public HitByTrain(ContinuationDeterminer continuationDeterminer) : base(continuationDeterminer)
        {
            this.continuationDeterminer = continuationDeterminer;
        }

        public string GetAnswer()
        {
            while (true)
            {
                AskQuestion();
                var possibleAnswers = GetPossibleAnswers();
                Console.WriteLine(Instruction);
                PrintPossibleAnswers(possibleAnswers);

                if (!GetChoice(possibleAnswers, out var answer, out var index))
                {
                    return null;
                }

                if (answer != null)
                {
                    AnswerChoice = index;
                    return $"You've been hit by a train {answer.ToLower()}.";
                }
            }
        }

        public string GetAnswerShortForm()
        {
            return PossibleAnswers[AnswerChoice];
        }

        public string GetQuestion()
        {
            return question;
        }

        public string GetCorrectAnswer()
        {
            return PossibleAnswers[CorrectAnswer];
        }

        private void AskQuestion()
        {
            Console.WriteLine(question);
        }

        private Dictionary<int, string> GetPossibleAnswers()
        {
            return PossibleAnswers;
        }
    }
}
