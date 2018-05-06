using Newtonsoft.Json.Linq;

namespace Observer.Extensions
{
    public static class JTokenExtensions
    {
        public static News ToNews(this JToken jToken, NewsType newsType)
        {
            return new News
            {
                NewsType = newsType,
                NewsItem = jToken.Value<string>() ?? string.Empty
            };
        }
    }
}
