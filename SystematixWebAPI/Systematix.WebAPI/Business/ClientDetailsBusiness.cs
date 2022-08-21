using Systematix.WebAPI.Models.DTO.ClientDetails;
using Systematix.WebAPI.Models.DTO.Holdings;
using Systematix.WebAPI.Repositories.ClientDetailsRepositories;

namespace Systematix.WebAPI.Business
{
    public class ClientDetailsBusiness : IClientDetailsBusiness
    {
        public readonly IClientDetailsRepository _clientDetailsRepository;
        public ClientDetailsBusiness(IClientDetailsRepository clientDetailsRepository) 
        {
            _clientDetailsRepository = clientDetailsRepository;
        }

        public async Task<Response> LoginClientAsync(ClientInformationRequest employeedetail)
        {

            if (string.IsNullOrEmpty(employeedetail.EmailID) || string.IsNullOrEmpty(employeedetail.Password)) 
            {
                return new Response()
                {
                    Client = new ClientLoginResponse(),
                    Token = string.Empty,
                    Status = false,
                    StatusMessage = "User Details Invalid"
                };
            }

            var Response = await _clientDetailsRepository.LoginClientAsync(employeedetail).ConfigureAwait(false);

            return Response;
        }
        public async Task<(bool, string)> RegisterClientAsync(ClientRegister ClientRegister)
        {
           
            if (string.IsNullOrEmpty(ClientRegister.EmailID) || string.IsNullOrEmpty(ClientRegister.Password)||
                string.IsNullOrEmpty(ClientRegister.FirstName) || string.IsNullOrEmpty(ClientRegister.LastName)) 
            {
                return (false, "Details are Invalid");
            }
            ClientInformation clientInformation = new ClientInformation() 
            {
                EmailID = ClientRegister.EmailID,
                ClientCode = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                FirstName = ClientRegister.FirstName,
                LastName = ClientRegister.LastName,
                Status = 1,
                IsDelete = false,
                CreatedDate = DateTime.Now,
                Password = ClientRegister.Password
            };

            var Response = await _clientDetailsRepository.RegisterClientAsync(clientInformation).ConfigureAwait(false);

            return Response;
        }
        public async Task<(bool, string)> RegisterClientAddress(ClientResponse ClientAddress,string ClientCode)
        {
            if (string.IsNullOrEmpty(ClientCode))
            {
                return (false, "client Code not exist or valid try again ......");
            }
            if (string.IsNullOrEmpty(ClientAddress.PINCode) || string.IsNullOrEmpty(ClientAddress.Address) ||
                string.IsNullOrEmpty(ClientAddress.Country) || string.IsNullOrEmpty(ClientAddress.City))
            {
                return (false, "Details are Invalid");
            }

            if (ClientAddress.PINCode.Length != 6)
            {
                return (false, "PINCode are Invalid");
            }
            ClientAddress clientAddress = new ClientAddress() 
            {
                City = ClientAddress.City,
                Address = ClientAddress.Address,
                ClientCode = ClientCode,
                Country = ClientAddress.Country,
                PINCode = ClientAddress.PINCode,
                IsDelete = false,
                Status = 1,
                CreatedDate = DateTime.Now
            };

            var result = await _clientDetailsRepository.RegisterClientAddress(clientAddress).ConfigureAwait(false);

            return result;
        }
        public async Task<(bool, string)> RegisterClientDetails(ClientDetailsRequest ClientDetailsRequest,string clientCode)
        {
            if (string.IsNullOrEmpty(clientCode))
            {
                return (false, "client Code not exist or valid try again ......");
            }

            if (string.IsNullOrEmpty(ClientDetailsRequest.PANNumber) || string.IsNullOrEmpty(ClientDetailsRequest.MobileNumber)||
                string.IsNullOrEmpty(ClientDetailsRequest.FatherName) || string.IsNullOrEmpty(ClientDetailsRequest.MotherName)) 
            {
                return (false, "Details are Invalid");
            }
            ClientDetails clientInformation = new ClientDetails() 
            {
                FatherName = ClientDetailsRequest.FatherName,
                ClientCode = clientCode,
                TradingCode = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                MotherName = ClientDetailsRequest.MobileNumber,
                Status = 1,
                PANNumber = ClientDetailsRequest.PANNumber,
                MobileNumber = ClientDetailsRequest.MobileNumber,
                IsDelete = false,
                CreatedDate = DateTime.Now
            };

            var Response = await _clientDetailsRepository.RegisterClientDetails(clientInformation, clientCode).ConfigureAwait(false);

            return Response;
        }
        public async Task<(bool, string)> AddClientStocks(ClientHoldingsRequest ClientHoldings,string ClientCode)
        {

            ClientHoldings clientHoldingsRequest = new ClientHoldings() 
            {
                ClientCode = ClientCode,
                IsDelete =false,
                CreatedDate = DateTime.Now,
                ISIN = ClientHoldings.ISIN,
                Quantity = ClientHoldings.Quantity,
                Status = 1
            };

            var result = await _clientDetailsRepository.AddClientStocks(clientHoldingsRequest).ConfigureAwait(false);

            return result;
        }

        public async Task<ClientHoldingResponse> VerifyClientPAN_DetailsAsync(ClientPANValidateRequest ClientPANValidateRequest)
        {
            
            if (string.IsNullOrEmpty(ClientPANValidateRequest.PANNumber) || 
                string.IsNullOrEmpty(ClientPANValidateRequest.EmailId) ||
                string.IsNullOrEmpty(ClientPANValidateRequest.ClientCode))
            {
                return new ClientHoldingResponse()
                {
                    Status = false,
                    StatusMessage = "Details is not Valid"
                };
            }

            var Response = await _clientDetailsRepository.VerifyClientPAN_DetailsAsync(ClientPANValidateRequest).ConfigureAwait(false);
            return Response;
        }

    }
}
