using Microsoft.EntityFrameworkCore;
using VLK.Trading.Domain.Interfaces;
using VLK.Trading.Domain.Models;
using VLK.Trading.Infrastructure.Data;


namespace VLK.Trading.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly TradingDBContext _dbContext;
        public UserRepository(TradingDBContext context)
        {
            _dbContext = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Email == email); 
        }
    }
}
