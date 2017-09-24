using System;
using System.Collections.Generic;
using CommonClientLib;

namespace Adapter.PersonalInformation
{
    public class WhyNotLikeHorses : AbstractPersonalInformation, IPersonalInformationGettable
    {
        private static Dictionary<int, string> PossibleAnswers = new Dictionary<int, string>
        {
            {1, "They're stupid" },
            {2, "They age without grace" },
            {3, "It's raining" }
        };

        private const int CorrectAnswer = 2;
        private int AnswerChoice { get; set; }

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

                if (!GetChoice(possibleAnswers, out var answer, out var index))
                {
                    return null;
                }

                if (answer != null)
                {
                    AnswerChoice = index;
                    return $"You don't like horses because {answer.ToLower()}.";
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

        public string GetAnswerShortForm()
        {
            return PossibleAnswers[AnswerChoice];
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
