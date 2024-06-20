using Data.Infrastructure.Context;
using Data.Infrastructure.S3;
using Data.Infrastructure.UnitOfWork;
using Data.Repositories.MessageRepository;
using Data.Repositories.RoomRepository;
using Data.Repositories.UserRepository;
using Data.Repositories.UserRoomRepository;
using Services.Features.Auth;
using Services.Features.Messages;
using Services.Features.Rooms;
using Services.Features.Users;

namespace WebApi.Configurations;

public static class Services
{
    public static void AddServiceConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IAppDbContext, AppDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IUserRoomRepository, UserRoomRepository>();
        services.AddScoped<IS3Handler, S3Handler>();
    }
}