using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Systematix.WebAPI.Models.DTO.EntityModel;

namespace Systematix.WebAPI.Models.DTO.UserDetails
{
    public class UserMaster : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool MailIdStatus { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string TermsAndConditions { get; set; }
        
    }
    public class UserAddress : Entity
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
}
