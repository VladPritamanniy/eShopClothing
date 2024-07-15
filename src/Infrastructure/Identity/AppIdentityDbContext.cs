using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }

        DbSet<Clothing> Clothing { get; set; }

        DbSet<Core.Entities.Type> Type { get; set; }

        DbSet<Size> Size { get; set; }
    }
}
