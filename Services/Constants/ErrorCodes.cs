namespace Services.Constants;

public enum ErrorCodes
{
    EntityNotFoundBase = 1000,
    UserNotFound = EntityNotFoundBase + 1,
    RoomNotFound = EntityNotFoundBase + 2,
    MessageNotFound = EntityNotFoundBase + 2,
    
    AuthorizationBase = 2000,
    
    BusinessBase = 3000,
    RoomMinUsers = BusinessBase + 1,
    UsernameAlreadyExists = BusinessBase + 2,
    MessageEmpty = BusinessBase + 3,
    InvalidUsernameOrPassword = BusinessBase + 4,
    GroupOnlyAction = BusinessBase + 5

}