namespace Builder.CharacterBuilders
{
    class ComplacentCharacterBuilder : AbstractCharacterBuilder
    {
        public override void DetermineMaterialSuccess()
        {
            character.WorkEthic = WorkEthic.Apathetic;
            character.Education = Education.Rudimentary | Education.SelfTaught;
            character.IsWealthy = false;
        }

        public override void DetermineSocialSuccess()
        {
            character.Sociability = Sociability.Introverted;
            character.Personality = Personality.Agreeable;
            character.IsMarried   = true;
        }

        public override void DetermineSpiritualSuccess()
        {
            character.Religion = Religion.Protestant;
            character.Hobbies  = Hobbies.Boating | Hobbies.Golf;
            character.IsWise   = false;
        }

        public override void SetName()
        {
            character.Name = "Complacent";
        }
    }
}
