using System.Collections.Generic;
using Visitor.PersonalAspects;

namespace Visitor
{
    public class Person
    {
        private List<IPersonalAspect> PersonalAspects { get; set; }

        public Person()
        {
            PersonalAspects = new List<IPersonalAspect>();
        }

        public void AddPersonalAspect(IPersonalAspect personalAspect)
        {
            if (personalAspect != null)
            {
                PersonalAspects.Add(personalAspect);
            }
        }

        public void Accept(IVisitor visitor)
        {
            if (PersonalAspects != null && PersonalAspects.Count != 0)
            {
                foreach (var aspect in PersonalAspects)
                {
                    aspect.Accept(visitor);
                }
            }
        }
    }
}
