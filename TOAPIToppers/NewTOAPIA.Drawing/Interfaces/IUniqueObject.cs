using System;
using NewTOAPIA;

namespace NewTOAPIA.Drawing
{
    public interface IUniqueGDIObject : IHandle
    {
        Guid UniqueID { get; }
    }
}
