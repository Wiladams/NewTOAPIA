using System;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;

/// <summary>
/// Summary description for Class1
/// </summary>
public class PLCColorScheme
{
    static PLCColorScheme gDefault = new PLCColorScheme();

    public static PLCColorScheme Default
    {
        get { return gDefault; }
        set { gDefault = value; }
    }

	public PLCColorScheme()
	{
	}

	public uint ArrowRegular { get { return RGBColor.RGB(238, 238, 238); } }
	public uint ArrowRegularBorder { get { return RGBColor.RGB(204, 204, 204); } }
	
	public uint ArrowEditing { get { return RGBColor.RGB(230, 230, 230); } }
	public uint ArrowEditingBorder { get { return RGBColor.RGB(163, 163, 163); } }


	public uint ProductBack { get { return RGBColor.RGB(204, 221, 255); } }
	public uint ProductBorder {get{return RGBColor.RGB(119, 153, 238);}}
	public uint ProductText { get { return RGBColor.RGB(51, 102, 204); } }

	public uint FeatureBack { get { return RGBColor.RGB(238, 221, 255); } }
	public uint FeatureBorder { get { return RGBColor.RGB(136, 102, 153); } }
	public uint FeatureText { get { return RGBColor.RGB(136, 102, 153); } }

	public uint MilestoneBack { get { return RGBColor.RGB(255, 205, 170); } }
	public uint MilestoneBorder { get { return RGBColor.RGB(204, 51, 0); } }
	public uint MilestoneText { get { return RGBColor.RGB(153, 51, 0); } }

	public uint ChecklistBack { get { return RGBColor.RGB(221, 255, 204); } }
	public uint ChecklistBorder { get { return RGBColor.RGB(102, 204, 51); } }
	public uint ChecklistText { get { return RGBColor.RGB(17, 102, 51); } }


	public uint HeaderBack { get { return RGBColor.RGB(255, 221, 102); } }
	public uint HeaderBorder { get { return RGBColor.RGB(255, 204, 0); } }
	public uint HeaderText { get { return RGBColor.RGB(204, 153, 0); } }

    public uint StageHeaderBack { get { return RGBColor.RGB(255, 221, 102); } }
    public uint StageHeaderBorder { get { return RGBColor.RGB(255, 204, 0); } }
    public uint StageHeaderText { get { return RGBColor.RGB(102, 17, 0); } }
}
