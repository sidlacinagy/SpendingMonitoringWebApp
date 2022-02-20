
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApplication3.Controllers;
using WebApplication3.Model;


class Test {

    
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigurationManager configuration = builder.Configuration;
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            
        })

            // Adding Jwt Bearer
        .AddJwtBearer(options =>
        {
        options.SaveToken = true;
            options.MapInboundClaims = false;
            options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = configuration["Jwt:Issuer"],
          ValidIssuer = configuration["Jwt:Issuer"],

          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("email", policy => { policy.RequireClaim("email");});
            options.AddPolicy("subusers", policy => { policy.RequireClaim("subusers"); policy.RequireClaim("email"); });
        });
        builder.Services.AddDbContext<SpendingAppDbContext>(options => options.UseSqlServer("Server = localhost\\SQLEXPRESS; Database = SpendingApp; Trusted_Connection = True;"));
        var app = builder.Build();

    
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseAuthentication();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}