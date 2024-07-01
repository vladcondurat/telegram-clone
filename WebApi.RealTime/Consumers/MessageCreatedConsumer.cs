using Data.Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
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
        var chatRoomId = context.Message.RoomId;
        await _hubContext.Clients.Group(chatRoomId).ReceiveMessage(context.Message);
    }
}