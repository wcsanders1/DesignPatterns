using System;

namespace Builder
{
    [Flags]
    public enum Hobby
    {
        Golf        = 0b00000001,
        Boating     = 0b00000010,
        Astronomics = 0b00000100,
        Blasting    = 0b00001000
    }
}
