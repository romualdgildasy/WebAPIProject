using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Models;

namespace WebAPIProject.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stockage>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreatedAsync(Portfolio portfolio);
    }
}