using System.Collections.Generic;

namespace Composite
{
    public class Decedent
    {
        public string Name { get; }

        private decimal EstateValue { get; }
        public List<Descendant> Descendants { get; set; } = new List<Descendant>();

        public Decedent(string name, decimal estateValue)
        {
            Name = name;
            EstateValue = estateValue;
        }

        public void DistributeEstate()
        {
            DistributeEstate(Descendants, EstateValue);
        }

        private void DistributeEstate(List<Descendant> descendants, decimal remainingShare)
        {
            var share = remainingShare / descendants.Count;
            descendants.ForEach(descendant =>
            {
                if (descendant.Deceased && 
                    descendant.Descendants != null && 
                    descendant.Descendants.Count > 0)
                {
                    DistributeEstate(descendant.Descendants, share);
                }
                else
                {
                    descendant.Inheritance = share;
                }
            });
        }
    }
}
