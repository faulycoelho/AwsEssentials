using AutoMapper;
using Users.Api.Contracts.Data;
using Users.Api.Contracts.Requests;
using Users.Api.Contracts.Responses;
using Users.Api.Domain;

namespace Users.Api.Mapping
{
    public class DomainToDtoMapper : Profile
    {
        public DomainToDtoMapper()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
