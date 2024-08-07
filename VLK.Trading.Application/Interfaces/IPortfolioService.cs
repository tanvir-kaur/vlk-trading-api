using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLK.Trading.Application.DTOModels;
using VLK.Trading.Domain.Models;

namespace VLK.Trading.Application.Interfaces
{
    public interface IPortfolioService
    {
        Task<UserPortfolioDTO> GetPortfolioByUserIdAsync(Guid userId);
    }
}
