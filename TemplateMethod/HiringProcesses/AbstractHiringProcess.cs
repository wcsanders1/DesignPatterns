using CommonClientLib;
using System;

namespace TemplateMethod.HiringProcesses
{
    public abstract class AbstractHiringProcess
    {
        protected static QuestionAsker Asker = new QuestionAsker();

        public void ExecuteHiringProcess()
        {
            Console.WriteLine("First, we must get your contact information");
            var name = GetContactInformation();

            Console.WriteLine("Thank you for providing your contact information. Next, please take " +
                "a short exam that we will use to help determine your qualifications.");
            AdministerExam();

            Console.WriteLine("Thank you for taking the exam. Now, it's time for an interview.");
            ConductInterview();
            GivePartingWords(name);
        }

        protected virtual string GetContactInformation()
        {
            var firstName = Asker.GetValue<string>("What is your first name?");
            var lastName = Asker.GetValue<string>("What is your last name?");
            var address = Asker.GetValue<string>($"Hello {firstName} {lastName}. What is your address?");

            Console.WriteLine($"You've provided the following information:\n" +
                $"First name: {firstName}\n" +
                $"Last name: {lastName}\n" +
                $"Address: {address}");

            return $"{firstName} {lastName}";
        }

        protected virtual void GivePartingWords(string name)
        {
            Console.WriteLine($"Thank you for taking part in the hiring process {name}. " +
                $"We might get back to you someday about things.");
        }

        protected abstract void AdministerExam();
        protected abstract void ConductInterview();
    }
}