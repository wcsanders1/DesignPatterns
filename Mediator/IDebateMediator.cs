namespace Mediator
{
    public interface IDebateMediator
    {
        void RegisterDebator(Debator debator);
        bool ArgumentIsSuitable(string argument);
    }
}
