using System.ComponentModel.DataAnnotations;
using WebApi.Models.Message;

namespace WebApi.Models.Room;

public class RoomCardModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    
    public MessageModel? LastMessage { get; set; }
}