using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Room;

public class UserIdsModel
{ 
    [Required] 
    public IEnumerable<int> UserIds{ get; set; } = new List<int>();
}