using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace Business.Mappers.AutoMapper.Profiles
{
    public class UserOperationClaimProfile : Profile
    {
        public UserOperationClaimProfile()
        {
            CreateMap<UserOperationClaimAddDto, UserOperationClaim>();
        }
    }
}
