using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.Validators.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal _userOperationClaimDal;
        IMapper _mapper;
        public UserOperationClaimManager(IUserOperationClaimDal operationClaimDal, IMapper mapper)
        {
            _userOperationClaimDal = operationClaimDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(UserOperationClaimAddDtoValidator))]
        public async Task<IResult> AddAsync(UserOperationClaimAddDto userOperationClaimAddDto)
        {
            var rulesResult = BusinessRules.Run(await this.CheckClaimExistByUser(userOperationClaimAddDto.UserId, userOperationClaimAddDto.OperationClaimId));
            if (!rulesResult.Success)
                return rulesResult;

            var userOperationClaimToAdd = _mapper.Map<UserOperationClaim>(userOperationClaimAddDto);

            var addResult = await _userOperationClaimDal.AddAsync(userOperationClaimToAdd);
            if (!addResult)
                return new ErrorResult(Messages.UserOperationClaimNotAdded);

            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        /// <summary>
        /// Yetkinin Kullanıcıda olup olmadığını kontrol eder.
        /// </summary>
        /// <returns>Yetki Kullanıcıda Yoksa TRUE, kullanıcıda varsa FALSE döner.</returns>
        private async Task<IResult> CheckClaimExistByUser(Guid userId, Guid operationClaimId)
        {
            var existsResult = await _userOperationClaimDal.IsExistsAsync(p => p.UserId == userId && p.OperationClaimId == operationClaimId);
            if (existsResult)
                return new ErrorResult(Messages.UserOperationClaimAlreadyUsedByUser);

            return new SuccessResult(Messages.UserOperationClaimNotFoundByUser);
        }
    }
}
