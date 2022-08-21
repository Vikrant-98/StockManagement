using Systematix.WebAPI.Data;
using Systematix.WebAPI.Models.DTO.Stocks;

namespace Systematix.WebAPI.Repositories.StockDetailsRepositories
{
    public class StockDetailsRepository : IStockDetailsRepository
    {
        private readonly SystematixDbContext systematixDbContext;
        public StockDetailsRepository(SystematixDbContext _systematixDbContext)
        {
            systematixDbContext = _systematixDbContext;
        }

        public async Task<(bool, string)> AddStockDetails(StockDetails stockDetailsRequest)
        {
            var IfExist = systematixDbContext.tbl_StockDetails.Any(x => x.StockName == stockDetailsRequest.StockName || x.Symbol == stockDetailsRequest.Symbol);
            
            if (IfExist)
            {
                return (false, "StockName Or Symbol already Exist");
            }

            await systematixDbContext.tbl_StockDetails.AddAsync(stockDetailsRequest);
            await systematixDbContext.SaveChangesAsync();

            return (true, "StockDetails Register Succesfully");
        }

        public async Task<List<StockDetails>> GetAllStockDetails()
        {
            var listStockDetails = systematixDbContext.tbl_StockDetails.ToList();
            return listStockDetails;
        }
    }
}
