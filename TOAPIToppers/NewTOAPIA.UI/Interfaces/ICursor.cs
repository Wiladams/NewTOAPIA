
namespace NewTOAPIA.UI
{
    using System;

    public interface ICursor : IMoveable, IShowable
    {


        // MakeVisible will call the Show() method until the Cursor 
        // is actually visible.  You would use this method if you are
        // not keeping track of how many times Hide() gets called, but
        // you just want the Cursor to be visible.
        void MakeVisible();
    }
}