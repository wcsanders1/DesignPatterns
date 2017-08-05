using System;

namespace Builder
{
    [Flags]
    public enum Sociability
    {
        Gregarious  = 0b00000001,
        Introverted = 0b00000010,
        Shy         = 0b00000100,
        Overbearing = 0b00001000
    }
}
