using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IResult> AddAsync(User user)
        {
            var rulesResult = BusinessRules.Run(await this.CheckMailExistAsync(user.Email));
            if (!rulesResult.Success)
                return rulesResult;


            var addStatus = await _userDal.AddAsync(user);
            if (!addStatus)
                return new ErrorResult(Messages.UserNotAdded);

            return new SuccessResult(Messages.UserAdded);
        }

        public async Task<IDataResult<List<User>>> GetAllAsync()
        {
            var users = await _userDal.GetAllAsync(disableTracking: true);
            if (users.Count == 0)
                return new ErrorDataResult<List<User>>(null, Messages.UsersNotFoundInSystem);

            return new SuccessDataResult<List<User>>(users, Messages.UsersListed);
        }

        public async Task<IDataResult<User>> GetByMailAsync(string email)
        {
            var foundUser = await _userDal.GetAsync(u => u.Email == email);
            if (foundUser == null)
                return new ErrorDataResult<User>(null, Messages.UserNotFound);

            return new SuccessDataResult<User>(foundUser, Messages.UserBrought);
        }

        private async Task<IResult> CheckMailExistAsync(string email)
        {
            var existResult = await _userDal.IsExistsAsync(p => p.Email.Equals(email));
            if (existResult)
                return new ErrorResult(Messages.UserEmailAlreadyUsed);

            return new SuccessResult(Messages.UserEmailNotExist);
        }
    }
}