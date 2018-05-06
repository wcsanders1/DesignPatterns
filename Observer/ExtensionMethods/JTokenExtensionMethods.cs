using Newtonsoft.Json.Linq;

namespace Observer.Extensions
{
    public static class JTokenExtensionMethods
    {
        public static News ToNews(this JToken jToken, NewsType newsType)
        {
            return new News
            {
                NewsType = newsType,
                NewsItem = jToken.Value<string>() ?? string.Empty
            };
        }

        public static Response ToResponse(this JToken jToken, NewsType newsType)
        {
            return new Response
            {
                NewsType = newsType,
                ResponseMsg = jToken.Value<string>() ?? string.Empty
            };
        }
    }
}
