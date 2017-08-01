using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public abstract class CharacterBuilder
    {
        protected Character character;

        public Character GetCharacter()
        {
            return character;
        }

        public void CreateNewCharacter()
        {
            character = new Character();
        }


    }
}
