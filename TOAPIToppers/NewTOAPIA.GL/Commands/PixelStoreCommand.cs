using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    public enum PixelStoreAlignment : int
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4
    }

    public class PixelStoreCommand
    {
        PixelStore fStoreParam;

        public PixelStoreCommand(PixelStore storeparam)
        {
            fStoreParam = storeparam;
        }

        public PixelStore Parameter
        {
            get { return fStoreParam; }
        }

        public virtual int CommandValue
        {
            get { return 0; }
        }
    }

    #region Data Type Specific Commands
    public class PixelStoreIntCommand : PixelStoreCommand
    {
        protected int fIntValue;

        public PixelStoreIntCommand(PixelStore storeparam, int aValue)
            :base(storeparam)
        {
            fIntValue = aValue;
        }

        public override int CommandValue
        {
            get { return fIntValue; }
        }
    }

    public class PixelStoreBoolCommand : PixelStoreCommand
    {
        protected bool fBoolValue;

        public PixelStoreBoolCommand(PixelStore storeparam, bool aValue)
            : base(storeparam)
        {
            fBoolValue = aValue;
        }

        public override int CommandValue
        {
            get { 
                int retValue = fBoolValue == true ? 1 : 0;
                return retValue;
            }
        }
    }

    #endregion

    #region Unpack Commands
    public class UnpackSwapBytesCmd : PixelStoreBoolCommand
    {
        public UnpackSwapBytesCmd(bool swapbytes)
            : base(PixelStore.PackSwapBytes, swapbytes)
        {
        }
    }

    public class UnpackLsbFirstCmd : PixelStoreBoolCommand
    {
        public UnpackLsbFirstCmd(bool swapbytes)
            : base(PixelStore.PackLsbFirst, swapbytes)
        {
        }
    }

    public class UnpackRowLengthCmd : PixelStoreIntCommand
    {
        public UnpackRowLengthCmd(int intValue)
            : base(PixelStore.PackRowLength, intValue)
        {
        }
    }

    public class UnpackSkipRowsCmd : PixelStoreIntCommand
    {
        public UnpackSkipRowsCmd(int intValue)
            : base(PixelStore.PackSkipRows, intValue)
        {
            fIntValue = intValue;
        }
    }

    public class UnpackSkipPixelsCmd : PixelStoreIntCommand
    {
        public UnpackSkipPixelsCmd(int intValue)
            : base(PixelStore.PackSkipPixels, intValue)
        {
            fIntValue = intValue;
        }
    }

    public class UnpackAlignmentCmd : PixelStoreCommand
    {
        PixelStoreAlignment fAlignmentValue;

        public UnpackAlignmentCmd(PixelStoreAlignment alignmentValue)
            : base(PixelStore.PackAlignment)
        {
            fAlignmentValue = alignmentValue;
        }

        public override int CommandValue
        {
            get { return (int)fAlignmentValue; }
        }
    }
    #endregion

    #region Pack Commands
    public class PackSwapBytesCmd : PixelStoreBoolCommand
    {
        public PackSwapBytesCmd(bool swapbytes)
            : base(PixelStore.PackSwapBytes, swapbytes)
        {
        }
    }

    public class PackLsbFirstCmd : PixelStoreBoolCommand
    {
        public PackLsbFirstCmd(bool swapbytes)
            : base(PixelStore.PackLsbFirst, swapbytes)
        {
        }
    }

    public class PackRowLengthCmd : PixelStoreIntCommand
    {
        public PackRowLengthCmd(int intValue)
            : base(PixelStore.PackRowLength, intValue)
        {
            fIntValue = intValue;
        }
    }

    public class PackSkipRowsCmd : PixelStoreIntCommand
    {
        public PackSkipRowsCmd(int intValue)
            : base(PixelStore.PackSkipRows, intValue)
        {
            fIntValue = intValue;
        }
    }

    public class PackSkipPixelsCmd : PixelStoreIntCommand
    {
        public PackSkipPixelsCmd(int intValue)
            : base(PixelStore.PackSkipPixels, intValue)
        {
            fIntValue = intValue;
        }
    }

    public class PackAlignmentCmd : PixelStoreCommand
    {
        PixelStoreAlignment fAlignmentValue;

        public PackAlignmentCmd(PixelStoreAlignment alignmentValue)
            : base(PixelStore.PackAlignment)
        {
            fAlignmentValue = alignmentValue;
        }
    }
    #endregion
}
