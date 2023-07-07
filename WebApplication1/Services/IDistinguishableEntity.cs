namespace WebApplication1.Services
{
    public interface IDistinguishableEntity
    {
        int EntityClassId { get; }
        int EntityId { get; }
        string Key { get; }
    }
}
