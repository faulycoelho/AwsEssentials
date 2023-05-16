using Users.Api.Domain;

namespace Users.Api.Services
{
    public interface IUserService
    {
        Task<bool> CreateAsync(User User);

        Task<User?> GetAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();

        Task<bool> UpdateAsync(User user, DateTime requestStarted);

        Task<bool> DeleteAsync(int id);
    }
}
