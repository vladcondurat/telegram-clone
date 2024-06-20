namespace WebApi.Configurations;

public static class Cors
{
    public static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                corsPolicyBuilder => corsPolicyBuilder
                    .WithOrigins("http://localhost:5173", "https://4374-79-112-61-159.ngrok-free.app", "http://localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
    }
}