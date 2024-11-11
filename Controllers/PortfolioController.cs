using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Extension;
using WebAPIProject.Interfaces;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockageRepository _stockageRepo;
        private readonly IPortfolioRepository _portfolioRepo;
      
           public PortfolioController (UserManager<AppUser> userManager , IStockageRepository stockageRepo
           , IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockageRepo = stockageRepo;
            _portfolioRepo = portfolioRepo;

        }
         
         [HttpGet]
         [Authorize]
         public async  Task<IActionResult> GetUserPortfolio()
         {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);

         }
         [HttpPost]
         [Authorize]
         public async Task<IActionResult> AddPortfolio(string symbole)
         {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stockage = await _stockageRepo.GetBySymboleAsync(symbole); 

            if (stockage == null) return BadRequest("stockage not found");

            var userPortfolio= await _portfolioRepo.GetUserPortfolio(appUser);

            if(userPortfolio.Any(e=> e.Symbole.ToLower()==symbole.ToLower())) return BadRequest("Cannot  add same stockage to portfolio");

            var portfolioModel = new Portfolio 
            {
                StockageId = stockage.Id,
                AppUserId = appUser.Id
            };

            await _portfolioRepo.CreatedAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500,"Could not create");
            }
            else
            {
                return Created();
            }
         }
        
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbole)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            var filterStock = userPortfolio.Where(s => s.Symbole.ToLower() == symbole.ToLower()).ToList();

            if(filterStock.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolio(appUser, symbole);
            }
            else
            {
                return BadRequest("Stockage not  in your  portfolio");  
            }
                return Ok();
         }
    }
}