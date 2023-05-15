using Amazon.SimpleNotificationService.Model;
 

namespace Users.Api.Messaging
{
    public interface ISnsMessenger
    {
        Task<PublishResponse> PublishMessageAsync<T>(T message);
    }
}
