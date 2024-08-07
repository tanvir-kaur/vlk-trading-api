using VLK.Trading.Domain.Models;

namespace VLK.Trading.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
    }
}
