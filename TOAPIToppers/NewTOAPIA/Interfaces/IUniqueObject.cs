
namespace NewTOAPIA
{
    using System;

    /// <summary>
    /// There are many cases where an object is created and must be uniquely 
    /// identified in the running system, and in a distributed system.
    /// This interface ensures that unique objects can be identified similarly.
    /// </summary>
    public interface IUniqueObject
    {
        Guid UniqueID { get; }
    }
}
