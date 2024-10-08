﻿using InformationSystems.Server.Data;
using InformationSystems.Server.DTO.Stock;
using InformationSystems.Server.Filter;
using InformationSystems.Server.Interfaces;
using InformationSystems.Server.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InformationSystems.Server.Controllers
{
    [Route("/api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _stockRepo = stockRepository;
            _context = context;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) {         // mapper
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model state is not valid");
                return BadRequest(ModelState);
            }
            var stocks = await _stockRepo.GetAllAsync(query);
            var stocksDTO = stocks.Select(x => x.ToStockDTO()).ToList();
            return Ok(stocksDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock==null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequest stockDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = stockDTO.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stock);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDTO());
        }

        [HttpPut]
        [Route("/api/stock/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequest stockDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepo.UpdateAsync(id, stockDTO);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpDelete]
        [Route("/api/stock/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepo.DeleteAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
