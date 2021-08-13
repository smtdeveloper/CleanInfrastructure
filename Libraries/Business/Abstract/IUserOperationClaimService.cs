using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        Task<IResult> AddAsync(UserOperationClaimAddDto userOperationClaimAddDto);
    }
}
