using Systematix.WebAPI.Models.DTO.ClientDetails;
using Systematix.WebAPI.Models.DTO.Holdings;

namespace Systematix.WebAPI.Repositories.ClientDetailsRepositories
{
    public interface IClientDetailsRepository
    {
        Task<Response> LoginClientAsync(ClientInformationRequest employeedetail);
        Task<(bool, string,string)> RegisterClientAsync(ClientInformation employeedetail);
        Task<(bool, string)> RegisterClientDetails(ClientDetails ClientDetailsRequest, string clientCode);
        Task<(bool, string)> RegisterClientAddress(ClientAddress ClientAddress);
        Task<(bool, string)> AddClientStocks(ClientHoldings ClientHoldings);
        Task<ClientHoldingResponse> VerifyClientPAN_DetailsAsync(ClientPANValidateRequest employeedetail);
    }
}
