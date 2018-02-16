using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaRestHarness
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // curl statement
            // $ curl -X POST -H "Content-Type: application/vnd.kafka.json.v2+json" --data '{"records":[{"value":{"name": "testUser"}}]}' "http://localhost:8082/topics/jsontest"
            var broker = "localhost";
            var port = "9092";
            var topic = "test-a";

            try
            {
                var client = new RestClient
                {
                    BaseUrl = new Uri(string.Format("http://{0}:{1}/topics/{2}", broker, port, topic))
                };
                //client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token.Data));

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/vnd.kafka.json.v2+json");
                request.AddJsonBody(new { id = 0, name = "testing from console app" });

                var response = await client.ExecuteTaskAsync<bool>(request, new CancellationTokenSource().Token).ConfigureAwait(false);
                if (response.Data)
                {
                    Console.WriteLine(response.Data);
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
