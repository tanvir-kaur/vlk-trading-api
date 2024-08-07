using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLK.Trading.Core.Models;

namespace VLK.Trading.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
    }
}
