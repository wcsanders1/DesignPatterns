using System.Collections.Generic;

namespace Composite
{
    public class Decedent
    {
        public decimal EstateValue { get; set; }
        public List<Descendant> Descendants { get; set; }

        public void DistributeEstate(List<Descendant> descendants, decimal remainingShare)
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
