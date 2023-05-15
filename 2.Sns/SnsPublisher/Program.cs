using System.Text.Json;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using SnsPublisher;

var customer = new CustomerCreated
{
    id = Guid.NewGuid(),
    Email = "faulycoelho@gmail.com",
    FullName = "Fauly Coelho",
    DateOfBirth = new DateTime(1992, 7, 13),
    GitHubUsername = "faulycoelho"
};

var snsClient = new AmazonSimpleNotificationServiceClient();

var topicArnResponse = await snsClient.FindTopicAsync("customers");

var publishRequest = new PublishRequest
{
    TopicArn = topicArnResponse.TopicArn,
    Message = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(CustomerCreated)
            }
        }
    }
};

var response = await snsClient.PublishAsync(publishRequest);