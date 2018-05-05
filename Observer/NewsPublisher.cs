using System;

namespace Observer
{
    public class NewsPublisher : IObservable<News>
    {
        public IDisposable Subscribe(IObserver<News> observer)
        {
            throw new NotImplementedException();
        }
    }
}
