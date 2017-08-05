using System;

namespace Builder
{
    [Flags]
    public enum WorkEthic
    {
        Industrious = 0b00000001,
        Apathetic   = 0b00000010,
        Lazy        = 0b00000100,
        Indifferent = 0b00001000
    }
}