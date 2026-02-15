using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository: IstockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync()
        {
           var stock = await _context.Stocks.Include(c=>c.Comments).ToListAsync();
            return stock;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return null;
            }
            _context.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

       

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(x=>x.Comments).FirstOrDefaultAsync(x=>x.Id == id);
        }

            public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            stockModel.Symbol = stockDto.Symbol;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.Industry = stockDto.Industry;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.MarketCap = stockDto.MarketCap;
            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}
