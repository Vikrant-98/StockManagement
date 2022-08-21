using Systematix.WebAPI.Models.DTO.Stocks;
using Systematix.WebAPI.Repositories.StockDetailsRepositories;

namespace Systematix.WebAPI.Business
{
    public class StockDetailsBusiness : IStockDetailsBusiness
    {
        private readonly IStockDetailsRepository _stockDetailsRepository;
        public StockDetailsBusiness(IStockDetailsRepository stockDetailsRepository) 
        {
            _stockDetailsRepository = stockDetailsRepository;
        }

        public async Task<(bool, string)> AddStockDetails(StockDetailsRequest stockDetailsRequest)
        {
            
            if (string.IsNullOrEmpty(stockDetailsRequest.Symbol) || string.IsNullOrEmpty(stockDetailsRequest.ISIN) ||
                string.IsNullOrEmpty(stockDetailsRequest.StockName) || stockDetailsRequest.StockPrice < 0)
            {
                return (false, "Details are Invalid");
            }

            StockDetails stockDetails = new StockDetails() 
            {
                StockName = stockDetailsRequest.StockName,
                StockPrice = stockDetailsRequest.StockPrice,
                Symbol = stockDetailsRequest.Symbol,
                ISIN = stockDetailsRequest.ISIN,
                Status =1,
                IsDelete = false,
                CreatedDate = DateTime.Now,
            };

            var Results = _stockDetailsRepository.AddStockDetails(stockDetails).Result;

            return Results;
        }

        public async Task<List<StockDetails>> GetAllStockDetails()
        {
            var Results = _stockDetailsRepository.GetAllStockDetails().Result;
            return Results;
        }
    }
}
