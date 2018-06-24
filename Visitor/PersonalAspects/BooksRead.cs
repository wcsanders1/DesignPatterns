using CommonClientLib;
using System;

namespace Visitor.PersonalAspects
{
    public class BooksRead : IPersonalAspect
    {
        public int NumberOfBooksRead { get; private set; }
        public BookGenre FavoriteGenre { get; private set; }

        private static readonly QuestionAsker Asker = new QuestionAsker();
        private static readonly TypeParser TypParser = new TypeParser(new TextParser());
        private static TextPrinter TxtPrinter = new TextPrinter();

        public void SetAspect()
        {
            TxtPrinter.PrintInformation("Now we'll get information regarding books you've read.", '-', ConsoleColor.DarkGreen);
            NumberOfBooksRead = Asker.GetValue<int>("How many books have you read?");
            FavoriteGenre = (BookGenre)(Asker.GetChoiceFromList("What is your favorite genre of book?",
                TypParser.GetEnumValuesList<BookGenre>()));
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
