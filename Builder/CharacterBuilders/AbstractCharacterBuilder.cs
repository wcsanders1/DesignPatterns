namespace Builder
{
    public abstract class AbstractCharacterBuilder
    {
        protected Character character;

        public AbstractCharacterBuilder()
        {
            character = new Character();
        }

        public Character GetCharacter()
        {
            return character;
        }

        public abstract void SetName();
        public abstract void DetermineMaterialSuccess();
        public abstract void DetermineSocialSuccess();
        public abstract void DetermineSpiritualSuccess();
    }
}
