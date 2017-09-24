using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Adapter.PersonalInformation
{
    public class TreasurySecretary : AbstractPersonalInformation, IPersonalInformationGettable
    {
        private static Dictionary<int, string> PossibleAnswers = new Dictionary<int, string>
        {          
            {1, "Salmon P. Chase" },
            {2, "Bud Bedsy" },
            {3, "Bill Cosby" }
        };

        private const int CorrectAnswer = 1;
        
        public string QuestionTopic { get; } = "Favorite Treasury Secretary";

        private readonly ContinuationDeterminer continuationDeterminer;
        private readonly string question = "Who is your favorite secretary of the treasury?";

        public TreasurySecretary() : this(new ContinuationDeterminer()) {}
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
