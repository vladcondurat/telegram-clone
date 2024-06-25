using WebApi.RealTime.Consumers;
using WebApi.RealTime.Hubs;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddLogging(configure => configure.AddConsole());

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<MessageCreatedConsumer>();
    
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        corsPolicyBuilder => corsPolicyBuilder
            .WithOrigins("http://localhost:5173", "https://4374-79-112-61-159.ngrok-free.app", "http://localhost")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()); 
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.MapHub<MessageHub>("/messageHub");

app.Run();