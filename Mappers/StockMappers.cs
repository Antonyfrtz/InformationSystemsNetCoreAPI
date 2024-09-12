using InformationSystems.Server.DTO.Stock;

namespace InformationSystems.Server.Mappers
{
    public static class StockMappers // trim out unnecessary fields
    {
        public static StockDTO ToStockDTO(this Models.Stock stock)
        {
            return new StockDTO
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap
            };
        }
    }
}
