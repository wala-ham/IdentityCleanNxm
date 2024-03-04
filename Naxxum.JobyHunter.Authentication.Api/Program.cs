
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Naxxum.JobyHunter.Authentication.Application;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces;
using Naxxum.JobyHunter.Authentication.Infrastructure;
using Naxxum.JobyHunter.Authentication.Infrastructure.Data;
using Naxxum.JobyHunter.Authentication.Infrastructure.Identity;
using Naxxum.JobyHunter.Authentication.Infrastructure.Services;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();


// Include Application Dependency
builder.Services.AddApplicationServices();
// Include Infrastructur Dependency
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .WithOrigins("https://localhost:7219")
            .AllowAnyMethod()
            .AllowAnyHeader() // Allow any header
            .AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

//builder.Services.AddAuthorization();

//builder.Services.AddIdentityCore<ApplicationUser>()
//    .AddEntityFrameworkStores<ApplicationDbContext>().AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapIdentityApi<ApplicationUser>();
app.UseHttpsRedirection();


app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
