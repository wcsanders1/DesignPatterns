using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Adapter.PersonalInformation
{
    public abstract class AbstractPersonalInformation
    {
        public string Instruction { get; } = "Type the number corresponding to the correct answer below.";

        private readonly ContinuationDeterminer continuationDeterminer;

        public AbstractPersonalInformation(ContinuationDeterminer continuationDeterminer)
        {
            this.continuationDeterminer = continuationDeterminer;
        }

        protected bool GetChoice(Dictionary<int, string> possibleAnswers, out string choice, out int tempChoice)
        {
            if (!Int32.TryParse(Console.ReadLine(), out tempChoice))
            {
                choice = null;
                return continuationDeterminer.GoAgainWithInvalidChoiceMessage();
            }

            if (!possibleAnswers.TryGetValue(tempChoice, out choice))
            {
                return continuationDeterminer.GoAgainWithInvalidChoiceMessage("That isn't one of the choices.");
            }
            
            return true;
        }

        protected virtual void PrintPossibleAnswers(Dictionary<int, string> possibleAnswers)
        {
            foreach (var kv in possibleAnswers)
            {
                Console.WriteLine($"{kv.Key}. {kv.Value}");
            }
        }
    }
}
