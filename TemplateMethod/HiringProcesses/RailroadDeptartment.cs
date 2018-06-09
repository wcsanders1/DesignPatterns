using System.Collections.Generic;

namespace TemplateMethod.HiringProcesses
{
    public class RailroadDeptartment : AbstractHiringProcess
    {
        protected override void AdministerExam()
        {
            Asker.GetChoiceFromList("If you're driving a train 1000 mph and you hit a cow, what will happen?", new List<string>
            {
                "The cow will be angry",
                "The trail will explode",
                "You will rejoice",
                "All of the above"
            });
        }

        protected override void ConductInterview()
        {
            Asker.GetValue<string>("What are your strengths with trains?");
            Asker.GetValue<string>("Did you ever drive a train?");
        }
    }
}
