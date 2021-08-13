using Core.DataAccess.RepositoryPattern;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EntityRepositoryBase<User, ProjectDbContext>, IUserDal
    {
        public EfUserDal(ProjectDbContext context) : base(context)
        {
        }
    }
}
