namespace Builder.CharacterBuilders
{
    class OddCharacterBuilder : AbstractCharacterBuilder
    {
        public override void DetermineMaterialSuccess()
        {
            character.Education = Education.Doctor | Education.University | Education.SelfTaught;
            character.WorkEthic = WorkEthic.Industrious;
            character.IsWealthy = true;
        }

        public override void DetermineSocialSuccess()
        {
            character.Personality = Personality.Conscientious | Personality.Straightforward | Personality.Agreeable;
            character.Sociability = Sociability.Gregarious | Sociability.Introverted;
            character.IsMarried = true;
        }

        public override void DetermineSpiritualSuccess()
        {
            character.Religion = Religion.Catholic | Religion.MedicineMan;
            character.Hobby = Hobby.Golf | Hobby.Boating | Hobby.Astronomics | Hobby.Blasting;
            character.IsWise = false;
        }

        public override void SetName()
        {
            character.Name = "Odd";
        }
    }
}
