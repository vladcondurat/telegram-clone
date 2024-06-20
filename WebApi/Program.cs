using WebApi.Configurations;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddJwtAuthorization(builder.Configuration);
builder.Services.AddAwsConfiguration(builder.Configuration);
builder.Services.AddServiceConfiguration();
builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerProperties();

builder.Services.AddCorsConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
