using Prototype.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prototype
{
    public class WebPageExplorer
    {
        private HttpClient Client { get; }
        private string Url { get; }

        public WebPageExplorer(string url)
        {
            Client = new HttpClient();
            Url    = $"http://www.{url}";
        }

        public async Task<(HttpResponseMessage, ErrorMessage)> GetInformationAsync()
        {
            HttpResponseMessage response;
            try
            {
                response = await Client.GetAsync(Url).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return (null, new ErrorMessage
                {
                    Message    = $"Unable to get information for the url {Url}",
                    Exception  = ex,
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }

            return (response, null);
        }
    }
}
