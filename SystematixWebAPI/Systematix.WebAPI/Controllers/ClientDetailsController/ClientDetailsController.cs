using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Systematix.WebAPI.Business;
using Systematix.WebAPI.Models.DTO.ClientDetails;
using Systematix.WebAPI.Models.DTO.Holdings;

namespace Systematix.WebAPI.Controllers.ClientDetailsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientDetailsController : ControllerBase
    {
        public readonly IClientDetailsBusiness _clientDetailsBusiness;
        public ClientDetailsController(IClientDetailsBusiness clientDetailsBusiness) 
        {
            _clientDetailsBusiness = clientDetailsBusiness;
        }

        [HttpPost("ClientLogin")]
        public async Task<IActionResult> VerifyClient([FromBody] ClientInformationRequest clientInformation)
        {
            var result = await _clientDetailsBusiness.LoginClientAsync(clientInformation).ConfigureAwait(false);

            if (result.Status)
            {
                return Ok(new { status = result.Status , statusMessage = result.StatusMessage, token = result.Token , data = result.Client});
            } 

            return Ok(new { result.Status, result.StatusMessage });
        }

        [HttpPost("AddClient")]
        public async Task<IActionResult> AddClient([FromBody] ClientRegister client)
        {
            var result = await _clientDetailsBusiness.RegisterClientAsync(client).ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item2,ClientCode = result.Item3});
        }

        [HttpPost("AddClientDetails")]
        public async Task<IActionResult> AddClientDetails([FromBody] ClientDetailsRequest clientDetailsRequest)
        {
            var user = HttpContext.User;

            var userEmailID = user.Claims.FirstOrDefault(u => u.Type == "EmailID").Value;
            var ClientCode = user.Claims.FirstOrDefault(u => u.Type == "ClientCode").Value;
            var result = await _clientDetailsBusiness.RegisterClientDetails(clientDetailsRequest, ClientCode).ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item2 });
        }

        [HttpPost("AddClientAddress")]
        public async Task<IActionResult> AddClientAddress([FromBody] ClientResponse ClientResponse)
        {
            var user = HttpContext.User;

            var userEmailID = user.Claims.FirstOrDefault(u => u.Type == "EmailID").Value;
            var ClientCode = user.Claims.FirstOrDefault(u => u.Type == "ClientCode").Value;
            var result = await _clientDetailsBusiness.RegisterClientAddress(ClientResponse, ClientCode).ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item2 });
        }
        
        [HttpPost("AddClientStocks")]
        public async Task<IActionResult> AddClientStocks([FromBody] ClientHoldingsRequest ClientResponse)
        {
            var user = HttpContext.User;

            var userEmailID = user.Claims.FirstOrDefault(u => u.Type == "EmailID").Value;
            var ClientCode = user.Claims.FirstOrDefault(u => u.Type == "ClientCode").Value;
            var result = await _clientDetailsBusiness.AddClientStocks(ClientResponse, ClientCode).ConfigureAwait(false);
            return Ok(new { status = result.Item1, Message = result.Item2 });
        }

        [HttpGet("VerifyPAN")]
        public async Task<IActionResult> VerifyPAN(string PAN_Number)
        {
            var user = HttpContext.User;

            var userEmailID = user.Claims.FirstOrDefault(u => u.Type == "EmailID").Value;
            var ClientCode = user.Claims.FirstOrDefault(u => u.Type == "ClientCode").Value;

            ClientPANValidateRequest clientPANValidateRequest = new ClientPANValidateRequest() 
            {
                ClientCode = ClientCode,
                EmailId = userEmailID,
                PANNumber = PAN_Number
            };

            var result = await _clientDetailsBusiness.VerifyClientPAN_DetailsAsync(clientPANValidateRequest).ConfigureAwait(false);

            if (result.Status)
            {
                return Ok(new { result.Status, result.StatusMessage, result.ClientDetails});
            }

            return Ok(new { result.Status, result.StatusMessage });
        }
    }
}
