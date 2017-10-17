using System.Collections.Generic;

namespace Composite
{
    public class Decedent
    {
        private decimal EstateValue { get; }
        private List<Descendant> Descendants { get; }

        public Decedent(decimal estateValue, List<Descendant> descendants)
        {
            EstateValue = estateValue;
            Descendants = descendants;
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
