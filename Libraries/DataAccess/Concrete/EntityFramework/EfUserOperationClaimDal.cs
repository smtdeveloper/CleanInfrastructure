using Core.DataAccess.RepositoryPattern;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EntityRepositoryBase<UserOperationClaim, ProjectDbContext>, IUserOperationClaimDal
    {
        public EfUserOperationClaimDal(ProjectDbContext context) : base(context)
        {
        }
    }
}
