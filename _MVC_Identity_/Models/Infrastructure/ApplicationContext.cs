using _MVC_Identity_.Entities.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _MVC_Identity_.Models.Infrastructure
{
    public class ApplicationContext:IdentityDbContext<AppUser,IdentityRole<Guid> ,Guid>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
