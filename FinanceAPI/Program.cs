using FinanceAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinanceAPI.Application.Interfaces;
using FinanceAPI.Infrastructure.Repositories;
using FinanceAPI.Application.UseCases.Users;
using FinanceAPI.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FinanceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
