using System;
using System.Collections.Generic;

namespace Observer
{
    public class NewsPublisher : IObservable<News>
    {
        private List<IObserver<News>> Observers;

        public NewsPublisher()
        {
            Observers = new List<IObserver<News>>();
        }

        public IDisposable Subscribe(IObserver<News> observer)
        {
            if (!Observers.Contains(observer))
            {
                Observers.Add(observer);
            }

            return new Unsubscriber<News>(Observers, observer);
        }
    }
}
