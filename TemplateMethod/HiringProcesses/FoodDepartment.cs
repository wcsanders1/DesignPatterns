using System.Collections.Generic;

namespace TemplateMethod.HiringProcesses
{
    public class FoodDepartment : AbstractHiringProcess
    {
        protected override void AdministerExam()
        {
            Asker.GetChoiceFromList("If you need to provide food for ovr 7 people, what is a good food to provide?", new List<string>
            {
                "Wheat",
                "Dark Food",
                "Chewy"
            });
        }

        protected override void ConductInterview()
        {
            Asker.GetValue<string>("What food do you eat in a day?");
            Asker.GetValue<string>("What do you think of large food?");
        }
    }
}
