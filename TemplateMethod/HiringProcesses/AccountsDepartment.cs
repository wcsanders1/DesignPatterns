using System;
using System.Collections.Generic;

namespace TemplateMethod.HiringProcesses
{
    public class AccountsDepartment : AbstractHiringProcess
    {
        protected override void AdministerExam()
        {
            Asker.GetChoiceFromList("What is your favorite account?", new List<string>
            {
                "Rolling",
                "Royal",
                "Imminent"
            });
        }

        protected override void ConductInterview()
        {
            Asker.GetValue<string>("If you had an account that was messy, how would you clean it up?");
            Asker.GetValue<string>("What is the neatest account you ever worked with?");
        }

        protected override string GetContactInformation()
        {
            var name = base.GetContactInformation();
            Asker.GetValue<long>("What is your account number?");

            return name;
        }

        protected override void GivePartingWords(string name)
        {
            base.GivePartingWords(name);
            Console.WriteLine("In the meantime, we urge you to keep toiling in the fecund soils of accounts!");
        }
    }
}
