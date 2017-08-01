using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    [Flags]
    public enum Personality
    {
        Agreeable     = 0b00000001,
        Neurotic      = 0b00000010,
        Conscientious = 0b00000100,
        Extroverted   = 0b00001000
    }
}
