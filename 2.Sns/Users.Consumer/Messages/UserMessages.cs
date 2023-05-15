namespace Users.Consumer.Messages
{
    public class User : ISqsMessage
    {
        public required int Id { get; init; }

        public required string Name { get; init; }

        public required string Surname { get; init; }

        public required string Email { get; init; }

        public required DateTime DateOfBirth { get; init; }
    }

    public class UserCreated : User
    {
    }

    public class UserUpdated : User
    {
    }

    public class UserDeleted : ISqsMessage
    {
        public required int Id { get; init; }
    }

}
