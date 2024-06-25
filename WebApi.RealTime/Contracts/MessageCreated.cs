
namespace Services.Contracts;

public class MessageCreated : IMessageCreated
{
    public string MessageId { get; set; } = string.Empty;
}