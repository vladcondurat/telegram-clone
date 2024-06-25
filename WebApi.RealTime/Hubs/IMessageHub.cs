using Services.Contracts;

namespace WebApi.RealTime.Hubs;

public interface IMessageHub
{
    Task ReceiveMessage(IMessageCreated message);
}