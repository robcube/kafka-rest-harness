using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace KafkaRestHarness
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // curl statement
            // $ curl -X POST -H "Content-Type: application/vnd.kafka.json.v2+json" --data '{"records":[{"value":{"name": "testUser"}}]}' "http://localhost:8082/topics/jsontest"
            var broker = "localhost";
            var port = "8082";
            var topic = "test-a";

            var root = new Root
            {
                //records = new List<Record> { new Record { value = new Value { test = "this is a message" } } }
                records = new List<Record> { new Record { value = "this is a message"  } }
            };

            var json = new JavaScriptSerializer().Serialize(root);  //@"{""id"": 0, ""value"": ""testing this"""; // 

            try
            {
                var client = new RestClient
                {
                    BaseUrl = new Uri(string.Format("http://{0}:{1}/topics/{2}", broker, port, topic))
                };
                var request = new RestRequest(Method.POST);
                request.Parameters.Clear();
                request.AddParameter("application/vnd.kafka.json.v2+json", json, ParameterType.RequestBody);
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

        class Root
        {
            public List<Record> records { get; set; }
        }
        class Record
        {
            //public Value value { get; set; }
            public string value { get; set; }
        }

        class Value
        {
            public string test { get; set; }
        }
    }
}
