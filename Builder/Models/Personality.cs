using System;

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
