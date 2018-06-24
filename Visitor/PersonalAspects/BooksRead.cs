namespace Visitor.PersonalAspects
{
    public class BooksRead : IPersonalAspect
    {
        public int NumberOfBooksRead { get; set; }
        public BookGenre FavoriteGenre { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
