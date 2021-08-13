using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        Task<IResult> AddAsync(OperationClaimAddDto operationClaimAddDto);
        Task<IDataResult<OperationClaim>> GetDefaultClaimAsync();
        Task<IDataResult<List<OperationClaim>>> GetClaimsAsync(User user);
    }
}
