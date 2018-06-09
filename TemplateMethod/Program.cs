using CommonClientLib;
using System;
using TemplateMethod.HiringProcesses;

namespace Strategy
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();        
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker Asker = new QuestionAsker();
        private static TypeParser TypParser = new TypeParser(new TextParser());

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE INTERPRETER METHOD PROGRAM -- WHICH MAY PROVIDE SOME INTEREST");
            var (departmentDictionary, departmentList) = TypParser.GetInstantiatedTypeDictionaryAndNameList<AbstractHiringProcess>();

            while (true)
            {
                var chosenIndex = Asker.GetChoiceFromList("For what department do you want to apply for a job?", departmentList);
                var chosenDepartment = departmentDictionary[++chosenIndex];
                chosenDepartment.ExecuteHiringProcess();

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}