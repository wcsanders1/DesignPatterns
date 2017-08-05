namespace Builder
{
    class EccentricCharacterBuilder : AbstractCharacterBuilder
    {
        public override void DetermineMaterialSuccess()
        {
            character.Education = Education.SelfTaught;
            character.WorkEthic = WorkEthic.Indifferent;
            character.IsWealthy = true;
        }

        public override void DetermineSocialSuccess()
        {
            character.Personality = Personality.Neurotic;
            character.Sociability = Sociability.Gregarious | Sociability.Overbearing;
            character.IsMarried = false;
        }

        public override void DetermineSpiritualSuccess()
        {
            character.Religion = Religion.MedicineMan;
            character.Hobbies = Hobbies.Astronomics | Hobbies.Blasting;
            character.IsWise = false;
        }
    }
}
