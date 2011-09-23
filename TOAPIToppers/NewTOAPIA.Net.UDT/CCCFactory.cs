namespace NewTOAPIA.Net.Udt
{
    public class CCCFactory<T> : CCCVirtualFactory
        where T : CCC, new()
    {
        public override CCC create()
        {
            return new T();
        }

        public override CCCVirtualFactory clone()
        {
            return new CCCFactory<T>();
        }
    }
}
