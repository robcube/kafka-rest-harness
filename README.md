# kafka-rest-harness
To show how C# along with the RestSharp library works against a Kafka REST Proxy

Seems there's is a strict schema we must use for it to work out of the box:

{"records":[{"value":{"name": "testUser"}}]}

or

{"records":[{"value":"message1"}]}

