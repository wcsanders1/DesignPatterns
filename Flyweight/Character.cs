using System;

namespace Flyweight
{
    public class Character : ICharacter
    {
        public char ASCIICharacter { get; }
        public ConsoleColor ForegroundColor { get; }
        public ConsoleColor BackgroundColor { get; }

        public Character(char asciiCharacter, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            ASCIICharacter = asciiCharacter;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public void Render(int yPosition)
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.CursorTop = yPosition;
            Console.Write(ASCIICharacter);
            Console.ResetColor();
        }
    }
}
