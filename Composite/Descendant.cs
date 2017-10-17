using System.Collections.Generic;

namespace Composite
{
    public class Descendant
    {
        public string Name { get; set; }
        public decimal Inheritance { get; set; }
        public bool Deceased { get; set; }
        public List<Descendant> Descendants { get; set; }
    }
}
