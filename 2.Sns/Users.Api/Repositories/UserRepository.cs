using Dapper;
using Users.Api.Contracts.Data;
using Users.Api.Database;

namespace Users.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> CreateAsync(UserDto User)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"INSERT INTO Users ( Name, Surname, Email, DateOfBirth) 
            VALUES (@Name, @Surname, @Email, @DateOfBirth)",
                User);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(@"DELETE FROM Users WHERE Id = @Id",
                new { Id = id });
            return result > 0;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QueryAsync<UserDto>("SELECT * FROM Users");
        }

        public async Task<UserDto?> GetAsync(int id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QuerySingleOrDefaultAsync<UserDto>(
                "SELECT * FROM Users WHERE Id = @Id LIMIT 1", new { Id = id });
        }

        public async Task<bool> UpdateAsync(UserDto User)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"UPDATE Users SET Name = @Name, Surname = @Surname, Email = @Email, 
                 DateOfBirth = @DateOfBirth WHERE Id = @Id",
                User);
            return result > 0;
        }
    }
}
