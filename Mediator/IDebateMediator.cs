namespace Mediator
{
    public interface IDebateMediator
    {
        void RegisterDebater(Debator debator);
        bool ArgumentIsSuitable(string argument);
    }
}
