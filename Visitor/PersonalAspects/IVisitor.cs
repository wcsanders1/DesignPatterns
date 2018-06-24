namespace Visitor.PersonalAspects
{
    public interface IVisitor
    {
        void Visit(BooksRead booksRead);
        void Visit(Education education);
        void Visit(TravelExperience travelExperience);
    }
}
