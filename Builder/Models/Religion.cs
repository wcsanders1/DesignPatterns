using System;

namespace Builder
{
    [Flags]
    public enum Religion
    {
        Catholic    = 0b00000001,
        Protestant  = 0b00000010,
        Buddhist    = 0b00000100,
        MedicineMan = 0b00001000
    }
}
