using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Room;

public class AddUsersToRoomRequest
{ 
    [Required] 
    public IEnumerable<int> UserIdsToAdd { get; set; } = new List<int>();
}