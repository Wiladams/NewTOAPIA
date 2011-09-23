using System;
using System.Drawing;

using TOAPI.Types;
using TOAPI.Kernel32;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.UI;

/// <summary>
/// Summary description for Class1
/// </summary>
public class ImageTransition : IDrawable, IAnimatable
{
    public const int
    RIGHT = 1,
    LEFT = 2,
    UP = 4,
    DOWN = 8;

	Rectangle fFrame;
	IGraphPort fGraphPort;
	GDIDIBSection fSourcePixelBuffer;
    GDIDIBSection fDestinationPixelBuffer;

    NewTOAPIA.Kernel.PrecisionTimer fTimer;
	double fDuration;

    #region Constructors
    public ImageTransition()
		:this(null,0)
	{
	}

    public ImageTransition(string name, float duration)
        : this(name, Rectangle.Empty, null, null, duration)
    {
    }

    public ImageTransition(string name, Rectangle frame, float duration)
        :this(name, frame, null, null, duration)
    {
    
    }

    public ImageTransition(string name, Rectangle frame, GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, double duration)
	{
        fTimer = new NewTOAPIA.Kernel.PrecisionTimer();
		fDestinationPixelBuffer = dstPixelBuffer;
		fSourcePixelBuffer = srcPixelBuffer;
		fDuration = duration;
		fGraphPort = null;

		// Do this last because others properties may need to be 
		// setup first.
		Frame = frame;
        DestinationPixelBuffer = fDestinationPixelBuffer;
        SourcePixelBuffer = fSourcePixelBuffer;
    }
    #endregion

    #region Properties

    public double Duration
    {
        get { return fDuration; }
    }

    public IGraphPort GraphPort
    {
        get { return fGraphPort; }
    }
    
    public Rectangle Frame
	{
		get { return fFrame; }
		set { 
			fFrame = value;
			OnFrameChanged();
		}
    }

    public double CompletionPercentage
    {
        get
        {
            double percentComplete = fTimer.GetElapsedSeconds() / Duration;
            return percentComplete;
        }
    }

    public GDIDIBSection SourcePixelBuffer
    {
        get { return fSourcePixelBuffer; }
        set { 
            fSourcePixelBuffer = value;
            OnSourcePixelBufferSet();
        }
    }

    protected virtual void OnSourcePixelBufferSet()
    {
    }

    public GDIDIBSection DestinationPixelBuffer
    {
        get { return fDestinationPixelBuffer; }
        set
        {
            fDestinationPixelBuffer = value;
            OnSetDestinationPixelBuffer();
        }
    }

    public virtual void OnSetDestinationPixelBuffer()
    {
    }

    public virtual void OnFrameChanged()
    {
    }
    #endregion


	public virtual void Reset()
	{
        fTimer.Reset();

        OnReset();
	}

	public virtual void OnReset()
	{
	}


    public virtual void Run(IGraphPort grfx)
    {
        // Reset the clock
        Reset();
        fGraphPort = grfx;

        OnFirstCell();

        while (fTimer.GetElapsedSeconds() < Duration)
        {
            UpdateGeometryState(CompletionPercentage);
            Draw(new DrawEvent(GraphPort, Frame));
        }

        OnFinalCell();
    }

    public virtual void UpdateGeometryState(double atTime)
    {
    }

    #region IDrawable
    public virtual void Draw(DrawEvent devent)
    {
    }
    #endregion

    protected virtual void OnFirstCell()
    {
    }

    protected virtual void OnFinalCell()
    {
        GraphPort.PixBlt(SourcePixelBuffer, Frame.Left, Frame.Top);
    }
}
