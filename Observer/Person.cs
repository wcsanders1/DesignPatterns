using System;

namespace Observer
{
    public class Person : IObserver<News>
    {
        public string Name { get; set; }

        private IDisposable Cancellation;

        public Person(string name)
        {
            Name = name;
        }

        public void Subscribe(NewsPublisher provider)
        {
            Cancellation = provider.Subscribe(this);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(News value)
        {
            throw new NotImplementedException();
        }
    }
}
