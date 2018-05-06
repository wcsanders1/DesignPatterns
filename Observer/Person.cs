using System;
using System.Collections.Generic;
using System.Linq;

namespace Observer
{
    public class Person : IObserver<News>
    {
        public string Name { get; set; }

        private List<Response> Responses { get; }
        private Random Random { get; }
        private IDisposable Cancellation;

        public Person(string name, List<Response> responses)
        {
            Name = name;
            Random = new Random();
            Responses = responses;
        }

        public void Subscribe(NewsPublisher provider)
        {
            Cancellation = provider.Subscribe(this);
        }

        public void OnNext(News news)
        {
            Console.WriteLine($"{Name}'s response to '{news.NewsItem}':");
            Console.ForegroundColor = ConsoleColor.DarkRed;

            var responseChoices = Responses.Where(r => r.NewsType == news.NewsType).ToList();

            Console.WriteLine($"{GetRandomResponse(responseChoices).ResponseMsg}\n");

            Console.ResetColor();
        }

        public void Unsubscribe()
        {
            Cancellation.Dispose();
        }

        private Response GetRandomResponse(List<Response> responses)
        {
            return responses[Random.Next(0, responses.Count)];
        }

        #region NotImplemented
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
