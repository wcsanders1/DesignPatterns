using CommonClientLib;
using System;
using System.Linq;
using Visitor.PersonalAspects;

namespace Visitor
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker Asker = new QuestionAsker();
        private static TypeParser TypParser = new TypeParser(new TextParser());

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE VISITOR PROGRAM -- WHICH MAY OFFER SOME CONSOLATION");

            while (true)
            {
                var personalAspects = TypParser.GetInstantiatedTypeDictionaryAndNameList<IPersonalAspect>()
                .Item1.Select(kv => kv.Value).ToList();
                var visitor = new SophisticationLevelVisitor();

                foreach (var aspect in personalAspects)
                {
                    aspect.SetAspect();
                    aspect.Accept(visitor);
                }

                Console.WriteLine($"Your level of sophistication is {visitor.GetSophisticationLevel()}.");

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
