using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Systematix.WebAPI.Models.DTO.ClientDetails;
using Systematix.WebAPI.Models.DTO.EntityModel;

namespace Systematix.WebAPI.Models.DTO.Holdings
{
    public class ClientHoldings : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ClientCode { get; set; }
        public string Symbol { get; set; }
        public string ISIN { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public double Value { get; set; }
        public string BranchCode { get; set; }
    }

    public class Branch : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
    }
    public class BranchRequest
    {
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
    }

        public class ClientHoldingResponse 
    {
        public ClientDetailsResponse ClientDetails { get; set; }
        public bool Status { get; set; }
        public string StatusMessage { get; set; }
    }

    public class ClientHoldingsInfo
    {
        public string ClientCode { get; set; }
        public string Symbol { get; set; }
        public string ISIN { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public double Value { get; set; }
    }

    public class ClientHoldingsRequest
    {
        public string ISIN { get; set; }
        public string BranchCode { get; set; }
        public int Quantity { get; set; }
    }

}
