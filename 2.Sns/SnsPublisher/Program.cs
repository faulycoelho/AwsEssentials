using System.Text.Json;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using SnsPublisher;

var customer = new UserCreated
{
    Id = 1,
    Email = "jose@gmail.com",
    Name = "Fauly Coelho",
    DateOfBirth = new DateTime(1990, 1, 1),
    Surname = "faulycoelho"
};

var snsClient = new AmazonSimpleNotificationServiceClient();

var topicArnResponse = await snsClient.FindTopicAsync("users");

var publishRequest = new PublishRequest()
{
    TopicArn = topicArnResponse.TopicArn,
    Message = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(UserCreated)
            }
        }
    }
};

var response = await snsClient.PublishAsync(publishRequest);