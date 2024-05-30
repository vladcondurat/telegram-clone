namespace Services.Features.Users;

public interface IUserService
{
    UserPreviewDto GetUser(int id);
}