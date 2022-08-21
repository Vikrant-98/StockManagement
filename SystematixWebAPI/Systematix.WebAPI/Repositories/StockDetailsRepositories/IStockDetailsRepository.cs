using Systematix.WebAPI.Models.DTO.Stocks;

namespace Systematix.WebAPI.Repositories.StockDetailsRepositories
{
    public interface IStockDetailsRepository
    {
        Task<(bool, string)> AddStockDetails(StockDetails stockDetailsRequest);
        Task<List<StockDetails>> GetAllStockDetails();
    }
}
