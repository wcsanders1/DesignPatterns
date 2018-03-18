using System.Collections.Generic;

namespace ChainOfResponsibility
{
    public static class DefaultQuestions
    {
        public static List<Question> GetDefaultQuestions()
        {
            return new List<Question>
            {
                new Question
                {
                    Query = "Where is my car?",
                    Answer = "Your car is in the bowels of the earth.",
                    PhilosophicalDepth = DegreeOfPhilosophicalDepth.Low
                },
                new Question
                {
                    Query = "Are beasts good at keeping secrets?",
                    Answer = "Beasts are good at keeping secrets, unless you look them in the eye.",
                    PhilosophicalDepth = DegreeOfPhilosophicalDepth.Medium
                },
                new Question
                {
                    Query = "What card game would I be good at?",
                    Answer = "You would be good at nearly any card game that doesn't require eveningwear.",
                    PhilosophicalDepth = DegreeOfPhilosophicalDepth.High
                },
                new Question
                {
                    Query = "Why doesn't God like me?",
                    Answer = "It's not that God doesn't like you. He's just shy and doesn't know you very well.",
                    PhilosophicalDepth = DegreeOfPhilosophicalDepth.Extreme
                },
                new Question
                {
                    Query = "How does light go so fast?",
                    Answer = "Light goes so fast because it's very light.",
                    PhilosophicalDepth = DegreeOfPhilosophicalDepth.Excessive
                }
            };
        }
    }
}
