using Users.Api.Contracts.Data;

namespace Users.Api.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(UserDto User);

        Task<UserDto?> GetAsync(int id);

        Task<IEnumerable<UserDto>> GetAllAsync();

        Task<bool> UpdateAsync(UserDto User, DateTime requestStarted);

        Task<bool> DeleteAsync(int id);
    }
}
