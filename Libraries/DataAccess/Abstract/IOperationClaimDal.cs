using Core.DataAccess.RepositoryPattern;
using Core.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOperationClaimDal : IEntityRepository<OperationClaim>
    {
        Task<List<OperationClaim>> GetClaimsAsync(User user);
    }
}
