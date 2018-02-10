using System;

namespace Flyweight
{
    public interface ICharacter
    {
        char ASCIICharacter { get; }
        ConsoleColor ForegroundColor { get; }
        ConsoleColor BackgroundColor { get; }
        void Render(int YPosition);
    }
}
