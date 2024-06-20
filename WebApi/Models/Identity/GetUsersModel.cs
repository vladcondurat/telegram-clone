namespace WebApi.Models.Identity;

public class GetUsersModel
{
    public IEnumerable<UserModel> Users { get; set; } = new List<UserModel>();
}