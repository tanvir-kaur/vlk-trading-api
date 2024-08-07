using Microsoft.Extensions.Options;
using VLK.Trading.Core.Interfaces;
using VLK.Trading.Core.Models;
using VLK.Trading.Infrastructure.Helper;
using VLK.Trading.Infrastructure.Helpers;
using VLK.Trading.Services.Interfaces;

namespace VLK.Trading.Services
{
    public class AccountService : IAccountService
    {
        public IUnitOfWork _unitOfWork;
        private readonly TokenInformation _tokenInformation;

        public AccountService(IUnitOfWork unitOfWork, IOptions<TokenInformation> options)
        {
            _unitOfWork = unitOfWork;
            _tokenInformation = options.Value;
        }
        public async Task<string> Login(string email, string password)
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
                //TODO: Add logging
                return null;
            }
        }
    }
}
