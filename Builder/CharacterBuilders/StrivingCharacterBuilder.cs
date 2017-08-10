namespace Builder.CharacterBuilders
{
    class StrivingCharacterBuilder : AbstractCharacterBuilder
    {
        public override void DetermineMaterialSuccess()
        {
            character.Education = Education.SelfTaught | Education.University;
            character.WorkEthic = WorkEthic.Industrious;
            character.IsWealthy = true;
        }

        public override void DetermineSocialSuccess()
        {
            character.Personality = Personality.Agreeable | Personality.Straightforward;
            character.Sociability = Sociability.Introverted | Sociability.Gregarious;
            character.IsMarried   = true;
        }

        public override void DetermineSpiritualSuccess()
        {
            character.Religion = Religion.Buddhist | Religion.Catholic;
            character.Hobby  = Hobby.Golf;
            character.IsWise   = true;
        }

        public override void SetName()
        {
            character.Name = "Striving";
        }
    }
}
