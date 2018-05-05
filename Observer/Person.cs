using System;

namespace Observer
{
    public class Person : IObserver<News>
    {
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
