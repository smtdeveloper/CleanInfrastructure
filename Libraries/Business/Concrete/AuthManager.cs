using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        IOperationClaimService _operationClaimService;
        IUserOperationClaimService _userOperationClaimService;
        IMapper _mapper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper, IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
        }

        public async Task<IResult> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var ruleResult = BusinessRules.Run(this.CheckIfPasswordsMatch(userRegisterDto.Password, userRegisterDto.PasswordAgain));
            if (!ruleResult.Success)
                return new ErrorResult(ruleResult.Message);

            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userToAdd = _mapper.Map<User>(userRegisterDto);
            userToAdd.PasswordHash = passwordHash;
            userToAdd.PasswordSalt = passwordSalt;

            var addResult = await _userService.AddAsync(userToAdd);
            if (!addResult.Success)
                return new ErrorResult(addResult.Message);

            var addDefaultClaimResult = await AddDefaultClaimToUser(userToAdd);
            if (!addDefaultClaimResult.Success)
                return new ErrorResult(addDefaultClaimResult.Message);

            return new SuccessResult(Messages.UserRegistered);
        }

        public async Task<IDataResult<User>> LoginAsync(UserLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByMailAsync(userForLoginDto.Email);
            if (!userToCheck.Success)
                return new ErrorDataResult<User>(null, Messages.UserNotFound);

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
                return new ErrorDataResult<User>(null, Messages.PasswordError);

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        public async Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user)
        {
            var claimsResult = await _operationClaimService.GetClaimsAsync(user);
            if (!claimsResult.Success)
                return new ErrorDataResult<AccessToken>(null, claimsResult.Message);

            var accessToken = _tokenHelper.CreateToken(user, claimsResult.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        private async Task<IResult> AddDefaultClaimToUser(User user)
        {
            var defaultClaimResult = await _operationClaimService.GetDefaultClaimAsync();
            if (!defaultClaimResult.Success)
                return defaultClaimResult;

            UserOperationClaimAddDto userOperationClaim = new UserOperationClaimAddDto()
            {
                OperationClaimId = defaultClaimResult.Data.Id,
                UserId = user.Id
            };

            var addResult = await _userOperationClaimService.AddAsync(userOperationClaim);
            if (!addResult.Success)
                return new ErrorResult(Messages.UserOperationClaimNotAdded);

            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        /// <summary>
        /// Girilen şifrelerin aynı olup olmadığını kontrol eder.
        /// </summary>
        /// <returns>Şifreler aynısa Success döner.</returns>
        private IResult CheckIfPasswordsMatch(string password1, string password2)
        {
            if (!password1.Equals(password2))
                return new ErrorResult(Messages.PasswordsDoNotMatch);

            return new SuccessResult(Messages.PasswordsMatched);
        }

        /// <summary>
        /// Kullanıcının sistemi kullanma izninin olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="canUseSystem"></param>
        /// <returns>Kullanma izni varsa Success döner.</returns>
        private IResult CheckCanUseSystem(bool canUseSystem)
        {
            if (canUseSystem == false)
                return new ErrorResult(Messages.YouAreNotAllowedToUseSystem);

            return new SuccessResult(Messages.YouAreAllowedToUseSystem);
        }
    }
}