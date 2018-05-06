using System;
using System.Collections.Generic;

namespace Observer
{
    internal class Unsubscriber<News> : IDisposable
    {
        private List<IObserver<News>> Observers;
        private IObserver<News> Observer;

        internal Unsubscriber(List<IObserver<News>> observers, IObserver<News> observer)
        {
            Observers = observers;
            Observer = observer;
        }

        public void Dispose()
        {
            if (Observers.Contains(Observer))
            {
                Observers.Remove(Observer);
            }
        }
    }
}