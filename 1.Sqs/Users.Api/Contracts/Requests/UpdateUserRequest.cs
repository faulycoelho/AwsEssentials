using Microsoft.AspNetCore.Mvc;

namespace Users.Api.Contracts.Requests
{
    public class UpdateUserRequest
    {
        [FromRoute(Name = "id")] public int Id { get; init; }

        [FromBody] public UserRequest User { get; set; } = default!;
    }
}
