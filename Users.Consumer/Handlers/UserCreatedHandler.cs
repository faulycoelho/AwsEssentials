
using MediatR;
using Users.Consumer.Messages;

namespace Users.Consumer.Handlers
{
    public class UserCreatedHandler : IRequestHandler<UserCreated>
    {
        private readonly ILogger<UserCreatedHandler> _logger;

        public UserCreatedHandler(ILogger<UserCreatedHandler> logger)
        {
            _logger = logger;
        }

        Task IRequestHandler<UserCreated>.Handle(UserCreated request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.Email);
            return Unit.Task;
        }
    }

}
