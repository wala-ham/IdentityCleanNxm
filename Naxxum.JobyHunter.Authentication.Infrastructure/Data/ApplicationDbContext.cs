using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Naxxum.JobyHunter.Authentication.Infrastructure.Identity;



namespace Naxxum.JobyHunter.Authentication.Infrastructure.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

    
       

    }

}
