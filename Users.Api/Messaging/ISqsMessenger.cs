using Amazon.SQS.Model;

namespace Users.Api.Messaging
{
    public interface ISqsMessenger
    {
        Task<SendMessageResponse> SendMessageAsync<T>(T message);
    }
}
