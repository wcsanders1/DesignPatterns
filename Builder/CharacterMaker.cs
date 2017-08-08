namespace Builder
{
    public static class CharacterMaker
    {
        public static Character GetCharacter(AbstractCharacterBuilder builder)
        {
            builder.SetName();
            builder.DetermineMaterialSuccess();
            builder.DetermineSocialSuccess();
            builder.DetermineSpiritualSuccess();
            return builder.GetCharacter();
        }
    }
}
