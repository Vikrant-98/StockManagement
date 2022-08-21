using Microsoft.AspNetCore.Mvc;
using Systematix.WebAPI.Business;
using Systematix.WebAPI.Models.DTO.Stocks;

namespace Systematix.WebAPI.Controllers.StocksController
{
    public class StocksController : Controller
    {
        public readonly IStockDetailsBusiness _stockDetailsBusiness;
        public StocksController(IStockDetailsBusiness stockDetailsBusiness)
        {
            _stockDetailsBusiness = stockDetailsBusiness;
        }

        [HttpPost("AddStocks")]
        public async Task<IActionResult> AddStocks([FromBody] StockDetailsRequest client)
        {
            var result = await _stockDetailsBusiness.AddStockDetails(client).ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item2 });
        }

        [HttpGet("GetAllStocks")]
        public async Task<IActionResult> GetAllStocks()
        {
            var result = await _stockDetailsBusiness.GetAllStockDetails().ConfigureAwait(false);
            return Ok(new { status = true, Message = "Success",data = result });
        }
    }
}
