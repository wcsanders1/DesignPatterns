﻿using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Adapter.PersonalInformation
{
    public class HitByTrain : AbstractPersonalInformation, IPersonalInformationGettable
    {
        public string QuestionTopic { get; } = "Times Hit By Train";

        private readonly ContinuationDeterminer continuationDeterminer;

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

                if (!GetChoice(possibleAnswers, out var answer))
                {
                    return null;
                }

                if (answer != null)
                {
                    return $"You've been hit by a train {answer.ToLower()}.";
                }
            }
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
    }
}
