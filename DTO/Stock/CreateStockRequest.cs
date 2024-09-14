namespace InformationSystems.Server.DTO.Stock
{
    public class CreateStockRequest
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; } // Last dividend
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; } // Market capitalization
    }

}
