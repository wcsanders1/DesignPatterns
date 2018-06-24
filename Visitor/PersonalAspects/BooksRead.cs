using CommonClientLib;

namespace Visitor.PersonalAspects
{
    public class BooksRead : IPersonalAspect
    {
        public int NumberOfBooksRead { get; private set; }
        public BookGenre FavoriteGenre { get; private set; }

        private static readonly QuestionAsker Asker = new QuestionAsker();
        private static readonly TypeParser TypParser = new TypeParser(new TextParser());

        public void SetAspect()
        {
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
