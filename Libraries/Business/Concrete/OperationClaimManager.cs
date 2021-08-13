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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal _operationClaimDal;
        IMapper _mapper;
        public OperationClaimManager(IOperationClaimDal operationClaimDal, IMapper mapper)
        {
            _operationClaimDal = operationClaimDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(OperationClaimAddDtoValidator))]
        public async Task<IResult> AddAsync(OperationClaimAddDto operationClaimAddDto)
        {
            if (operationClaimAddDto.IsDefault)
            {
                var checkDefaultClaimResult = await this.CheckDefaultClaimExistsAsync();
                if (!checkDefaultClaimResult.Success)
                    return checkDefaultClaimResult;
            }

            var rulesResult = BusinessRules.Run(await this.CheckNameExistsAsync(operationClaimAddDto.Name));
            if (!rulesResult.Success)
                return rulesResult;

            var mappedEntity = _mapper.Map<OperationClaim>(operationClaimAddDto);

            var addResult = await _operationClaimDal.AddAsync(mappedEntity);

            if (!addResult)
                return new ErrorResult(Messages.OperationClaimNotAdded);

            return new SuccessResult(Messages.OperationClaimAdded);
        }

        public async Task<IDataResult<OperationClaim>> GetDefaultClaimAsync()
        {
            var findedEntity = await _operationClaimDal.GetAsync(predicate: p => p.IsDefault == true, disableTracking: true);
            if (findedEntity == null)
                return new ErrorDataResult<OperationClaim>(null, Messages.DefaultOperationClaimNotFound);

            return new SuccessDataResult<OperationClaim>(findedEntity, Messages.DefaultOperationClaimListed);
        }

        public async Task<IDataResult<List<OperationClaim>>> GetClaimsAsync(User user)
        {
            var claims = await _operationClaimDal.GetClaimsAsync(user);
            if (claims.Count == 0)
                return new ErrorDataResult<List<OperationClaim>>(null, Messages.OperationClaimsNotFoundForUser);

            return new SuccessDataResult<List<OperationClaim>>(claims, Messages.OperationClaimsListedForUser);
        }

        /// <summary>
        /// Varsayılan bir yetkinin databasede olup olmadığını kontrol eder.
        /// </summary>
        /// <returns>Varsayılan Yetki varsa Success FALSE, Varsayılan Yetki yoksa TRUE döner.</returns>
        private async Task<IResult> CheckDefaultClaimExistsAsync()
        {
            var existsResult = await _operationClaimDal.IsExistsAsync(p => p.IsDefault == true);
            if (existsResult)
                return new ErrorResult(Messages.OperationClaimDefaultValueAlreadyExist);

            return new SuccessResult(Messages.OperationClaimDefaultValueNotFound);
        }

        /// <summary>
        /// Yetki Adının databasede kayıtlı olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="name">Yetki Adı</param>
        /// <returns>Yetki Adı varsa Success FALSE, Yetki Adı yoksa TRUE döner.</returns>
        private async Task<IResult> CheckNameExistsAsync(string name)
        {
            var existsResult = await _operationClaimDal.IsExistsAsync(p => p.Name.Equals(name));
            if (existsResult)
                return new ErrorResult(Messages.OperationClaimNameAlreadyExist);

            return new SuccessResult(Messages.OperationClaimNameNotFound);
        }

    }
}
