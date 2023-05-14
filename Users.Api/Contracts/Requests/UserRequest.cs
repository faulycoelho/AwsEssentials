namespace Users.Api.Contracts.Requests
{
    public class UserRequest
    {
        public int Id { get; init; } = default!;
        public string Name { get; init; } = default!;

        public string Surname { get; init; } = default!;

        public string Email { get; init; } = default!;

        public DateTime DateOfBirth { get; init; } = default!;
    }
}
