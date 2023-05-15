namespace Users.Api.Contracts.Responses
{
    public class UserResponse
    {

        public int Id { get; init; }

        public string Name { get; init; } = default!;

        public string Surname { get; init; } = default!;

        public string Email { get; init; } = default!;

        public DateTime DateOfBirth { get; init; }
    }
}
