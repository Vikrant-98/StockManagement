using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Systematix.WebAPI.Models.DTO.EntityModel;
using Systematix.WebAPI.Models.DTO.Holdings;

namespace Systematix.WebAPI.Models.DTO.ClientDetails
{
    public class ClientInformation : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ClientCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
    }
    
    public class ClientRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
    }



    public class ClientInformationRequest
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
    }

    public class ClientLoginResponse
    {
        public string EmailID { get; set; }
        public string ClientCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Response
    {
        public ClientLoginResponse Client { get; set; }
        public string Token { get; set; }
        public bool Status { get; set; }
        public string StatusMessage { get; set; }
    }

    public class ClientPANValidateRequest
    {
        public string PANNumber { get; set; }
        public string ClientCode { get; set; }
        public string EmailId { get; set; }
    }

    public class ClientDetails : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ClientCode { get; set; }
        public string TradingCode { get; set; }
        public string MobileNumber { get; set; }
        public string PANNumber { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
    }

    public class ClientDetailsRequest
    {
        public string MobileNumber { get; set; }
        public string PANNumber { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
    }

    public class ClientDetailsResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string ClientCode { get; set; }
        public string TradingCode { get; set; }
        public string MobileNumber { get; set; }
        public string PANNumber { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public List<ClientHoldingsInfo> ClientHoldings { get; set; }
    }

    public class ClientAddress : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ClientCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PINCode { get; set; }
    }
    public class ClientResponse
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PINCode { get; set; }
    }
}
