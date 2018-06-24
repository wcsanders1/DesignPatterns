using CommonClientLib;
using System;

namespace Visitor.PersonalAspects
{
    public class Education : IPersonalAspect
    {
        public EducationLevel EducationLevel { get; private set; }
        public decimal GPA { get; private set; }

        private static readonly QuestionAsker Asker = new QuestionAsker();
        private static readonly TypeParser TypParser = new TypeParser(new TextParser());
        private static TextPrinter TxtPrinter = new TextPrinter();

        public void SetAspect()
        {
            TxtPrinter.PrintInformation("Now we'll get information regarding your education.", '-', ConsoleColor.DarkGreen);
            EducationLevel = (EducationLevel)(Asker.GetChoiceFromList("What is the highest level of education you've attained?",
                TypParser.GetEnumValuesList<EducationLevel>()));
            GPA = Asker.GetValue<decimal>($"What was your average GPQ in {EducationLevel}?");
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
