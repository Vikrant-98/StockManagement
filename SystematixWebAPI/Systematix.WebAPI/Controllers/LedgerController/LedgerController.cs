using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Systematix.WebAPI.Business;

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

            var userEmailID = user.Claims.FirstOrDefault(u => u.Type == "EmailID").Value;
            var ClientCode = user.Claims.FirstOrDefault(u => u.Type == "ClientCode").Value;
            var result = await _ledgerBusiness.AddLedgerAsync(ledger, ClientCode).ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item2 });
        }

        [HttpGet("GetLedger")]
        public async Task<IActionResult> GetLedger()
        {
            var user = HttpContext.User;

            var userEmailID = user.Claims.FirstOrDefault(u => u.Type == "EmailID").Value;
            var ClientCode = user.Claims.FirstOrDefault(u => u.Type == "ClientCode").Value;
            var result = await _ledgerBusiness.GetLedgerAsync(ClientCode).ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item3 ,Fund = result.Item2});
        }

    }
}
