using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Modeling
{
    public class GlobalStateManager
    {
        GlobalState[] fStates;

        public GlobalStateManager()
        {
            fStates = new GlobalState[(int)GlobalState.StateType.MAXStateType];
        }

        public GlobalState this[GlobalState.StateType i]
        {
            get
            {
                return fStates[(int)i];
            }

            set
            {
                fStates[(int)i] = value;
            }
        }

        public void RemoveAllStates()
        {
            for (int i = 0; i < fStates.Length; i++)
            {
                fStates[i] = null;
            }
        }
    }
}
