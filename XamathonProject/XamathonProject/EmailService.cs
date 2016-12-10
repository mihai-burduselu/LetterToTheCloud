using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XamathonProject
{
    public class EmailService
    {
        private const string MarketUrl = "https://www.amazon.com/s/ref=nb_sb_noss?url=search-alias%3Daps&field-keywords=";
        private static string GetEmailContent(string childName, Stack<string> wishes)
        {
            string result = "Hello! <br>" + childName + " has some wishes for Christmas and you can check some offerts bellow.<br>";

            while (wishes.Count != 0)
            {
                string keyword = wishes.Pop();
                string link = MarketUrl + keyword;
                result += "<br><br>" + link;
            }
            result += "<br><br>Don't forget to make someone happy :) .";
            return result;
        }
        public static async void SendEmail(string childName, Stack<string> wishes)
        {
            using (var client = new HttpClient())
            {
                string emailContent = GetEmailContent(childName, wishes);
                var values = new Dictionary<string, string>
                {
                   { "ToEmailAddress", "director@wishes.com" },
                   { "FromEmailAddress", "child@wishes.com" },
                   { "Content", emailContent},
                   {"Subject", "Wishes list" }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("http://emailwebapixamathon.azurewebsites.net/api/email/SendEmail", content);
                var responseString = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
