using Amazon.SQS;
using Amazon.SQS.Model;
using SqsPublisher;
using System.Text.Json;

var sqsClient = new AmazonSQSClient();

var customer = new CustomerCreated
{
    id = Guid.NewGuid(),
    Email = "faulycoelho@gmail.com",
    FullName = "Fauly Coelho",
    DateOfBirth = new DateTime(1992, 7, 13),
    GitHubUsername = "faulycoelho"
};
var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");
var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType",
            new MessageAttributeValue
            {
                DataType = "string",
                StringValue = nameof(CustomerCreated)
            }
        }
    }
};

var response = await sqsClient.SendMessageAsync(sendMessageRequest);
Console.ReadKey();
