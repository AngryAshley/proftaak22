using System;
//API
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;

namespace CSHttpClientSample
{
    static class Program
    {
        static void Main()
        {
            MakeRequest();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "aec6b0eaaa984650838bc80");

            var uri = "https://gateway.apiportal.ns.nl/virtual-train-api/api/v1/ingekort?" + queryString;

            var response = await client.GetAsync(uri);

            Console.WriteLine(response);
        }
    }
}