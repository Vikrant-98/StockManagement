using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Systematix.WebAPI.Models.DTO.EntityModel;

namespace Systematix.WebAPI.Models.DTO.Ledger
{
    public class Ledger : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ClientCode { get; set; }
        public double LedgerBalance { get; set; }
    }
    
    
}
