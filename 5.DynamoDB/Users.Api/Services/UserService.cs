using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Users.Api.Contracts.Data;
using Users.Api.Repositories;

namespace Users.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(Domain.User user)
        {
            var existingUser = await _userRepository.GetAsync(user.Id);
            if (existingUser is not null)
            {
                var message = $"A user with id {user.Id} already exists";
                throw new ValidationException(message, GenerateValidationError(nameof(user), message));
            }

            var userDto = _mapper.Map<UserDto>(user);
            var response = await _userRepository.CreateAsync(userDto);

            return response;
        }

        public async Task<Domain.User?> GetAsync(int id)
        {
            var userDto = await _userRepository.GetAsync(id);
            var user = _mapper.Map<Domain.User>(userDto);

            return user;
        }

        public async Task<IEnumerable<Domain.User>> GetAllAsync()
        {
            var userDtos = await _userRepository.GetAllAsync();
            return userDtos.Select(_mapper.Map<Domain.User>);
        }

        public async Task<bool> UpdateAsync(Domain.User user, DateTime requestStarted)
        {
            var userDto = _mapper.Map<UserDto>(user);


            var response = await _userRepository.UpdateAsync(userDto, requestStarted);

            return response;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _userRepository.DeleteAsync(id);

            return response;
        }

        private static ValidationFailure[] GenerateValidationError(string paramName, string message)
        {
            return new[]
            {
            new ValidationFailure(paramName, message)
        };
        }

    }
}
