using _MVC_Identity_.Models.Enums;

namespace _MVC_Identity_.Entities.Interface
{
    public interface IBaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; } 
        public Status Status { get; set; }  
    }
}
