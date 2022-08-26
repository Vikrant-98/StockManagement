using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Systematix.WebAPI.Business;
using Systematix.WebAPI.Models.DTO.Holdings;

namespace Systematix.WebAPI.Controllers.LedgerController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        private readonly ILedgerBusiness _ledgerBusiness;
        public LedgerController(ILedgerBusiness ledgerBusiness)
        {
            _ledgerBusiness = ledgerBusiness;
        }

        [HttpPost("AddLedger")]
        public async Task<IActionResult> AddLedger(double ledger)
        {
            var user = HttpContext.User;

            string? userEmailID = user?.Claims?.FirstOrDefault(u => u.Type == "EmailID")?.Value;
            string? ClientCode = user?.Claims?.FirstOrDefault(u => u.Type == "ClientCode")?.Value;

            if (string.IsNullOrEmpty(userEmailID) || string.IsNullOrEmpty(ClientCode))
            {
                return Unauthorized(new { Status = false, StatusMessage = "Token Not Valid or Unauthorized" });
            }
            var result = await _ledgerBusiness.AddLedgerAsync(ledger, ClientCode).ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item2 });
        }

        [HttpGet("GetLedger")]
        public async Task<IActionResult> GetLedger()
        {
            var user = HttpContext.User;

            string? userEmailID = user?.Claims?.FirstOrDefault(u => u.Type == "EmailID")?.Value;
            string? ClientCode = user?.Claims?.FirstOrDefault(u => u.Type == "ClientCode")?.Value;

            if (string.IsNullOrEmpty(userEmailID) || string.IsNullOrEmpty(ClientCode))
            {
                return Unauthorized(new { Status = false, StatusMessage = "Token Not Valid or Unauthorized" });
            }
            var result = await _ledgerBusiness.GetLedgerAsync(ClientCode).ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item3 ,Fund = result.Item2});
        }

        [HttpPost("AddBranch")]
        public async Task<IActionResult> AddBranch([FromBody] BranchRequest branch)
        {
            
            var result = await _ledgerBusiness.AddBranchAsync(branch).ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item2 });
        }

        [HttpGet("GetBrand")]
        public async Task<IActionResult> GetBranch()
        {
            
            var result = await _ledgerBusiness.GetBranchAsync().ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item1?"Success":"Someting Went Wrong", Data = result.Item2 });
        }


    }
}
