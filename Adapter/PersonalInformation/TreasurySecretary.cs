using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Adapter.PersonalInformation
{
    public class TreasurySecretary : AbstractPersonalInformation, IPersonalInformationGettable
    {
        public string QuestionTopic { get; } = "Favorite Treasury Secretary";

        private readonly ContinuationDeterminer continuationDeterminer;

        public TreasurySecretary(ContinuationDeterminer continuationDeterminer) : base(continuationDeterminer)
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

                if (!GetChoice(possibleAnswers, out var answer))
                {
                    return null;
                }

                if (answer != null)
                {
                    return $"Your favorite Treasury Secretary is {answer}.";
                }
            }
        }

        private void AskQuestion()
        {
            Console.WriteLine("Who is your favorite secretary of the treasury?");
        }

        private Dictionary<int, string> GetPossibleAnswers()
        {
            return new Dictionary<int, string>
            {
                {1, "Salmon P. Chase" },
                {2, "Salmon P. Chase" },
                {3, "Bill Cosby" }
            };
        }
    }
}
