using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Systematix.WebAPI.Models.DTO.EntityModel;

namespace Systematix.WebAPI.Models.DTO.Stocks
{
    public class StockDetails : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string StockName { get; set; }
        public string Symbol { get; set; }
        public string ISIN { get; set; }
        public double StockPrice { get; set; }
    }
    public class StockDetailsRequest
    {
        public string StockName { get; set; }
        public string Symbol { get; set; }
        public string ISIN { get; set; }
        public double StockPrice { get; set; }
    }
}
