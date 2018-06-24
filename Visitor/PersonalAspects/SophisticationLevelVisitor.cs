using System;
using System.Collections.Generic;
using System.Linq;

namespace Visitor.PersonalAspects
{
    public class SophisticationLevelVisitor : IVisitor
    {
        private List<SophisticationLevel> SophisticationLevels { get; set; }

        public SophisticationLevelVisitor()
        {
            SophisticationLevels = new List<SophisticationLevel>();
        }

        public void Visit(BooksRead booksRead)
        {
            if (booksRead.NumberOfBooksRead < 5)
            {
                SophisticationLevels.Add(SophisticationLevel.Low);
            }
            else if (booksRead.NumberOfBooksRead < 50)
            {
                SophisticationLevels.Add(SophisticationLevel.Moderate);
            }
            else
            {
                SophisticationLevels.Add(SophisticationLevel.High);
            }

            switch (booksRead.FavoriteGenre)
            {
                case BookGenre.Silliness:
                    SophisticationLevels.Add(SophisticationLevel.Low);
                    break;
                case BookGenre.Horror:
                case BookGenre.SciFi:
                    SophisticationLevels.Add(SophisticationLevel.Moderate);
                    break;
                case BookGenre.History:
                case BookGenre.Literature:
                    SophisticationLevels.Add(SophisticationLevel.High);
                    break;
                default:
                    SophisticationLevels.Add(SophisticationLevel.Low);
                    break;
            }
        }

        public void Visit(Education education)
        {
            if (education.GPA < 2)
            {
                SophisticationLevels.Add(SophisticationLevel.Low);
            }
            else if (education.GPA < 3)
            {
                SophisticationLevels.Add(SophisticationLevel.Moderate);
            }
            else
            {
                SophisticationLevels.Add(SophisticationLevel.High);
            }

            switch (education.EducationLevel)
            {
                case EducationLevel.HighSchool:
                    SophisticationLevels.Add(SophisticationLevel.Low);
                    break;
                case EducationLevel.College:
                    SophisticationLevels.Add(SophisticationLevel.Moderate);
                    break;
                case EducationLevel.PostGraduate:
                    SophisticationLevels.Add(SophisticationLevel.High);
                    break;
                default:
                    SophisticationLevels.Add(SophisticationLevel.Low);
                    break;
            }
        }

        public void Visit(TravelExperience travelExperience)
        {
            if (travelExperience.NumberOfCountriesVisited < 2 && 
                travelExperience.NumberOfMonthsAbroad < 2)
            {
                SophisticationLevels.Add(SophisticationLevel.Low);
            }
            else if (travelExperience.NumberOfCountriesVisited < 5 &&
                travelExperience.NumberOfMonthsAbroad < 5)
            {
                SophisticationLevels.Add(SophisticationLevel.Moderate);
            }
            else
            {
                SophisticationLevels.Add(SophisticationLevel.High);
            }
        }

        public SophisticationLevel GetSophisticationLevel()
        {
            return (SophisticationLevel)
                (SophisticationLevels.Select(level => (int)level).Sum() / SophisticationLevels.Count);
        }
    }
}
