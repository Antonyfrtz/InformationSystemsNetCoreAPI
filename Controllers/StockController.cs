using InformationSystems.Server.Data;
using InformationSystems.Server.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InformationSystems.Server.Controllers
{
    [Route("/api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() {         // mapper
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDTO());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock==null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

    }
}
