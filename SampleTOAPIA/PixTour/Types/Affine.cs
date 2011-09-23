using System;
using TOAPI.Types;

/// <summary>
/// Affine is a class used to perform Affine transforms on a matrix.
/// Affine transforms are those which can be mathematically reversed.
/// Examples include translation, scaling, shearing, and the like.
/// </summary>
public class Affine
{
	public XFORM fMatrix;

	public Affine()
	{
		fMatrix = new XFORM();

		// Start out with identity matrix
		Reset();
	}

	// Reset to identity transform
	public void Reset()
	{
		fMatrix.eM11 = 1F;
		fMatrix.eM12 = 0F;
		fMatrix.eM21 = 0F;
		fMatrix.eM22 = 1F;
		fMatrix.eDx = 0F;
		fMatrix.eDy = 0F;
	}


	// Copy transform if valid
	public bool SetTransform(XFORM xm)
	{
		if (xm.eM11 * xm.eM22 == xm.eM12 * xm.eM21)
			return false;

		fMatrix = xm;

		return true;
	}


	// transform = transform * b
	public bool Combine(XFORM b)
	{
		if (b.eM11 * b.eM22 == b.eM12 * b.eM21)
			return false;

		XFORM a = new XFORM(fMatrix);

		// 11 12   11 12
		// 21 22   21 22
		fMatrix.eM11 = a.eM11 * b.eM11 + a.eM12 * b.eM21;
		fMatrix.eM12 = a.eM11 * b.eM12 + a.eM12 * b.eM22;
		fMatrix.eM21 = a.eM21 * b.eM11 + a.eM22 * b.eM21;
		fMatrix.eM22 = a.eM21 * b.eM12 + a.eM22 * b.eM22;
		fMatrix.eDx = a.eDx * b.eM11 + a.eDy * b.eM21 + b.eDx;
		fMatrix.eDy = a.eDx * b.eM12 + a.eDy * b.eM22 + b.eDy;

		return true;
	}


	// transform = 1 / transform
	//  M = A * x + B 
	//  Inv(M) = Inv(A) * x - Inv(A) * B
	public bool Invert()
	{
		float det = fMatrix.eM11 * fMatrix.eM22 - fMatrix.eM21 * fMatrix.eM12;

		if (det == 0)
			return false;

		XFORM old = new XFORM(fMatrix);

		fMatrix.eM11 = old.eM22 / det;
		fMatrix.eM12 = -old.eM12 / det;
		fMatrix.eM21 = -old.eM21 / det;
		fMatrix.eM22 = old.eM11 / det;

		fMatrix.eDx = -(fMatrix.eM11 * old.eDx + fMatrix.eM21 * old.eDy);
		fMatrix.eDy = -(fMatrix.eM12 * old.eDx + fMatrix.eM22 * old.eDy);

		return true;
	}


	public bool Translate(float dx, float dy)
	{
		fMatrix.eDx += dx;
		fMatrix.eDy += dy;

		return true;
	}


	public bool Scale(float sx, float sy)
	{
		if ((sx == 0) || (sy == 0))
			return false;

		fMatrix.eM11 *= sx;
		fMatrix.eM12 *= sx;
		fMatrix.eM21 *= sy;
		fMatrix.eM22 *= sy;
		fMatrix.eDx *= sx;
		fMatrix.eDy *= sy;

		return true;
	}


	public bool Rotate(float angle, float x0, float y0)
	{
		XFORM xm = new XFORM();

		Translate(-x0, -y0);	// make (x0,y0) the origin

		double rad = angle * (3.14159265359 / 180);

		xm.eM11 = (float)Math.Cos(rad);
		xm.eM12 = (float)Math.Sin(rad);
		xm.eM21 = -xm.eM12;
		xm.eM22 = xm.eM11;
		xm.eDx = 0;
		xm.eDy = 0;

		Combine(xm);			// rotate
		Translate(x0, y0);		// move origin back

		return true;
	}


	// Find a transform which maps (0,0) (1,0) (0,1) to p, q, and r respectively
	public bool MapTri(float px0, float py0, float qx0, float qy0, float rx0, float ry0)
	{
		// px0 = dx, qx0 = m11 + dx, rx0 = m21 + dx
		// py0 = dy, qy0 = m12 + dy, ry0 = m22 + dy
		fMatrix.eM11 = qx0 - px0;
		fMatrix.eM12 = qy0 - py0;
		fMatrix.eM21 = rx0 - px0;
		fMatrix.eM22 = ry0 - py0;
		fMatrix.eDx = px0;
		fMatrix.eDy = py0;

		return fMatrix.eM11 * fMatrix.eM22 != fMatrix.eM12 * fMatrix.eM21;
	}


	// Find a transform which maps p0, q0, and r0 to p1, p1 and r1 respectively
	public bool MapTri(float px0, float py0, float qx0, float qy0, float rx0, float ry0,
						 float px1, float py1, float qx1, float qy1, float rx1, float ry1)
	{
		if (!MapTri(px0, py0, qx0, qy0, rx0, ry0))
			return false;

		Invert();		// transform p0, q0, and r0 back to (0,0),(1,0),(0,1)

		Affine map1 = new Affine();

		if (!map1.MapTri(px1, py1, qx1, qy1, rx1, ry1))
			return false;

		return Combine(map1.fMatrix);	// then to p1,r1,q1
	}


	// get the combined world to device coordinate space mapping
    //public bool GetDPtoLP(IntPtr hDC)
    //{
    //    if (!GDI.GetWorldTransform(hDC, ref fMatrix))
    //        return false;

    //    PointG origin = new PointG();
    //    GDI.GetWindowOrgEx(hDC,out origin);
    //    Translate(-(float)origin.X, -(float)origin.Y);

    //    SizeG sizew = new SizeG();
    //    SizeG sizev = new SizeG();
    //    GDI.GetWindowExtEx(hDC, out sizew);
    //    GDI.GetViewportExtEx(hDC, out sizev);

    //    Scale((float)sizew.Width / sizev.Height, (float)sizew.Height / sizev.Height);

    //    GDI.GetViewportOrgEx(hDC, out origin);
    //    Translate((float)origin.X, (float)origin.Y);

    //    return true;
    //}


	void minmax(int x0, int x1, int x2, int x3, ref int minx, ref int maxx)
	{
		if (x0 < x1)
		{
			minx = x0; maxx = x1;
		}
		else
		{
			minx = x1; maxx = x0;
		}

		if (x2 < minx)
			minx = x2;
		else if (x2 > maxx)
			maxx = x2;

		if (x3 < minx)
			minx = x3;
		else if (x3 > maxx)
			maxx = x3;
	}
}