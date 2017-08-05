using System;

namespace Builder
{
    [Flags]
    public enum Education
    {
        Rudimentary = 0b00000001,
        SelfTaught  = 0b00000010,
        University  = 0b00000100,
        Doctor      = 0b00001000
    }
}
