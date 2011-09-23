
using System;

namespace NewTOAPIA.UI
{
    public delegate void KeyboardEventHandler(Object sender, KeyboardActivityArgs args);

    public interface IHandleKeyboardActivity
    {
        void OnKeyDown(KeyboardActivityArgs ke);
        void OnKeyUp(KeyboardActivityArgs ke);
        void OnKeyPress(KeyboardActivityArgs kpe);
    }

    public interface IReactToKeyboardActivity : IObserver<KeyboardActivityArgs>
    {
    }

}