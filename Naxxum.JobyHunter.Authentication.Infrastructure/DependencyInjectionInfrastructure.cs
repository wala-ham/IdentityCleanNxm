using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Naxxum.JobyHunter.Authentication.Infrastructure.Data;



namespace Naxxum.JobyHunter.Authentication.Infrastructure
{
    public static  class DependencyInjectionInfrastructure
    {
        public static IServiceCollection AddInfrastructureServices
           (this IServiceCollection services, IConfiguration configuration)

        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ??
                    throw new InvalidOperationException("connection string 'DefaultConnection not found '"))
            );

         


            return services;

        }
    }
}
