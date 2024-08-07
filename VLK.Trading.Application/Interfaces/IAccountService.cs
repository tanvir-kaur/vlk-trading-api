using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLK.Trading.Application.Interfaces
{
    public interface IAccountService
    {
        Task<string> LoginAsync(string userName, string password);
    }
}
