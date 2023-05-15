using AutoMapper;
using Users.Api.Domain;

namespace Users.Api.Mapping
{
    public class DomainToMessageMapper : Profile
    {
        public DomainToMessageMapper()
        {
            CreateMap<User, Contracts.Messages.UserCreated>();
            CreateMap<User, Contracts.Messages.UserUpdated>();
        }
    }
}
