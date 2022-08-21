using Microsoft.EntityFrameworkCore;
using Systematix.WebAPI.Data;
using Systematix.WebAPI.Models.DTO.ClientDetails;
using Systematix.WebAPI.Models.DTO.Holdings;
using Systematix.WebAPI.Models.DTO.Ledger;
using Systematix.WebAPI.Repositories.TokenHandlerRepositories;
using Systematix.WebAPI.Services.Mapping;

namespace Systematix.WebAPI.Repositories.ClientDetailsRepositories
{
    public class ClientDetailsRepository : IClientDetailsRepository
    {
        private readonly SystematixDbContext systematixDbContext;
        private readonly ObjectMapper _objectMapper;
        private readonly ITokenHandler _tokenHandler;

        public ClientDetailsRepository(SystematixDbContext SystematixDbContext, ObjectMapper objectMapper, ITokenHandler tokenHandler)
        {
            systematixDbContext = SystematixDbContext;
            _objectMapper = objectMapper;
            _tokenHandler = tokenHandler;
        }

        public async Task<Response> LoginClientAsync(ClientInformationRequest employeedetail)
        {
            try
            {
                Response ClientLoginResponse = new Response();
                var ClientInformation = await systematixDbContext.tbl_Client.FirstOrDefaultAsync(x => x.ClientCode == employeedetail.ClientCode
                                                                    && x.Password == employeedetail.Password).ConfigureAwait(false);

                if (ClientInformation == null)
                {
                    return new Response()
                    {
                        Client = new ClientLoginResponse(),
                        Token = string.Empty,
                        Status = false,
                        StatusMessage = "User Dose Not Exist"
                    };
                }

                return new Response()
                {
                    Client = new ClientLoginResponse()
                    {
                        EmailID = ClientInformation.EmailID,
                        ClientCode = ClientInformation.ClientCode,
                        FirstName = ClientInformation.FirstName,
                        LastName = ClientInformation.LastName
                    },
                    Status = true,
                    Token = await _tokenHandler.TokenAsync(ClientInformation).ConfigureAwait(false),
                    StatusMessage = "Success"
                };
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        
        public async Task<(bool, string, string)> RegisterClientAsync(ClientInformation ClientRegister)
        {
            try
            {
                var details = systematixDbContext.tbl_Client.Any(x => x.EmailID == ClientRegister.EmailID);
                if (details)
                {
                    return (false, "User already Exist", ClientRegister.ClientCode);
                }

                await systematixDbContext.tbl_Client.AddAsync(ClientRegister);
                await systematixDbContext.SaveChangesAsync();

                return (true, "User Register Succesfully", ClientRegister.ClientCode);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<(bool, string)> RegisterClientDetails(ClientDetails ClientDetailsRequest, string clientCode)
        {
            try
            {
                var details = systematixDbContext.tbl_ClientDetails.Any(x => x.PANNumber == ClientDetailsRequest.PANNumber);
                if (details)
                {
                    return (false, "PAN Number already Exist");
                }

                await systematixDbContext.tbl_ClientDetails.AddAsync(ClientDetailsRequest);
                await systematixDbContext.SaveChangesAsync();

                return (true, "User Details Register Succesfully");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<(bool, string)> RegisterClientAddress(ClientAddress ClientAddress)
        {
            try
            {
                var details = systematixDbContext.tbl_ClientAddress.Where(x => x.ClientCode == ClientAddress.ClientCode).ToList();

                foreach (var item in details)
                {
                    item.IsDelete = true;
                    item.UpdateDate = DateTime.Now;
                    systematixDbContext.tbl_ClientAddress.Update(item);
                    await systematixDbContext.SaveChangesAsync().ConfigureAwait(false);
                }

                Ledger ledgerDetails = new Ledger()
                {
                    CreatedDate = DateTime.Now,
                    ClientCode = ClientAddress.ClientCode,
                    LedgerBalance = 0,
                    Status = 1,
                    IsDelete = false,
                };

                await systematixDbContext.tbl_ClientAddress.AddAsync(ClientAddress).ConfigureAwait(false);
                await systematixDbContext.tbl_ClientLedger.AddAsync(ledgerDetails).ConfigureAwait(false);
                await systematixDbContext.SaveChangesAsync();

                return (true, "Address Added Succesfully");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<(bool, string)> AddClientStocks(ClientHoldings ClientHoldings)
        {
            try
            {
                var Stocks = systematixDbContext.tbl_StockDetails.Where(x => x.ISIN == ClientHoldings.ISIN).FirstOrDefault();
                if (Stocks == null)
                {
                    return (false, "ISIN Is Invalid");
                }
                var funds = systematixDbContext.tbl_ClientLedger.Where(x => x.ClientCode == ClientHoldings.ClientCode).FirstOrDefault();
                var FundBalance = funds.LedgerBalance;
                var totalValue = ClientHoldings.Quantity * Stocks.StockPrice;
                ClientHoldings.Symbol = Stocks.Symbol;
                ClientHoldings.Rate = Stocks.StockPrice;
                ClientHoldings.Value = totalValue;
                if (totalValue > FundBalance)
                {
                    return (false, "Fund Is Not Available");
                }

                await systematixDbContext.tbl_ClientHoldings.AddAsync(ClientHoldings).ConfigureAwait(false);

                funds.LedgerBalance = funds.LedgerBalance - totalValue;
                funds.UpdateDate = DateTime.Now;
                systematixDbContext.tbl_ClientLedger.Update(funds);
                await systematixDbContext.SaveChangesAsync();



                return (true, "Stocks to Client Added Succesfully");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        
        public async Task<ClientHoldingResponse> VerifyClientPAN_DetailsAsync(ClientPANValidateRequest ClientDetail)
        {
            try
            {
                var details = await systematixDbContext.tbl_ClientDetails.FirstOrDefaultAsync(x => x.ClientCode == ClientDetail.ClientCode
                                                                && x.PANNumber == ClientDetail.PANNumber).ConfigureAwait(false);
                if (details == null)
                {
                    return new ClientHoldingResponse()
                    {
                        Status = false,
                        StatusMessage = "Details is not Valid"
                    };
                }

                var result = await ClientResponse(ClientDetail.EmailId).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }



        private async Task<ClientHoldingResponse> ClientResponse(string EmailId) 
        {
            List<ClientHoldingsInfo> clientHoldingsInfo = new List<ClientHoldingsInfo>();
            var ClientInformation = await systematixDbContext.tbl_Client.FirstOrDefaultAsync(x => x.EmailID == EmailId).ConfigureAwait(false);
            if (ClientInformation == null)
            {
                return new ClientHoldingResponse()
                {
                    Status = false,
                    StatusMessage = "Client Information is not valid"
                };
            }

            var ClientDetails = await systematixDbContext.tbl_ClientDetails.FirstOrDefaultAsync(x => x.ClientCode == ClientInformation.ClientCode).ConfigureAwait(false);
            var ClientAddress = await systematixDbContext.tbl_ClientAddress.FirstOrDefaultAsync(x => x.ClientCode == ClientInformation.ClientCode).ConfigureAwait(false);
            var ClientHoldings = systematixDbContext.tbl_ClientHoldings.Where(x => x.ClientCode == ClientInformation.ClientCode).ToList();
            var ClientHoldingGroup = ClientHoldings.GroupBy(x => x.ISIN);

            if (ClientDetails == null || ClientAddress == null || ClientHoldings == null)
            {
                return new ClientHoldingResponse()
                {
                    Status = false,
                    StatusMessage = "Details are not complete"
                };
            }

            foreach (var item in ClientHoldingGroup)
            {
                var TempQuantity = item.Sum(x => x.Quantity);
                var TempRate = item.Average(x => x.Rate);
                var TempClientCode = item.Select(x => x.ClientCode).FirstOrDefault();
                var TempSymbol = item.Select(x => x.Symbol).FirstOrDefault();
                var TempISIN = item.Select(x => x.ISIN).FirstOrDefault();
                clientHoldingsInfo.Add(new ClientHoldingsInfo()
                {
                    ClientCode = string.IsNullOrEmpty(TempClientCode) ? string.Empty : TempClientCode,
                    Symbol = string.IsNullOrEmpty(TempSymbol) ? string.Empty : TempSymbol,
                    ISIN = string.IsNullOrEmpty(TempISIN) ? string.Empty : TempISIN,
                    Quantity = TempQuantity,
                    Rate = TempRate,
                    Value = TempRate * TempQuantity
                });
            }

            var result = _objectMapper.mapClientDetails(ClientInformation, ClientDetails, ClientAddress, clientHoldingsInfo);

            return result;
        }

    }
}
