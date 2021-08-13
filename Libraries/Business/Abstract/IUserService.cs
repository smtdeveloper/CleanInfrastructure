using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> AddAsync(User user);
        Task<IDataResult<User>> GetByMailAsync(string email);
        Task<IDataResult<List<User>>> GetAllAsync();
    }
}