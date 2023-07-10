using System;

namespace WebApplication1.Services
{
    public interface IDistinguishableEntity
    {
        int EntityClassId { get; }
        Guid EntityId { get; }
    }
}
