namespace Builder.CharacterBuilders
{
    class RealisticCharacterBuilder : AbstractCharacterBuilder
    {
        public override void DetermineMaterialSuccess()
        {
            character.Education = Education.Doctor | Education.University;
            character.WorkEthic = WorkEthic.Industrious;
            character.IsWealthy = true;
        }

        public override void DetermineSocialSuccess()
        {
            character.Personality = Personality.Conscientious | Personality.Straightforward;
            character.Sociability = Sociability.Gregarious;
            character.IsMarried   = true;
        }

        public override void DetermineSpiritualSuccess()
        {
            character.Religion = Religion.Catholic | Religion.MedicineMan;
            character.Hobby  = Hobby.Golf | Hobby.Boating;
            character.IsWise   = false;
        }

        public override void SetName()
        {
            character.Name = "Realistic";
        }
    }
}
