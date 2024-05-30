using Services.Features.Messages;

namespace Services.Features.Rooms;

public class RoomCardDto
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; } 
    public string RoomName { get; set; } = string.Empty;
    public DateTime? LastMessageTime { get; set; }
    
    // aici trebuie sa las asa daca oricum am id in obiectul de LastMessage? Mai bine scot idul din obiect si il pun asa? Conteaza?
    // public int? LastMessageId { get; set; }
    public MessageDto? LastMessage { get; set; } 
}