using MediatR;
using Users.Consumer.Messages;

namespace Users.Consumer.Handlers
{
    public class UserDeletedHandler : IRequestHandler<UserDeleted>
    {
        private readonly ILogger<UserDeletedHandler> _logger;

        public UserDeletedHandler(ILogger<UserDeletedHandler> logger)
        {
            _logger = logger;
        }


        Task IRequestHandler<UserDeleted>.Handle(UserDeleted request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.Id.ToString());
            return Unit.Task;
        }
    }

}
