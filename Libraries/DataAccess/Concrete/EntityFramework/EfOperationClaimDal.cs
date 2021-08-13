using Core.DataAccess.RepositoryPattern;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimDal : EntityRepositoryBase<OperationClaim, ProjectDbContext>, IOperationClaimDal
    {
        public EfOperationClaimDal(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            using (var context = new ProjectDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return await result.ToListAsync();
            }
        }
    }
}
