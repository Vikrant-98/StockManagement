using Systematix.WebAPI.Models.DTO.ClientDetails;
using Systematix.WebAPI.Models.DTO.Holdings;

namespace Systematix.WebAPI.Services.Mapping
{
    public class ObjectMapper
    {
        private readonly ILogger<ObjectMapper> _logger;
        public ObjectMapper(ILogger<ObjectMapper> logger)
        {
            _logger = logger;
        }

        public ClientHoldingResponse mapClientDetails(ClientInformation clientInformation, ClientDetails clientDetails, ClientAddress clientAddress, List<ClientHoldingsInfo> clientHoldingsInfo,double portFolio) 
        {
            try
            {
                return new ClientHoldingResponse()
                {
                    ClientDetails = new ClientDetailsResponse()
                    {
                        EmailID = clientInformation.EmailID,
                        ClientCode = clientInformation.ClientCode,
                        FirstName = clientInformation.FirstName,
                        LastName = clientInformation.LastName,
                        MobileNumber = clientDetails.MobileNumber,
                        MotherName = clientDetails.MotherName,
                        PANNumber = clientDetails.PANNumber,
                        TradingCode = clientDetails.TradingCode,
                        FatherName = clientDetails.FatherName,
                        Details = clientHoldingsInfo, 
                        PortFolioValue = Math.Round(portFolio, 2) 
                    },
                    Status = true,
                    StatusMessage = "Success"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Map Client Details : " + ex.Message);
                return new ClientHoldingResponse()
                {
                    Status = false,
                    StatusMessage = ex.Message
                };
            }
        }

    }
}
