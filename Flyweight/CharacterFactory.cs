using System;
using System.Collections.Generic;

namespace Flyweight
{
    public class CharacterFactory
    {
        private Dictionary<char, ICharacter> Characters { get; set; }

        public CharacterFactory()
        {
            Characters = new Dictionary<char, ICharacter>();
        }

        public Character SetCharacter(char asciiCharacter, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            var newChar = new Character(asciiCharacter, foregroundColor, backgroundColor);
            Characters.Add(asciiCharacter, newChar);

            return newChar;
        }

        public ICharacter GetCharacter(char character)
        {
            if (!Characters.TryGetValue(character, out var createdChar))
            {
                return SetCharacter(character, Console.ForegroundColor, Console.BackgroundColor);
            }

            return createdChar;
        }
    }
}
