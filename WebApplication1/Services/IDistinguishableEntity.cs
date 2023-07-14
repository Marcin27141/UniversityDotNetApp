using System;

namespace WebApplication1.Services
{
    public interface IDistinguishableEntity
    {
        Guid EntityId { get; }
    }
}
