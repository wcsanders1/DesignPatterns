using CommonClientLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChainOfResponsibility
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker Asker = new QuestionAsker();
        private const string ConfigurableQuestionsPath = "Questions.json";

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE CHAIN OF RESPONSIBILITY PROGRAM -- WHICH IS KIND OF A SILLY PROGRAM");

            while (true)
            {
                List<Question> questions;
                try
                {
                    using (var reader = new StreamReader(ConfigurableQuestionsPath))
                    {
                        var json = reader.ReadToEnd();
                        questions = JsonConvert.DeserializeObject<List<Question>>(json);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to retrieve questions and answers from '{ConfigurableQuestionsPath}'.\n" +
                                      $"Exception message: {ex.Message}\n" +
                                      $"Using default questions and answers instead.\n");

                    questions = DefaultQuestions.GetDefaultQuestions();
                }

                var question = questions[Asker.GetChoiceFromList("Which question do you want to ask the clergy?",
                    questions.Select(q => q.Query).ToList())];

                var priest = new QuestionHandler(new Clergyman("priest", DegreeOfPhilosophicalDepth.Low));
                var bishop = new QuestionHandler(new Clergyman("bishop", DegreeOfPhilosophicalDepth.Medium));
                var archibishop = new QuestionHandler(new Clergyman("archbishop", DegreeOfPhilosophicalDepth.High));
                var pope = new QuestionHandler(new Clergyman("pope", DegreeOfPhilosophicalDepth.Extreme));

                priest.RegisterNext(bishop);
                bishop.RegisterNext(archibishop);
                archibishop.RegisterNext(pope);

                priest.AnswerQuestion(question);

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
