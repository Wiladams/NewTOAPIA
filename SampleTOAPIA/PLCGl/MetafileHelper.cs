using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public class ClipboardMetafileHelper
{
	[DllImport("user32.dll")]
	static extern bool OpenClipboard(IntPtr hWndNewOwner);
	[DllImport("user32.dll")]
	static extern bool EmptyClipboard();
	[DllImport("user32.dll")]
	static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
	[DllImport("user32.dll")]
	static extern bool CloseClipboard();
	[DllImport("gdi32.dll")]
	static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, IntPtr hNULL);
	[DllImport("gdi32.dll")]
	static extern bool DeleteEnhMetaFile(IntPtr hemf);

	// Metafile mf is set to a state that is not valid inside this function.
	static public bool PutEnhMetafileOnClipboard( IntPtr hWnd, Metafile mf )
	{
		bool bResult = false;
		IntPtr hEMF, hEMF2;
		hEMF = mf.GetHenhmetafile(); // invalidates mf
		if( ! hEMF.Equals( new IntPtr(0) ) )
		{
			hEMF2 = CopyEnhMetaFile( hEMF, new IntPtr(0) );
			if( ! hEMF2.Equals( new IntPtr(0) ) )
			{
				if( OpenClipboard( hWnd ) )
				{
					if( EmptyClipboard() )
					{
						IntPtr hRes = SetClipboardData( 14 /*CF_ENHMETAFILE*/, hEMF2 );
						bResult = hRes.Equals( hEMF2 );
						CloseClipboard();
					}
				}
			}
			DeleteEnhMetaFile( hEMF );
		}
		return bResult;
	}
}
