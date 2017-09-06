using Autofac;
using Singleton.Topics;
using System.Collections.Generic;

namespace Singleton
{
    public class Container
    {
        public ContainerBuilder Builder { get; set; }

        public Container(ContainerBuilder builder)
        {
            Builder = builder;
        }

        public IContainer GetContainer(Dictionary<int, IArguable> topicDict)
        {
            foreach (var key in topicDict.Keys)
            {
                var topic = topicDict[key];

                Builder.Register(c => topic)
                    .As<IArguable>()
                    .Keyed<IArguable>(key)
                    .SingleInstance()
                    .AsSelf();
            }

            return Builder.Build();
        }
    }
}
