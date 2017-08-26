using System.Collections.Generic;

namespace Singleton.Topics
{
    public interface IArguable
    {
        string Topic { get; }
        List<Argument> ForArguments { get; }
        List<Argument> AgainstArguments { get; }
    }
}
