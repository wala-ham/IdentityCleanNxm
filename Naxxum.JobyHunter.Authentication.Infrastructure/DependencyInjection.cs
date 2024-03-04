using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Naxxum.JobyHunter.Authentication.Infrastructure.Repository.Command.Base;
using Naxxum.JobyHunter.Authentication.Infrastructure.Data;
using Naxxum.JobyHunter.Authentication.Infrastructure.Identity;
using Naxxum.JobyHunter.Authentication.Infrastructure.Repository.Query.Base;
using System;
using System.Collections.Generic;
using Naxxum.JobyHunter.Authentication.Infrastructure.Services.Role;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.Role;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.User;
using Naxxum.JobyHunter.Authentication.Infrastructure.Services.User;

namespace Naxxum.JobyHunter.Authentication.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            ));

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false; // For special character
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            });

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IIdentityServiceUser, IdentityServiceUser>();
            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

            return services;
        }
    }
}
