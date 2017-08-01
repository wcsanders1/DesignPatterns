using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class CharacterMaker
    {
        private readonly CharacterBuilder _builder;

        public CharacterMaker(CharacterBuilder builder)
        {
            _builder = builder;
        }

        public void BuildCharacter()
        {

        }

        public Character GetCharacter()
        {
            return _builder.GetCharacter();
        }
    }
}
