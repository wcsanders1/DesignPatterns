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

        public void OnNext(News news)
        {
            Console.WriteLine($"{Name}'s response to '{news.NewsItem}':\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Hi!");
            Console.ResetColor();
        }

        public void Unsubscribe()
        {
            Cancellation.Dispose();
        }
    }
}
