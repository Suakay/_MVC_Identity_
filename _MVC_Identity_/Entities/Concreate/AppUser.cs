using _MVC_Identity_.Entities.Interface;
using _MVC_Identity_.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace _MVC_Identity_.Entities.Concreate
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {
        public string CreatedBy { get ; set; }
        public DateTime CreatedDate { get ; set ; }
        public DateTime? ModifiedDate { get ; set ; }
        public Status Status { get ; set ; }
    }
}
