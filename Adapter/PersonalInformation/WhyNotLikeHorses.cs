using System;
using System.Collections.Generic;
using CommonClientLib;

namespace Adapter.PersonalInformation
{
    public class WhyNotLikeHorses : AbstractPersonalInformation, IPersonalInformationGettable
    {
        public string QuestionTopic { get; } = "Reason for Not Liking Horses";

        private readonly ContinuationDeterminer continuationDeterminer;
        private readonly string question = "Why don't you like horses?";

        public WhyNotLikeHorses() : this(new ContinuationDeterminer()) { }
        public WhyNotLikeHorses(ContinuationDeterminer continuationDeterminer) : base(continuationDeterminer)
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
                    return $"You don't like horses because {answer.ToLower()}.";
                }
            }
        }

        public string GetQuestion()
        {
            return question;
        }

        private void AskQuestion()
        {
            Console.WriteLine(question);
        }

        private Dictionary<int, string> GetPossibleAnswers()
        {
            return new Dictionary<int, string>
            {
                {1, "They're stupid" },
                {2, "They age without grace" },
                {3, "It's raining" }
            };
        }
    }
}
