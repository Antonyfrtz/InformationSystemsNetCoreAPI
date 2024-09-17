using InformationSystems.Server.DTO.Stock;
using InformationSystems.Server.Filter;
using InformationSystems.Server.Models;

namespace InformationSystems.Server.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequest request);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
