using Microsoft.AspNetCore.SignalR;

namespace WebApi.RealTime.Hubs;

public class MessageHub : Hub<IMessageHub>;