using Systematix.WebAPI.Models.DTO.Stocks;

namespace Systematix.WebAPI.Business
{
    public interface IStockDetailsBusiness
    {
        Task<(bool, string)> AddStockDetails(StockDetailsRequest stockDetailsRequest);
        Task<List<StockDetails>> GetAllStockDetails();
    }
}
