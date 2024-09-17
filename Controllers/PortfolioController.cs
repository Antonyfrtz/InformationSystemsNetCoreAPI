using InformationSystems.Server.Interfaces;
using InformationSystems.Server.Models;
using InformationSystems.Server.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InformationSystems.Server.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly IFMPService _fmpService;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo, IFMPService FMPService)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
            _fmpService = FMPService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _stockRepo.CreateAsync(stock);
                }
            }
            if (stock == null) return BadRequest("Stock not found");
            var existingPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            if(existingPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Stock already in portfolio");
            var portfolio = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };
            await _portfolioRepo.CreateAsync(portfolio);
            if(portfolio == null) return BadRequest("Failed to add stock to portfolio");
            return Ok(portfolio);
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null) return BadRequest("Stock not found");
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var portfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            var stockToRemove = portfolio.FirstOrDefault(e => e.Symbol.ToLower() == symbol.ToLower());
            if (stockToRemove == null) return BadRequest("Stock not in portfolio");
            var removedStock = await _portfolioRepo.DeleteAsync(appUser,stockToRemove.Symbol);
            if (removedStock == null) return BadRequest("Failed to remove stock from portfolio");
            return NoContent();
        }
    }
}
