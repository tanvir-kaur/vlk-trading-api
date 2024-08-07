using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VLK.Trading.Application.Helpers;
using VLK.Trading.Application.Interfaces;
using VLK.Trading.Application.Models;
using VLK.Trading.Domain.Interfaces;

namespace VLK.Trading.Application
{
    public class AccountService : IAccountService
    {
        public IUnitOfWork _unitOfWork;
        private readonly TokenInformation _tokenInformation;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IUnitOfWork unitOfWork,
            IOptions<TokenInformation> options,
            ILogger<AccountService> logger)
        {
            _unitOfWork = unitOfWork;
            _tokenInformation = options.Value;
            _logger = logger;
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                string token = string.Empty;
                var user = await _unitOfWork.Users.GetUserByEmail(email);
                if (user != null)
                {
                    if (Encryption.ValidatePassword(password, user.PasswordSalt, user.PasswordHash, user.PasswordIterations))
                    {
                        token = TokenHelper.CreateToken(user, _tokenInformation.SigningKey, _tokenInformation.Issuer, _tokenInformation.Audience);
                    }
                }
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
