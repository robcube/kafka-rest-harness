# kafka-rest-harness
Simple example to show how C# along with the RestSharp library works against a Kafka REST Proxy

Logic shows how to post to a topic.

Seems there's is a strict schema we must use for it to work out of the box:

{"records":[{"value":{"name": "testUser"}}]}

or

{"records":[{"value":"message1"}]}

