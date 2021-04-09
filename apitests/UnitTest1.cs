using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace apitests
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly HttpClient client = new HttpClient();

        [TestMethod]
        public void getPrice()
        {
            WebRequest req = WebRequest.Create(@"https://localhost:44347/api/ebay/weather");
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            // Display the status.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
        }

        [TestMethod]
        public void postPrice()
        {


            var values = new Dictionary<string, string>
            {
                //{ "thing1", "hello" },
                //{ "thing2", "world" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = Task.Run(async() => await client.PostAsync("https://localhost:44347/api/ebay/weatherpost", content));

            // Get the stream containing content returned by the server.
            Stream dataStream = response.Result.Content.ReadAsStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
        }
    }
}
