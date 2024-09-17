using System.ComponentModel.DataAnnotations.Schema;

namespace InformationSystems.Server.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppUserId { get; set; }
        public int StockId { get; set; }
        public AppUser User { get; set; }
        public Stock Stock { get; set; }
    }
}
