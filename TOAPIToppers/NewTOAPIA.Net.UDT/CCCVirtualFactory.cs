namespace NewTOAPIA.Net.Udt
{
    public abstract class CCCVirtualFactory
    {
        ~CCCVirtualFactory() { }

        public abstract CCC create();
        public abstract CCCVirtualFactory clone();
    }
}
