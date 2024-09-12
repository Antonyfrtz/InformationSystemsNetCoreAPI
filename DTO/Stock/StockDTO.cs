using InformationSystems.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InformationSystems.Server.DTO.Stock
{
    public class StockDTO
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; } // Last dividend
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; } // Market capitalization
        // We don't need to include comments in the DTO to not expose them to client(example)
    }
}
