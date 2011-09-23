/* DCT and IDCT - listing 3
 * Copyright (c) 2001 Emil Mikulic.
 * http://dmr.ath.cx/
 *
 * Feel free to do whatever you like with this code.
 * Feel free to credit me.
 */

using System;

using NewTOAPIA;

//#define pixel(i,x,y) ( (i)->image_data[((y)*( (i)->width ))+(x)] )


public class AANDCT
{

/* Fast DCT algorithm due to Arai, Agui, Nakajima 
 * Implementation due to Tim Kientzle
 */
public void ForwardDCT(PixelArray<BGRb> tga, double [,]data, int xpos, int ypos)
{
	int i;
	int [,] rows= new int[8,8];

	const int	c1=1004 /* cos(pi/16) << 10 */,
				s1=200 /* sin(pi/16) */,
				c3=851 /* cos(3pi/16) << 10 */,
				s3=569 /* sin(3pi/16) << 10 */,
				r2c6=554 /* sqrt(2)*cos(6pi/16) << 10 */,
				r2s6=1337 /* sqrt(2)*sin(6pi/16) << 10 */,
				r2=181; /* sqrt(2) << 7*/

	int x0,x1,x2,x3,x4,x5,x6,x7,x8;

	/* transform rows */
	for (i=0; i<8; i++)
	{
		x0 = pixel(tga, xpos+0, ypos+i);
		x1 = pixel(tga, xpos+1, ypos+i);
		x2 = pixel(tga, xpos+2, ypos+i);
		x3 = pixel(tga, xpos+3, ypos+i);
		x4 = pixel(tga, xpos+4, ypos+i);
		x5 = pixel(tga, xpos+5, ypos+i);
		x6 = pixel(tga, xpos+6, ypos+i);
		x7 = pixel(tga, xpos+7, ypos+i);

		/* Stage 1 */
		x8=x7+x0;
		x0-=x7;
		x7=x1+x6;
		x1-=x6;
		x6=x2+x5;
		x2-=x5;
		x5=x3+x4;
		x3-=x4;

		/* Stage 2 */
		x4=x8+x5;
		x8-=x5;
		x5=x7+x6;
		x7-=x6;
		x6=c1*(x1+x2);
		x2=(-s1-c1)*x2+x6;
		x1=(s1-c1)*x1+x6;
		x6=c3*(x0+x3);
		x3=(-s3-c3)*x3+x6;
		x0=(s3-c3)*x0+x6;

		/* Stage 3 */
		x6=x4+x5;
		x4-=x5;
		x5=r2c6*(x7+x8);
		x7=(-r2s6-r2c6)*x7+x5;
		x8=(r2s6-r2c6)*x8+x5;
		x5=x0+x2;
		x0-=x2;
		x2=x3+x1;
		x3-=x1;

		/* Stage 4 and output */
		rows[i][0]=x6;
		rows[i][4]=x4;
		rows[i][2]=x8>>10;
		rows[i][6]=x7>>10;
		rows[i][7]=(x2-x5)>>10;
		rows[i][1]=(x2+x5)>>10;
		rows[i][3]=(x3*r2)>>17;
		rows[i][5]=(x0*r2)>>17;
	}

	/* transform columns */
	for (i=0; i<8; i++)
	{
		x0 = rows[0][i];
		x1 = rows[1][i];
		x2 = rows[2][i];
		x3 = rows[3][i];
		x4 = rows[4][i];
		x5 = rows[5][i];
		x6 = rows[6][i];
		x7 = rows[7][i];

		/* Stage 1 */
		x8=x7+x0;
		x0-=x7;
		x7=x1+x6;
		x1-=x6;
		x6=x2+x5;
		x2-=x5;
		x5=x3+x4;
		x3-=x4;

		/* Stage 2 */
		x4=x8+x5;
		x8-=x5;
		x5=x7+x6;
		x7-=x6;
		x6=c1*(x1+x2);
		x2=(-s1-c1)*x2+x6;
		x1=(s1-c1)*x1+x6;
		x6=c3*(x0+x3);
		x3=(-s3-c3)*x3+x6;
		x0=(s3-c3)*x0+x6;

		/* Stage 3 */
		x6=x4+x5;
		x4-=x5;
		x5=r2c6*(x7+x8);
		x7=(-r2s6-r2c6)*x7+x5;
		x8=(r2s6-r2c6)*x8+x5;
		x5=x0+x2;
		x0-=x2;
		x2=x3+x1;
		x3-=x1;

		/* Stage 4 and output */
		data[0][i]=(double)((x6+16)>>3);
		data[4][i]=(double)((x4+16)>>3);
		data[2][i]=(double)((x8+16384)>>13);
		data[6][i]=(double)((x7+16384)>>13);
		data[7][i]=(double)((x2-x5+16384)>>13);
		data[1][i]=(double)((x2+x5+16384)>>13);
		data[3][i]=(double)(((x3>>8)*r2+8192)>>12);
		data[5][i]=(double)(((x0>>8)*r2+8192)>>12);
	}
}



//void quantize(double [,] dct_buf)
//{
//    int x,y;

//    for (y=0; y<8; y++)
//        for (x=0; x<8; x++)
//            if (x > 3 || y > 3) 
//                dct_buf[y][x] = 0.0;
//}



void COEFFS(out double Cu, out double Cv,int u,int v) 
{ 
	if (u == 0) 
        Cu = 1.0 / sqrt(2.0); 
    else Cu = 1.0; 

	if (v == 0) 
        Cv = 1.0 / sqrt(2.0); 
    else Cv = 1.0; 
}

public void InverseDCT(tga_image *tga, double [,]data, int xpos, int ypos)
{
	int u,v,x,y;

	/* iDCT */
	for (y=0; y<8; y++)
	for (x=0; x<8; x++)
	{
		double z = 0.0;

		for (v=0; v<8; v++)
		for (u=0; u<8; u++)
		{
			double S, q;
			double Cu, Cv;
			
			COEFFS(out Cu,out Cv,u,v);
			S = data[v][u];

			q = Cu * Cv * S *
				Math.Cos((double)(2*x+1) * (double)u * Math.PI/16.0) *
				Math.Cos((double)(2*y+1) * (double)v * Math.PI/16.0);

			z += q;
		}

		z /= 4.0;
		if (z > 255.0) z = 255.0;
		if (z < 0) z = 0.0;

		pixel(tga, x+xpos, y+ypos) = (uint8_t) z;
	}
}
}
