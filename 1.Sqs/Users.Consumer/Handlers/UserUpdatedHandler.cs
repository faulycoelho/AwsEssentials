using MediatR;
using Users.Consumer.Messages;

namespace Users.Consumer.Handlers
{
    public class UserUpdatedHandler : IRequestHandler<UserUpdated>
    {
        private readonly ILogger<UserUpdatedHandler> _logger;

        public UserUpdatedHandler(ILogger<UserUpdatedHandler> logger)
        {
            _logger = logger;
        }


        Task IRequestHandler<UserUpdated>.Handle(UserUpdated request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.Email);
            return Unit.Task;
        }
    }

}
