using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TheOrganicShop.Tools
{
    public static class HttpHelper
    {
        public static HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("authorization", "Basic T3JnU2hvcE1vYmlsZVVzZXI6MTIzcXdl");
            return httpClient;
        }
    }
}
