using Data.Contracts;

namespace WebApi.RealTime.Hubs;

public interface IMessageHub
{
    Task ReceiveMessage(IMessageCreated message);
}