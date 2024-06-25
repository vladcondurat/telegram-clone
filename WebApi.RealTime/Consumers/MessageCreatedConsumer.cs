using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Services.Contracts;
using WebApi.RealTime.Hubs;

namespace WebApi.RealTime.Consumers;

public class MessageCreatedConsumer : IConsumer<MessageCreated>
{
    private readonly IHubContext<MessageHub, IMessageHub> _hubContext;

    public MessageCreatedConsumer(IHubContext<MessageHub, IMessageHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<MessageCreated> context)
    {
        await _hubContext.Clients.All.ReceiveMessage(context.Message);
    }
}