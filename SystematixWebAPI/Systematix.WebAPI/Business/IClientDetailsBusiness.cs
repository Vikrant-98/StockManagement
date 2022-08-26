using Systematix.WebAPI.Models.DTO.ClientDetails;
using Systematix.WebAPI.Models.DTO.Holdings;

namespace Systematix.WebAPI.Business
{
    public interface IClientDetailsBusiness
    {
        Task<Response> LoginClientAsync(ClientInformationRequest ClientInformationRequest);
        Task<(bool,string,string)> RegisterClientAsync(ClientRegister ClientRegister);
        Task<(bool,string)> RegisterClientDetails(ClientDetailsRequest ClientDetailsRequest,string clientCode);
        Task<(bool, string)> RegisterClientAddress(ClientResponse ClientAddress, string ClientCode);
        Task<(bool, string)> AddClientStocks(ClientHoldingsRequest ClientHoldings, string ClientCode);
        Task<PanResponse> VerifyClientPAN_DetailsAsync(ClientPANValidateRequest ClientPANValidateRequest);
        Task<ClientHoldingResponse> User_DetailsAsync(ClientPANValidateRequest employeedetail);
    }
}
