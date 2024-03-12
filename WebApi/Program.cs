using Microsoft.EntityFrameworkCore;
using Data.Infrastructure.Context;
using Data.Infrastructure.UnitOfWork;
using Data.Repositories.UserRepository;
using Services.Features.Auth;
using Services.Features.Users;
using WebApi.Configurations;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost,1433;Database=test-db;User Id=SA;Password=Vladcondurat2003;MultipleActiveResultSets=true;TrustServerCertificate=true",
        b => b.MigrationsAssembly("WebApi")));

builder.Services.AddControllers();
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerProperties();
builder.Services.AddJwtAuthorization(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseRouting();

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();