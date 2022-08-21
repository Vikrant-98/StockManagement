namespace Systematix.WebAPI.Models.DTO.EntityModel
{
    public class Entity
    {
        public int Status { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatesBy { get; set; }
    }
}
