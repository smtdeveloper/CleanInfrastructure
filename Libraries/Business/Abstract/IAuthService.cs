using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(UserRegisterDto userRegisterDto);
        Task<IDataResult<User>> LoginAsync(UserLoginDto userLoginDto);
        Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user);
    }
}