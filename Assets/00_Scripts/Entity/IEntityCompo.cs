public interface IEntityCompo
{
    
}

public interface IEntityCompoInit : IEntityCompo
{
    public void Initialize(Entity entity);
}

public interface IEntityCompoAfterInit : IEntityCompo
{
    public void AfterInitialize(Entity entity);
}
