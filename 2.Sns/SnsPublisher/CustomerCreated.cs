namespace SnsPublisher
{
    internal class CustomerCreated
    {
        public required Guid id { get; init; }
        public required string FullName { get; init; }
        public required string Email { get; init; }
        public required string GitHubUsername { get; init; }
        public required DateTime DateOfBirth { get; init; }
    }
}
