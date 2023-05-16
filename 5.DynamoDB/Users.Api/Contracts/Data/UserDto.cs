using System.Text.Json.Serialization;

namespace Users.Api.Contracts.Data
{
    public class UserDto
    {
        [JsonPropertyName("pk")]
        public string Pk => Id.ToString();

        [JsonPropertyName("sk")]
        public string Sk => Id.ToString();

        public int Id { get; init; } = default!;
        public string Name { get; init; } = default!;

        public string Surname { get; init; } = default!;

        public string Email { get; init; } = default!;

        public DateTime DateOfBirth { get; init; }
        public DateTime UpdatedAt { get; set; }
    }
}
