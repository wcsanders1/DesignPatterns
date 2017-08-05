namespace Builder
{
    public abstract class AbstractCharacterBuilder
    {
        protected Character character;

        public Character GetCharacter()
        {
            return character;
        }

        public abstract void DetermineMaterialSuccess();
        public abstract void DetermineSocialSuccess();
        public abstract void DetermineSpiritualSuccess();
    }
}
