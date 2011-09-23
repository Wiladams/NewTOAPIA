using System;

namespace NewTOAPIA.UI
{
    public interface IInteractor : IReactToMouseActivity, IHandleMouseActivity, IReactToMouseLocation, IReactToKeyboardActivity, IHandleKeyboardActivity
    {
        bool Enabled { get; set; }
        bool Active { get; set; }

        void Focus();
        void LoseFocus();
    }
}