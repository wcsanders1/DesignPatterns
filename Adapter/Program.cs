﻿using System;
using CommonClientLib;
using System.Collections.Generic;
using Adapter.PersonalInformation;
using Adapter.Renderers;
using System.IO;
using Newtonsoft.Json;

namespace Adapter
{
    class Program
    {
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static TextParser TextParser                         = new TextParser();
        private static TypeParser TypeParser                         = new TypeParser(TextParser);
        private const string ConfigurableInformationGettersPath      = "PersonalInformation/ConfigurableQuestionsAndAnswers.json";

        static void Main(string[] args)
        {
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                  WELCOME TO THE ADAPTER PROGRAM -- WHICH IS SORT OF A FUNNY PROGRAM");
            Console.WriteLine("**********************************************************************************************************\n");

            List<InfoGetter> infoGetters;
            try
            {
                using (var reader = new StreamReader(ConfigurableInformationGettersPath))
                {
                    var json = reader.ReadToEnd();
                    infoGetters = JsonConvert.DeserializeObject<List<InfoGetter>>(json);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Unable to retrieve questions and answers from '{ConfigurableInformationGettersPath}'.");
                Console.WriteLine("Using default questions and answers instead.\n");
                infoGetters = DefaultInfoGetters.GetDefaultInfoGetters();
            }
            
            var questionsAndAnswers = QuestionAndAnswerGetter.GetQuestionsAndAnswers(infoGetters);

            if (questionsAndAnswers == null)
            {
                return;
            }

            var choice = GetRenderer();
            if (choice == 0)
            {
                return;
            }

            switch (choice)
            {
                case Renderer.EvaluationRenderer:
                    var evaluationRenderer = new EvaluationRenderer();
                    var (results, score) = evaluationRenderer.ListTopicsAndScores(questionsAndAnswers);
                    Console.WriteLine(results);
                    Console.WriteLine($"You got a total of {score} questions correct.");
                    break;
                case Renderer.QuestionAndAnswerRenderer:
                    var questionAndAnswerRenderer = new QuestionAndAnswerRenderer();
                    Console.WriteLine(questionAndAnswerRenderer.ListQuestionsAndAnswers(questionsAndAnswers));
                    break;
            }

            Console.ReadKey();
        }

        private static Renderer GetRenderer()
        {
            while (true)
            {
                Console.WriteLine("Press 1 if you want your answers evaluated for correctness, or press 2 if you merely want them listed.");

                if (!Int32.TryParse(Console.ReadLine(), out var choice))
                {
                    if (!ContinuationDeterminer.GoAgainWithInvalidChoiceMessage())
                    {
                        return 0;
                    }
                    continue;
                }

                if (!Enum.IsDefined(typeof(Renderer), choice))
                {
                    if (!ContinuationDeterminer.GoAgainWithInvalidChoiceMessage())
                    {
                        return 0;
                    }
                    continue;
                }

                return (Renderer)choice;
            }
        }
    }
}
