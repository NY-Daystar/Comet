using NLog;
using System;
using System.Net.Http;


namespace Comet
{
    internal class Api
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly string HOST = "https://gitlab.com";

        public Api() { }

        // TODO a faire
        public Api(string host) { }

        // TODO a faire en mieux 
        // TODO changer le nom
        // TODO to comment
        private static string readResponseFromUrl(string url)
        {
            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(url)
            };
            using (var response = httpClient.GetAsync(url))
            {
                string responseBody = response.Result.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
        }

        // TODO to comment 
        // ex: https://gitlab.com/api/v4/projects/36458835/wikis/
        public string GetWikiPages(int projectId)
        {
            string url = $"{HOST}/api/v4/projects/{projectId}/wikis/";
            Logger.Debug($"FetchWiki call API: {url}"); // TODO afficher ddans la console

            return readResponseFromUrl(url);
        }

        // TODO to comment 
        // ex: https://gitlab.com/api/v4/projects/36458835/wikis/home/
        public string GetPageContent(int projectId, string slug)
        {
            string url = $"{HOST}/api/v4/projects/{projectId}/wikis/{slug}?render_html=true";
            Logger.Info($"FetchWikiPage call API: {url}"); // TODO afficher ddans la console


            string response = readResponseFromUrl(url);

            //  TODO convertir en WikiPage
            //var pages = JsonConvert.DeserializeObject<List<WikiPage>>(response.Content);

            return response;
        }
    }
}
