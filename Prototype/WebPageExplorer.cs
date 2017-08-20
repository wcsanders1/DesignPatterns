using Prototype.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prototype
{
    public class WebPageExplorer
    {
        private string _url;

        private HttpClient Client { get; }
        private string Url
        {
            get
            {
                return _url;
            }
            
            set
            {
                _url = $"http://www.{value}";
            }
        }

        public string CurrentUrl
        {
            get
            {
                return _url;
            }
        }

        public WebPageExplorer(string url)
        {
            Client = new HttpClient();
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

        public WebPageExplorer Clone(string url)
        {
            var clone = MemberwiseClone() as WebPageExplorer;
            clone.Url = url;

            return clone;
        }
    }
}
