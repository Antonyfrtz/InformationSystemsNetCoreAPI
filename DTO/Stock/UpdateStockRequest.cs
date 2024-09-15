using System.ComponentModel.DataAnnotations;

namespace InformationSystems.Server.DTO.Stock
{
    public class UpdateStockRequest
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(40, ErrorMessage = "Company name cannot be over 40 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Purchase price must be greater than 0")]
        public decimal Purchase { get; set; }
        [Range(0, 100.00, ErrorMessage = "Purchase price must be greater than 0")]
        public decimal LastDiv { get; set; } // Last dividend
        [Required]
        [MaxLength(20, ErrorMessage = "Industry cannot be over 20 characters")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Market cap must be greater than 0")]
        public long MarketCap { get; set; } // Market capitalization
    }
}
