using System;

interface IUniqueGDIObject : IHandle
{
    Guid UniqueID { get; }
}

