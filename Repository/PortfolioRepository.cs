using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Data;
using WebAPIProject.Interfaces;
using WebAPIProject.Models;
using Microsoft.EntityFrameworkCore ;

namespace WebAPIProject.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    { 
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreatedAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser appUser, string symbole)
        {
            var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Stockage.Symbole.ToLower() == symbole.ToLower());

            if(portfolioModel == null)
            {
                return null;
            }
            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel ;
        }

        public async Task<List<Stockage>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new  Stockage
            {
                Id = stock.StockageId,
                Symbole = stock.Stockage.Symbole,
                NomCompagnie = stock.Stockage.NomCompagnie,
                DerniereDividente = stock.Stockage.DerniereDividente,
                Industrie = stock.Stockage.Industrie,
                CapitalisationBoursiere = stock.Stockage.CapitalisationBoursiere,

            }).ToListAsync();      
        }
    }
}