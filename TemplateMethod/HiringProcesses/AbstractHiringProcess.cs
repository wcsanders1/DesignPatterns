using CommonClientLib;
using System;

namespace TemplateMethod.HiringProcesses
{
    public abstract class AbstractHiringProcess
    {
        private static QuestionAsker Asker = new QuestionAsker();

        public void ExecuteHiringProcess()
        {
            var name = GetContactInformation();
            AdministerExam();
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
            Console.WriteLine($"Thank you for taking part in the interview process {name}. " +
                $"We might get back to you someday about things.");
        }

        protected abstract void AdministerExam();
        protected abstract void ConductInterview();
    }
}