using InformationSystems.Server.Models;

namespace InformationSystems.Server.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeleteAsync(AppUser user, string symbol);
    }
}
