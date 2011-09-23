
namespace NewTOAPIA.Modeling
{
    public class GlobalState
    {
        public enum StateType
        {
            Alpha,
            Cull,
            Material,
            PolygonOffset,
            Stencil,
            WireFrame,
            ZBuffer,
            MAXStateType
        }

        StateType fStateType;
        public static GlobalState[] gDefaultStates;

        static GlobalState()
        {
            gDefaultStates = new GlobalState[(int)StateType.MAXStateType];
        }

        protected GlobalState(StateType aType)
        {
            fStateType = aType;
        }

        public StateType TypeOfState
        {
            get { return fStateType; }
            set { fStateType = value; }
        }
    }

}
