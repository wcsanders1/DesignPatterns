using System;

namespace Flyweight
{
    public interface ICharacter
    {
        ConsoleColor ForegroundColor { get; }
        ConsoleColor BackgroundColor { get; }
        void Render(int YPosition);
    }
}
