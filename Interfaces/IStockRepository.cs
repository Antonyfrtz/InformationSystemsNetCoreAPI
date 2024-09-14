﻿using InformationSystems.Server.DTO.Stock;
using InformationSystems.Server.Models;

namespace InformationSystems.Server.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequest request);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}