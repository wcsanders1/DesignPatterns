using System.Collections.Generic;

namespace Composite
{
    public class Decedent
    {
        public decimal EstateValue { get; set; }
        public List<Descendant> Descendants { get; set; }
    }
}
