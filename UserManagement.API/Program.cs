using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using UserManagement.Application;
using UserManagement.Application.Interfaces;
using UserManagement.Domain;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.DBContext;
using UserManagement.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUserService, UserService>();
var dbProvider = Environment.GetEnvironmentVariable("DB_PROVIDER") ??
                 builder.Configuration.GetValue<string>("DatabaseProvider");
if (string.IsNullOrEmpty(dbProvider))
{
    throw new Exception("DB_PROVIDER environment variable is not set.");
}

// Register the appropriate DbContext
if (dbProvider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddDbContext<PostgreSQLContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
    builder.Services.AddScoped<IUserRepository>(sp => new UserRepository(sp.GetRequiredService<PostgreSQLContext>()));
}
else if (dbProvider.Equals("MySQL", StringComparison.OrdinalIgnoreCase))
{
    var mySQLConnection = builder.Configuration.GetConnectionString("MySQLConnection");
    builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(mySQLConnection, ServerVersion.AutoDetect(mySQLConnection)));
    builder.Services.AddScoped<IUserRepository>(sp => new UserRepository(sp.GetRequiredService<MySQLContext>()));
}
else
{
    throw new Exception("Invalid DB_PROVIDER value. Use 'PostgreSQL' or 'MySQL'.");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
