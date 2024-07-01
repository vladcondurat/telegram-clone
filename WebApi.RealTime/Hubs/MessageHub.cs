using Data.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.SignalR;
using Services.Features.Auth.Jwt;

namespace WebApi.RealTime.Hubs
{
    public class MessageHub : Hub<IMessageHub>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public MessageHub(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = _httpContextAccessor.HttpContext!.User.Claims
                .FirstOrDefault(user => user.Type == JwtClaims.Id);

            if (userId is null)
            {
                Context.Abort();
                return;
            }
            
            var isParsed = int.TryParse(userId.Value, out var id);
            
            if (!isParsed)
            {
                Context.Abort();
                return;
            }
            
            var chatRoomIds = _unitOfWork.Rooms.GetRoomsIdByUserId(id);
            
            foreach (var chatRoomId in chatRoomIds)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());
            }

            await base.OnConnectedAsync();
        }

        public async Task JoinRooms(List<string> chatRoomIds)
        {
            foreach (var chatRoomId in chatRoomIds)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);
            }
        }

        public async Task JoinRoom(string chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);
        }

        public async Task LeaveRoom(string chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId);
        }
    }
}