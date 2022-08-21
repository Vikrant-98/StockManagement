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

            if (string.IsNullOrEmpty(employeedetail.ClientCode) || string.IsNullOrEmpty(employeedetail.Password)) 
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
        public async Task<(bool, string,string)> RegisterClientAsync(ClientRegister ClientRegister)
        {
           
            if (string.IsNullOrEmpty(ClientRegister.EmailID) || string.IsNullOrEmpty(ClientRegister.Password)||
                string.IsNullOrEmpty(ClientRegister.FirstName) || string.IsNullOrEmpty(ClientRegister.LastName)) 
            {
                return (false, "Details are Invalid",string.Empty);
            }
            ClientInformation clientInformation = new ClientInformation() 
            {
                EmailID = ClientRegister.EmailID,
                ClientCode = GenerateUnique("SMX"),
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
                TradingCode = GenerateUnique("TMX"),
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
                BranchCode = ClientHoldings.BranchCode,
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

        private static string GenerateUnique(string strPrifix)
        {
            int CodeSize = 8;
            string strOrderNumber = null;
            Random rnd = new Random();
            char[] letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();

            int iPrifixCount = strPrifix.Length;
            int OrderCount = CodeSize - iPrifixCount;
            int iAlphabetLimit = 0;
            int iNumberLimit = 0;

            if ((OrderCount % 2) == 0)
            {
                if ((OrderCount / 2) != 1)
                {
                    iAlphabetLimit = OrderCount / 2;
                    iNumberLimit = iAlphabetLimit;
                }
                else
                {
                    iAlphabetLimit = 1;
                    iNumberLimit = 1;
                }
            }
            else
            {
                int CurrentOC = OrderCount - 1;
                if (CurrentOC == 0)
                {
                    iAlphabetLimit = 1;
                    iNumberLimit = 0;
                }
                if ((CurrentOC / 2) != 1)
                {
                    iAlphabetLimit = CurrentOC / 2;
                    iNumberLimit = iAlphabetLimit + 1;
                }
                else
                {
                    iAlphabetLimit = 1;
                    iNumberLimit = 1;
                }
            }

            int iAlphaCount = 0, iCount = 0, count = 0;

            try
            {
                while (count < OrderCount)
                {
                    if (rnd.Next() % 2 == 0 && iAlphaCount < iAlphabetLimit)
                    {
                        strOrderNumber += letter[rnd.Next(0, letter.Length)];
                        iAlphaCount++;
                        count++;
                    }
                    else if (iCount < iNumberLimit)
                    {
                        strOrderNumber += rnd.Next(0, 9);
                        iCount++;
                        count++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            strOrderNumber = strPrifix + strOrderNumber;
            return strOrderNumber;
        }

    }
}
