using _MVC_Identity_.Entities.Concreate;
using Microsoft.AspNetCore.Identity;

namespace _20_Identity.Models.DTOs
{
    public class AssingnedRoleDTO
    {
        public IdentityRole<Guid> Role { get; set; }    
        public string RoleName { get; set; } 
        public IEnumerable<AppUser> HasRole { get; set; }   
        public IEnumerable<AppUser> HasNotRole { get; set; }    
        public string[] AddIds {  get; set; }   
        public string[] DeleteIds { get; set; } 
    }
}
