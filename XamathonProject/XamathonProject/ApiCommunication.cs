using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamathonProject
{
    class ApiCommunication
    {

        private const string BaseUrl = "https://westus.api.cognitive.microsoft.com/";
        private const string AccountKey = "Your api key";

        private const int NumLanguages = 1;
        private static string response;
        private static string jsonVersion;
        private static JObject components;
        private static JToken test;
        private static Stack<string> wishesToSend = new Stack<string>();
        private static JToken helper;
        private static string helperConvertedToString;

        public static async void MakeRequests(String letter, String name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                letter = letter.Replace('\n', ' ');

                string[] wishes = letter.Split('\r');
                jsonVersion = "{\"documents\":[";

                for (int i = 0; i < wishes.Length; i++)
                    if (!wishes[i].Equals(" "))
                    {
                        jsonVersion = jsonVersion + "{\"id\":\"" + i + "\",\"text\":\""
                                                  + wishes[i] + "\"},";
                    }

                jsonVersion = jsonVersion + "]}";

                byte[] byteData = Encoding.UTF8.GetBytes(jsonVersion);
                var uri = "text/analytics/v2.0/keyPhrases";
                response = await CallEndpoint(client, uri, byteData);

                components = JObject.Parse(response);
                test = components["documents"];
                char[] toRemove = { ' ', '[', ']', '\r', '\n', '\\', '"', '\'' };

                foreach (var testComponent in test)
                {
                    helper = testComponent["keyPhrases"];

                    if (helper != null)
                    {
                        helperConvertedToString = helper.ToString().Trim(toRemove);
                        wishesToSend.Push(helperConvertedToString.Replace(" ", "%20"));
                    }
                }
                EmailService.SendEmail(name, wishesToSend);
            }
        }

        public static void SendLogInData(String name, String email, String association)
        {
            EmailService.SetDirectorEmail(email);
        }

        static async Task<String> CallEndpoint(HttpClient client, string uri, byte[] byteData)
        {
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(uri, content);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}