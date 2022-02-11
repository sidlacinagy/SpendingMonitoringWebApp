
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Controllers;
using WebApplication3.Model;


class Test {

    
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<SpendingAppDbContext>(options => options.UseSqlServer("Server = localhost\\SQLEXPRESS; Database = SpendingApp; Trusted_Connection = True;"));
        var app = builder.Build();

    
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}