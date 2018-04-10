namespace Mediator
{
    public abstract class AbstractDebator
    {
        private readonly IDebateMediator DebateMediator;

        public AbstractDebator(IDebateMediator debateMediator)
        {
            DebateMediator = debateMediator;
        }
    }
}
