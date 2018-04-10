namespace Mediator
{
    public interface IDebateMediator
    {
        void RegisterDebater(AbstractDebator debator);
        void ReceiveProposition();
    }
}
