using Services.Constants;

namespace Services.Exceptions;

public class EntityNotFoundException : BaseException
{
    public int EntityId { get; }
    public Type EntityType { get; }
    
    public EntityNotFoundException(int entityId, Type entityType) : base(ErrorCodes.EntityNotFoundBase, GetDefaultMessage(entityId, entityType))
    {
        EntityId = entityId;
        EntityType = entityType;
    }

    public EntityNotFoundException(int entityId, Type entityType, string message) : base(ErrorCodes.EntityNotFoundBase, message) 
    {
        EntityId = entityId;
        EntityType = entityType;
    }

    public EntityNotFoundException(ErrorCodes code, int entityId, Type entityType) : base(code, GetDefaultMessage(entityId, entityType))
    {
        EntityId = entityId;
        EntityType = entityType;
    }

    public EntityNotFoundException(ErrorCodes code, int entityId, Type entityType, string message) : base(code, message)
    {
        EntityId = entityId;
        EntityType = entityType;
    }

    private static string GetDefaultMessage(int entityId, Type entityType)
    {
        return $"Entity {entityType.Name} not found for id {entityId}";
    }

}
