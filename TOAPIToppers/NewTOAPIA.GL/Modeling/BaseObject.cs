
namespace NewTOAPIA.Modeling
{
    public class BaseObject
    {
        public string Name { get; set; }

        public BaseObject()
            :this(string.Empty)
        {
        }

        public BaseObject(string name)
        {
            Name = name;
        }
    }
}
