using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Adapter.PersonalInformation
{
    public abstract class AbstractPersonalInformation
    {
        private readonly ContinuationDeterminer continuationDeterminer;

        public AbstractPersonalInformation(ContinuationDeterminer continuationDeterminer)
        {
            this.continuationDeterminer = continuationDeterminer;
        }
        protected bool GetChoice(Dictionary<int, string> possibleAnswers, out string choice)
        {
            if (!Int32.TryParse(Console.ReadLine(), out var tempChoice))
            {
                choice = null;
                return continuationDeterminer.GoAgainWithInvalidChoiceMessage();
            }

            if (!possibleAnswers.TryGetValue(tempChoice, out choice))
            {
                choice = null;
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
