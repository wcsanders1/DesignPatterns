using System;
using System.Collections.Generic;
using System.Linq;

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

        public void PublishNews(News news)
        {
            if (!Observers.Any())
            {
                Console.WriteLine("There are no subscribers to the news.");

                return;
            }

            foreach (var observer in Observers)
            {
                observer.OnNext(news);
            }
        }
    }
}
