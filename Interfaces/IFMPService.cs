using InformationSystems.Server.Models;

namespace InformationSystems.Server.Interfaces
{
    public interface IFMPService
    {
        Task<Stock> FindStockBySymbolAsync(string symbol);
    }
}
