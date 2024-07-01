namespace Data.Contracts;

public class MessageCreated : IMessageCreated
{
    public string RoomId { get; set; } = string.Empty;
}