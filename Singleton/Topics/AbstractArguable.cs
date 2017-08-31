using System.Collections.Generic;

namespace Singleton.Topics
{
    public abstract class AbstractArguable<T> : IArguable where T : new()
    {
        private static readonly T instance = new T();

        public static T Instance
        {
            get
            {
                return instance;
            }
        }

        public virtual string Topic => null;
        public virtual List<Argument> ForArguments => null;
        public virtual List<Argument> AgainstArguments => null;
    }
}
