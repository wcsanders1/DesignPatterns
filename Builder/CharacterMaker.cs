namespace Builder
{
    public class CharacterMaker
    {
        private readonly AbstractCharacterBuilder _builder;

        public CharacterMaker(AbstractCharacterBuilder builder)
        {
            _builder = builder;
        }

        public Character GetCharacter()
        {
            BuildCharacter();
            return _builder.GetCharacter();
        }

        private void BuildCharacter()
        {
            _builder.DetermineMaterialSuccess();
            _builder.DetermineSocialSuccess();
            _builder.DetermineSpiritualSuccess();
        }
    }
}
