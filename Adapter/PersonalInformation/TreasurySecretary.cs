using System;

namespace Adapter.PersonalInformation
{
    public class TreasurySecretary : IPersonalInformationGettable
    {
        public string QuestionTopic { get; } = "Favorite Treasury Secretary";

        public string GetAnswer()
        {
            throw new NotImplementedException();
        }
    }
}
