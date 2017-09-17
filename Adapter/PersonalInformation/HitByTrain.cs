using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Adapter.PersonalInformation
{
    public class HitByTrain : IPersonalInformationGettable
    {
        public string QuestionTopic { get; } = "Times Hit By Train";

        private readonly ContinuationDeterminer continuationDeterminer;

        public HitByTrain(ContinuationDeterminer continuationDeterminer)
        {
            this.continuationDeterminer = continuationDeterminer;
        }
        
        public string GetAnswer()
        {
            var keepLooping = true;
            while (keepLooping)
            {
                AskQuestion();
                var possibleAnswers = GetPossibleAnswers();
                Console.WriteLine("Type the number corresponding to the correct answer below.");
                PrintPossibleAnswers(possibleAnswers);

                if (!Int32.TryParse(Console.ReadLine(), out var choice))
                {
                    keepLooping = continuationDeterminer.GoAgainWithInvalidChoiceMessage();
                    continue;
                }

                if (!possibleAnswers.TryGetValue(choice, out var answer))
                {
                    keepLooping = continuationDeterminer.GoAgainWithInvalidChoiceMessage("That isn't one of the choices.");
                    continue;
                }

                return $"You've been hit by a train {answer.ToLower()}.";
            }

            return null;
        }

        private void AskQuestion()
        {
            Console.WriteLine("How many times have you been hit by a train?");
        }

        private Dictionary<int, string> GetPossibleAnswers()
        {
            return new Dictionary<int, string>
            {
                {1, "Twice" },
                {2, "Five Times" },
                {3, "Nearly Six Times" }
            };
        }

        private void PrintPossibleAnswers(Dictionary<int, string> possibleAnswers)
        {
            foreach (var kv in possibleAnswers)
            {
                Console.WriteLine($"{kv.Key}. {kv.Value}");
            }
        }
    }
}
