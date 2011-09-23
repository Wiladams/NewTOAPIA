
namespace NewTOAPIA
{
    /// <summary>
    /// There are many instances in stateful APIs where an entity
    /// must be 'selected', or activated.  ISelectable supports this pattern.
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// The object is to be selected.
        /// </summary>
        void Select();

        /// <summary>
        /// The object is to be unselected.
        /// </summary>
        void Unselect();
    }
}
