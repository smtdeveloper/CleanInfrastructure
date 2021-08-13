using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace Business.Mappers.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDto, User>();
        }
    }
}
