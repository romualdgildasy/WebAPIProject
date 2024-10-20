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

    }
}