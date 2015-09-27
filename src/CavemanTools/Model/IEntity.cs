using System;

namespace CavemanTools.Model
{
    [Obsolete("It will be replaced by IEntityId")]    
    public interface IEntity
    {
        Guid Id { get; }
    }
}