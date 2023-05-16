using AutoMapper;
using Users.Api.Contracts.Requests;
using Users.Api.Contracts.Responses;
using Users.Api.Domain;

namespace Users.Api.Mapping
{
    public class DomainToApiContractMapper : Profile
    {
        public DomainToApiContractMapper()
        {
            CreateMap<UserRequest, User>();
            CreateMap<UpdateUserRequest, User>();            
            CreateMap<User, UserResponse>();            
            //CreateMap<User, UserResponse>();            
        }
    }
}
